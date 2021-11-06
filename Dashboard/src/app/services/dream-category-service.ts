import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DreamCategoryRequestModel } from '../Models/Requests/dream-caregory-request-models';
import { DreamCategoryResponseModel, DreamCategoryWithTranslationsResponseModel } from '../models/responses/dream-caregory-response-models';
import { PagedListResponseModel } from '../models/responses/paged-list-response-model';
import { API_BASE_URL } from './end-point';

@Injectable({
    providedIn: 'root'
})
export class DreamCategoryService {

    baseUrl: string = '';
    constructor(private http: HttpClient,
        @Inject(API_BASE_URL) private hostUrl: string) {
        this.baseUrl = this.hostUrl + 'dreamtypes';
    }

    getDreamCategories(pageNumber: number = 1, searchText: string = ''): Observable<PagedListResponseModel<DreamCategoryResponseModel>> {
        let url = this.baseUrl + '?pageSize=10&searchText=' + searchText + '&pageNumber=' + pageNumber;
        return this.http.get<PagedListResponseModel<DreamCategoryResponseModel>>(url);
    }

    getDreamCategory(guid: string): Observable<DreamCategoryWithTranslationsResponseModel> {
        return this.http.get<DreamCategoryWithTranslationsResponseModel>(this.baseUrl + '/with-all-translations/' + guid);
    }

    updateDreamCategory(dreamCategory: DreamCategoryRequestModel): Observable<any> {
        return this.http.put(this.baseUrl, dreamCategory);
    }
}
