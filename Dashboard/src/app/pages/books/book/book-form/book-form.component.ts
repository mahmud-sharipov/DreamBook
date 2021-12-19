import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BookRequestModel, BookTranslationRequestModel } from 'src/app/models/requests/book-request-models';
import { BookWithTranslationsResponseModel } from 'src/app/models/responses/book-response-models';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit {

  @Input() bookEntity!: BookWithTranslationsResponseModel;
  @Output() changeSubmitted = new EventEmitter();
  @Output() changeCanceled = new EventEmitter();

  constructor(private service: BookService) { }

  ngOnInit(): void {
  }

  onChangeSubmit(event: Event): void {
    var requestModel = new BookRequestModel(this.bookEntity.guid, this.bookEntity.translations.map(t => new BookTranslationRequestModel(t.name, t.description, t.language.guid)));
    this.service.updateBook(requestModel).subscribe((response: any) => {
      this.changeSubmitted.emit();
    }, (error: any) => {
    });
  }

  onChangeCancel(event: Event): void {
    this.changeCanceled.emit();
  }

}
