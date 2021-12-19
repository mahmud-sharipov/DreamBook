import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { WordRequestModel, WordTranslationRequestModel } from 'src/app/models/requests/word-request-models';
import { WordWithTranslationsResponseModel } from 'src/app/models/responses/word-response-models';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-word-form',
  templateUrl: './word-form.component.html',
  styleUrls: ['./word-form.component.css']
})
export class WordFormComponent implements OnInit {

  @Input() wordEntity!: WordWithTranslationsResponseModel;
  @Output() changeSubmitted = new EventEmitter();
  @Output() changeCanceled = new EventEmitter();

  constructor(private service: WordService) { }

  ngOnInit(): void {

  }

  onChangeSubmit(event: Event): void {
    var requestModel = new WordRequestModel(this.wordEntity.guid, this.wordEntity.translations.map(t => new WordTranslationRequestModel(t.name, t.language.guid)));
    this.service.updateWord(requestModel).subscribe((response: any) => {
      this.changeSubmitted.emit();
    }, (error: any) => {
    });
  }

  onChangeCancel(event: Event): void {
    this.changeCanceled.emit();
  }
}
