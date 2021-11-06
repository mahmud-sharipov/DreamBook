using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.API.Auth.Model
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }

        public DateTime ExpiryOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedByIp { get; set; }

        public string RevokedByIp { get; set; }

        public DateTime RevokedOn { get; set; }
    }
}
