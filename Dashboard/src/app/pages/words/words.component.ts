import { Component, OnDestroy, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged, map, takeUntil } from 'rxjs/operators';
import { PagedListResponseModel } from 'src/app/models/responses/paged-list-response-model';
import { WordInterpretationResponseModel, WordResponseModel } from 'src/app/models/responses/word-response-models';
import { WordService } from 'src/app/services/word.service';

@Component({
  selector: 'app-words',
  templateUrl: './words.component.html',
  styleUrls: ['./words.component.css']
})
export class WordsComponent implements OnInit {

  words: PagedListResponseModel<WordResponseModel>;
  selectedWord: WordResponseModel;
  searchText: string = '';
  pageNumber: number = 1;
  isListBlocked: boolean = false;

  constructor(private wordService: WordService) {
    this.selectedWord = { guid: '', name: '' }
    this.words = new PagedListResponseModel<WordResponseModel>();
  }

  ngOnInit(): void {
    this.updateList();
  }

  OnWordSelectionChanged(selectedItem: WordResponseModel): void {
    this.selectedWord = selectedItem;
  }

  OnPageChanged(page: number) {
    this.pageNumber = page;
    this.updateList();
  }

  onWordEdited(id: string) {
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
    this.wordService.getWords(this.pageNumber, this.searchText).subscribe((result: PagedListResponseModel<WordResponseModel>) => {
      this.words = result;
    }, (error: any) => {
    });
  }
}
