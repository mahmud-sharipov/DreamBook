import { Injectable } from '@angular/core';
import { JwtTokenResponse } from '../models/responses/auth-response-models';

const TOKEN_KEY = 'auth-token';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor() { }

  signOut(): void {
    localStorage.clear();
  }

  public saveToken(token: JwtTokenResponse): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.setItem(TOKEN_KEY, JSON.stringify(token));
  }

  public getToken(): JwtTokenResponse | null {

    let token = localStorage.getItem(TOKEN_KEY);
    if (token == null || token === "")
      return null;

    return <JwtTokenResponse>JSON.parse(token);
  }
}
