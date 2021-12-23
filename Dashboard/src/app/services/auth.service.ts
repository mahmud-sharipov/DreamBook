import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthSucceededResponse, JwtTokenResponse } from '../models/responses/auth-response-models';
import { UserResponseModel } from '../models/responses/user-response-models';
import { API_BASE_URL } from './end-point';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, @Inject(API_BASE_URL) private hostUrl: string) { }

  login(username: string, password: string): Observable<AuthSucceededResponse> {
    return this.http.post<AuthSucceededResponse>(this.hostUrl + 'auth/login', { userName: username, password: password });
  }

  loginWithGoogle(token: string): Observable<AuthSucceededResponse> {
    return this.http.post<AuthSucceededResponse>(this.hostUrl + 'auth/login/external/google', { idToken: token });
  }

  refreshToken(token: string): Observable<JwtTokenResponse> {
    return this.http.post<JwtTokenResponse>(this.hostUrl + 'auth/refresh-token', '"' + token + '"');
  }

  getUserDate(token: string): Observable<UserResponseModel> {
    return this.http.get<UserResponseModel>(this.hostUrl + + 'auth/me');
  }
}
