import { Component, OnInit } from '@angular/core';
import { PagedListResponseModel } from 'src/app/models/responses/paged-list-response-model';
import { PostCategoryResponseModel } from 'src/app/models/responses/post-category-response-models';
import { PostCategoryService } from 'src/app/services/post-category-service';

@Component({
  selector: 'app-post-categories',
  templateUrl: './post-categories.component.html',
  styleUrls: ['./post-categories.component.css']
})
export class PostCategoriesComponent implements OnInit {

  page: PagedListResponseModel<PostCategoryResponseModel>;
  selectedEntity: PostCategoryResponseModel;
  searchText: string = '';
  pageNumber: number = 1;
  isListBlocked: boolean = false;

  constructor(private service: PostCategoryService) {
    this.selectedEntity = { guid: '', name: '', description: '' }
    this.page = new PagedListResponseModel<PostCategoryResponseModel>();
  }

  ngOnInit(): void {
    this.updateList();
  }

  onSelectedEntityChanged(selectedItem: PostCategoryResponseModel): void {
    this.selectedEntity = selectedItem;
  }

  OnPageChanged(page: number) {
    this.pageNumber = page;
    this.updateList();
  }

  onEntityEdited(id: string) {
    this.updateList()
  }

  onSearchTextChanged(searchText: string): void {
    this.searchText = searchText;
    this.pageNumber = 1;
    this.updateList();
  }

  onAdd(): void {

  }

  onDisplayModeChanged(isEditing: boolean): void {
    this.isListBlocked = isEditing;
  }

  updateList(): void {
    this.service.getPostCategories(this.pageNumber, this.searchText).subscribe((result: PagedListResponseModel<PostCategoryResponseModel>) => {
      this.page = result;
    }, (error: any) => {
    });
  }

}
