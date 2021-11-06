import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { WordInterpretationResponseModel, WordWithTranslationsResponseModel } from 'src/app/models/responses/word-response-models';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-word',
  templateUrl: './word.component.html',
  styleUrls: ['./word.component.css']
})
export class WordComponent implements OnInit {

  @Input() wordGuid!: string;
  @Input() isEditing: boolean = false;
  @Output() onWordChanged = new EventEmitter<string>();
  @Output() onDisplayModeChanged = new EventEmitter<boolean>();

  word: WordWithTranslationsResponseModel;
  wordNames: string = '';
  interpretations: WordInterpretationResponseModel[] = [];

  constructor(private wordService: WordService) {
    this.word = { guid: '', translations: [] };
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

  onWordChangeSubmitted(event: Event): void {
    this.isEditing = false;
    this.onWordChanged.emit(this.wordGuid);
    this.onDisplayModeChanged.emit(this.isEditing);
    this.init();
  }

  onWordChangeCanceled(event: Event): void {
    this.isEditing = false;
    this.onDisplayModeChanged.emit(this.isEditing);
  }

  init(): void {
    this.wordService.getWord(this.wordGuid).subscribe((result: WordWithTranslationsResponseModel) => {
      this.word = result;
      this.wordNames = result.translations.map(t => t.name).join('/');
    }, (error: any) => {
      console.log(error);
    });

    this.wordService.getWordInterpretation(this.wordGuid).subscribe((result: WordInterpretationResponseModel[]) => {
      this.interpretations = result;
    }, (error: any) => {
      console.log(error);
    });
  }

}
