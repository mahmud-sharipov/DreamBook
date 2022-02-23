import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BookRequestModel, BookTranslationRequestModel } from 'src/app/models/requests/book-request-models';
import { BookWithTranslationsResponseModel } from 'src/app/models/responses/book-response-models';
import { LanguageResponseModel } from 'src/app/models/responses/language-response-models';
import { BookService } from 'src/app/services/book.service';
import { enGuid, LanguageService, ruGuid } from 'src/app/services/language.service';
import { Toast, NotificationSeverity } from 'src/app/services/Notification';


@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styles: ['']
})
export class BookFormComponent implements OnInit {

  @Input() bookEntity!: BookWithTranslationsResponseModel;
  @Output() changeSubmitted = new EventEmitter();
  @Output() changeCanceled = new EventEmitter();
  form!: FormGroup;
  ruLanguage!: LanguageResponseModel;
  enLanguage!: LanguageResponseModel;

  constructor(private service: BookService, private languageService: LanguageService) {
    this.languageService.getLanguages().subscribe(l => {
      this.enLanguage = l.find(l => l.guid == enGuid) || { code: "", name: "", guid: '', isDefault: false };
      this.ruLanguage = l.find(l => l.guid == ruGuid) || { code: "", name: "", guid: '', isDefault: false };
    });
  }

  ngOnInit(): void {
    var en = this.bookEntity?.translations.find(b => b.language.guid = enGuid) || { name: "", description: "", language: enGuid };
    var ru = this.bookEntity?.translations.find(b => b.language.guid = ruGuid) || { name: "", description: "", language: ruGuid };

    this.form = new FormGroup({
      enName: new FormControl(en.name, [Validators.required]),
      enDescription: new FormControl(en.description),
      ruName: new FormControl(ru.name, [Validators.required]),
      ruDescription: new FormControl(ru.description)
    });
  }

  onChangeSubmit(event: Event): void {
    var requestModel = new BookRequestModel(this.bookEntity.guid,
      [
        { name: this.enName?.value, description: this.enDescription?.value, languageGuid: enGuid },
        { name: this.ruName?.value, description: this.ruDescription?.value, languageGuid: ruGuid }
      ]);

    this.service.updateBook(requestModel)
      .subscribe((response: any) => {
        this.changeSubmitted.emit();
      }, (error: any) => {

      });
  }

  onChangeCancel(event: Event): void {
    this.changeCanceled.emit();
  }

  alertConfirmation() {
    Toast.fire({ icon: NotificationSeverity.error, title: 'Signed in successfully' });
  }

  get enName() { return this.form.get('enName'); }
  get enDescription() { return this.form.get('enDescription'); }

  get ruName() { return this.form.get('ruName'); }
  get ruDescription() { return this.form.get('ruDescription'); }
}
