import { Component, OnInit } from '@angular/core';
import { DreamCategoryResponseModel } from 'src/app/models/responses/dream-caregory-response-models';
import { PagedListResponseModel } from 'src/app/models/responses/paged-list-response-model';
import { DreamCategoryService } from 'src/app/services/dream-category-service';

@Component({
  selector: 'app-dream-categories',
  templateUrl: './dream-categories.component.html',
  styleUrls: ['./dream-categories.component.css']
})
export class DreamCategoriesComponent implements OnInit {

  page: PagedListResponseModel<DreamCategoryResponseModel>;
  selectedEntity: DreamCategoryResponseModel;
  searchText: string = '';
  pageNumber: number = 1;
  isListBlocked: boolean = false;

  constructor(private service: DreamCategoryService) {
    this.selectedEntity = { guid: '', name: '', color: '', description: '' }
    this.page = new PagedListResponseModel<DreamCategoryResponseModel>();
  }

  ngOnInit(): void {
    this.updateList();
  }

  onSelectedEntityChanged(selectedItem: DreamCategoryResponseModel): void {
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
    this.service.getDreamCategories(this.pageNumber, this.searchText).subscribe((result: PagedListResponseModel<DreamCategoryResponseModel>) => {
      this.page = result;
    }, (error: any) => {
      console.log(error);
    });
  }

}
