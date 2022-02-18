using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace DreamBook.API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly UserManager<User> _userManager;
        private readonly IContext _dbContext;
        private readonly TimeSpan _accessTokenEexparationTime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TimeSpan _refreshTokenExparationTime;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, IContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("Jwt");
            _goolgeSettings = _configuration.GetSection("Auth:Google");
            _accessTokenEexparationTime = new TimeSpan(24, 0, 0);
            _refreshTokenExparationTime = new TimeSpan(30, 0, 0, 0);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleAuthRequest externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _goolgeSettings["ClientId"] }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<JwtTokenResponse> GenerateTokens(User user)
        {
            var token = await GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);
            user.RefreshTokens.Add(refreshToken);
            _dbContext.Update(user.Id, user);
            _dbContext.SaveChanges();
            return new JwtTokenResponse()
            {
                AccessToken = token.Token,
                AccessTokenValidTo = token.ValidTo,
                RefreshToken = refreshToken.Token,
                RefreshTokenValidTo = refreshToken.ExpiryOn
            };
        }

        internal const string UserIdClaim = "UserGuid";

        private async Task<(string Token, DateTime ValidTo)> GenerateAccessToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(UserIdClaim, user.Id.ToString()),
                new Claim(ClaimTypes.Sid, user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Issuer"],
                expires: DateTime.UtcNow.Add(_accessTokenEexparationTime),
                claims: authClaims,
                signingCredentials: credentials
            );
            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        private RefreshToken GenerateRefreshToken(Guid userId)
        {
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiryOn = DateTime.UtcNow.Add(_refreshTokenExparationTime),
                CreatedOn = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                UserGuid = userId
            };
        }

        public bool IsRefreshTokenValid(RefreshToken existingToken)
        {
            if (existingToken.RevokedByIp != null && existingToken.RevokedOn != DateTime.MinValue)
                return false;

            if (existingToken.ExpiryOn <= DateTime.UtcNow)
                return false;

            return true;
        }

        public DateTime GetValidToDate(DateTime? from = null) => (from ?? DateTime.UtcNow).Add(_accessTokenEexparationTime);

        public RefreshToken GetValidRefreshToken(string token, User identityUser)
        {
            if (identityUser == null)
                return null;

            var existingToken = identityUser.RefreshTokens.FirstOrDefault(x => x.Token == token);
            return IsRefreshTokenValid(existingToken) ? existingToken : null;
        }

        public async Task<JwtTokenResponse> RefreshToken(string token, User user)
        {
            var existingRefreshToken = GetValidRefreshToken(token, user);
            if (existingRefreshToken == null)
                throw new BadHttpRequestException("Invalid refresh token!");

            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            existingRefreshToken.RevokedByIp = ipAddress;
            existingRefreshToken.RevokedOn = DateTime.UtcNow;
            return await GenerateTokens(user);
        }

        public async Task RevokeRefreshToken(string token, User user)
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var existingToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
            existingToken.RevokedByIp = ipAddress;
            existingToken.RevokedOn = DateTime.UtcNow;
            _dbContext.Update(user.Id, user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RevokeRefreshToken(string userName)
        {
            var user = _dbContext.GetFirstOrDefault<User>(u => u.UserName.Equals(userName));
            if (user != null)
            {
                var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var tokens = user.RefreshTokens.Where(t => string.IsNullOrEmpty(t.RevokedByIp) && t.ExpiryOn > DateTime.UtcNow && t.CreatedByIp == ipAddress);
                foreach (var token in tokens)
                {
                    token.RevokedByIp = ipAddress;
                    token.RevokedOn = DateTime.UtcNow;
                    _dbContext.Update(token.Guid, token);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
