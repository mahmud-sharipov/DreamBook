import { Component, OnInit } from '@angular/core';
import { BookResponseModel } from 'src/app/models/responses/book-response-models';
import { PagedListResponseModel } from 'src/app/models/responses/paged-list-response-model';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styles: ['']
})
export class BooksComponent implements OnInit {
  books: PagedListResponseModel<BookResponseModel>;
  selectedBook: BookResponseModel;
  searchText: string = '';
  pageNumber: number = 1;
  isListBlocked: boolean = false;

  constructor(private service: BookService) {
    this.selectedBook = { guid: '', name: '', description: '' }
    this.books = new PagedListResponseModel<BookResponseModel>();
  }

  ngOnInit(): void {
    this.updateList();
  }

  onBookSelectionChanged(selectedItem: BookResponseModel): void {
    this.selectedBook = selectedItem;
  }

  OnPageChanged(page: number) {
    this.pageNumber = page;
    this.updateList();
  }

  onBookEdited(id: string) {
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
    this.service.getBooks(this.pageNumber, this.searchText).subscribe((result: PagedListResponseModel<BookResponseModel>) => {
      this.books = result;
    }, (error: any) => { });
  }

}
