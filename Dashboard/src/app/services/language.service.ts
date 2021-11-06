import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LanguageResponseModel } from '../models/responses/language-response-models';
import { API_BASE_URL } from './end-point';

export const LANGUAGES_STORAGE_KEY: string = 'ALL_SUPPORT_LANGUAGES';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private http: HttpClient,
    @Inject(API_BASE_URL) private hostUrl: string) { }

  getWords(): Observable<LanguageResponseModel[]> {
    let storageData = localStorage.getItem(LANGUAGES_STORAGE_KEY);
    if (storageData) {
      return of(JSON.parse(storageData))
    }
    else {
      let url = this.hostUrl + 'languages';
      const request = this.http.get<LanguageResponseModel[]>(url);
      request.subscribe((result: LanguageResponseModel[]) => {
        localStorage.setItem(LANGUAGES_STORAGE_KEY, JSON.stringify(result));
      });
      return request;
    }
  }
}
