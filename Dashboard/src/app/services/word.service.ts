import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WordRequestModel } from '../models/requests/word-request-models';
import { PagedListResponseModel } from '../models/responses/paged-list-response-model';
import { WordInterpretationResponseModel, WordResponseModel, WordWithTranslationsResponseModel } from '../models/responses/word-response-models';
import { API_BASE_URL } from './end-point';

@Injectable({
  providedIn: 'root'
})
export class WordService {

  constructor(private http: HttpClient,
    @Inject(API_BASE_URL) private hostUrl: string) {
  }

  getWords(pageNumber: number = 1, searchText: string = ''): Observable<PagedListResponseModel<WordResponseModel>> {
    let url = this.hostUrl + 'words?pageSize=10&searchText=' + searchText + '&pageNumber=' + pageNumber;
    return this.http.get<PagedListResponseModel<WordResponseModel>>(url);
  }

  getWord(guid: string): Observable<WordWithTranslationsResponseModel> {
    return this.http.get<WordWithTranslationsResponseModel>(this.hostUrl + 'words/with-all-translations/' + guid);
  }

  updateWord(word: WordRequestModel): Observable<any> {
    return this.http.put(this.hostUrl + 'words', word);
  }

  getWordInterpretation(id: string): Observable<WordInterpretationResponseModel[]> {
    return this.http.get<WordInterpretationResponseModel[]>(this.hostUrl + 'words/' + id + '/interpretations');
  }
}
