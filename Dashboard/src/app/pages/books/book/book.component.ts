import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BookWithTranslationsResponseModel } from 'src/app/models/responses/book-response-models';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styles: ['']
})
export class BookComponent implements OnInit {

  @Input() bookGuid!: string;
  @Input() isEditing: boolean = false;
  @Output() onBookChanged = new EventEmitter<string>();
  @Output() onDisplayModeChanged = new EventEmitter<boolean>();

  book: BookWithTranslationsResponseModel;
  bookNames: string = '';

  constructor(private service: BookService) {
    this.book = { guid: '', translations: [] };
  }

  ngOnInit(): void {
    this.init();
  }

  ngOnChanges(changes: any) {
    this.isEditing = false;
    this.init();
  }

  onEditBtnClick(): void {
    this.isEditing = !this.isEditing;
    this.onDisplayModeChanged.emit(this.isEditing);
  }

  onBookChangeSubmitted(event: Event): void {
    this.isEditing = false;
    this.onBookChanged.emit(this.bookGuid);
    this.onDisplayModeChanged.emit(this.isEditing);
    this.init();
  }

  onBookChangeCanceled(event: Event): void {
    this.isEditing = false;
    this.onDisplayModeChanged.emit(this.isEditing);
  }

  init(): void {
    this.service.getBook(this.bookGuid).subscribe((result: BookWithTranslationsResponseModel) => {
      this.book = result;
      this.bookNames = result.translations.map(t => t.name).join('/');
    }, (error: any) => {
    });
  }
}
