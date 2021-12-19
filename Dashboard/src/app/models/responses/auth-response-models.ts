import { UserResponseModel } from "./user-response-models";

export interface JwtTokenResponse {
    accessToken: string;
    refreshToken: string;
    accessTokenValidTo: string;
    refreshTokenValidTo: string;
}

export interface AuthSucceededResponse {
    tokenInfo: JwtTokenResponse;
    user: UserResponseModel;
}