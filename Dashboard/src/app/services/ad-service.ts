import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdResponseModel, AdWithTranslationsResponseModel } from '../models/responses/ad-response.models';
import { PagedListResponseModel } from '../models/responses/paged-list-response-model';
import { API_BASE_URL } from './end-point';

@Injectable({
    providedIn: 'root'
})
export class AdService {

    baseUrl: string = '';
    constructor(private http: HttpClient,
        @Inject(API_BASE_URL) private hostUrl: string) {
        this.baseUrl = this.hostUrl + 'ads';
    }

    getAds(pageNumber: number = 1, searchText: string = '', pageSize: number = 12): Observable<PagedListResponseModel<AdResponseModel>> {
        let url = `${this.baseUrl}?pageSize=${pageSize}&searchText=${searchText}&pageNumber=${pageNumber}`;
        return this.http.get<PagedListResponseModel<AdResponseModel>>(url);
    }

    getAd(guid: any): Observable<AdWithTranslationsResponseModel> {
        return this.http.get<AdWithTranslationsResponseModel>(this.baseUrl + '/with-all-translations/' + guid);
    }

}