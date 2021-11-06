export class PagedListResponseModel<T> {
    pageIndex: number = 1;
    pageSize: number = 0;
    totalCount: number = 0;
    totalPages: number = 0;
    items: T[] = [];
    hasPreviousPage: boolean = false;
    hasNextPage: boolean = false;
}