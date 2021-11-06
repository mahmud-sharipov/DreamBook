import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookRequestModel } from '../models/requests/book-request-models';
import { BookResponseModel, BookWithTranslationsResponseModel } from '../models/responses/book-response-models';
import { PagedListResponseModel } from '../models/responses/paged-list-response-model';
import { API_BASE_URL } from './end-point';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  baseUrl: string = '';
  constructor(private http: HttpClient,
    @Inject(API_BASE_URL) private hostUrl: string) {
    this.baseUrl = this.hostUrl + 'books';
  }

  getBooks(pageNumber: number = 1, searchText: string = ''): Observable<PagedListResponseModel<BookResponseModel>> {
    let url = this.baseUrl + '?pageSize=10&searchText=' + searchText + '&pageNumber=' + pageNumber;
    return this.http.get<PagedListResponseModel<BookResponseModel>>(url);
  }

  getBook(guid: string): Observable<BookWithTranslationsResponseModel> {
    return this.http.get<BookWithTranslationsResponseModel>(this.baseUrl + '/with-all-translations/' + guid);
  }

  updateBook(book: BookRequestModel): Observable<any> {
    return this.http.put(this.baseUrl, book);
  }
}
