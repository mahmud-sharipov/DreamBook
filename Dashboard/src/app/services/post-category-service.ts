import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostCategoryRequestModel } from '../models/requests/post-category-request-models';
import { PagedListResponseModel } from '../models/responses/paged-list-response-model';
import { PostCategoryResponseModel, PostCategoryWithTranslationsResponseModel } from '../models/responses/post-category-response-models';
import { API_BASE_URL } from './end-point';

@Injectable({
    providedIn: 'root'
})
export class PostCategoryService {

    baseUrl: string = '';
    constructor(private http: HttpClient,
        @Inject(API_BASE_URL) private hostUrl: string) {
        this.baseUrl = this.hostUrl + 'postcategories';
    }

    getPostCategories(pageNumber: number = 1, searchText: string = ''): Observable<PagedListResponseModel<PostCategoryResponseModel>> {
        let url = this.baseUrl + '?pageSize=10&searchText=' + searchText + '&pageNumber=' + pageNumber;
        return this.http.get<PagedListResponseModel<PostCategoryResponseModel>>(url);
    }

    getPostCategory(guid: string): Observable<PostCategoryWithTranslationsResponseModel> {
        return this.http.get<PostCategoryWithTranslationsResponseModel>(this.baseUrl + '/with-all-translations/' + guid);
    }

    updatePostCategory(postCategory: PostCategoryRequestModel): Observable<any> {
        return this.http.put(this.baseUrl, postCategory);
    }
}
