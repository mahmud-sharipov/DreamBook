import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, TemplateRef } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { PagedListResponseModel } from 'src/app/models/responses/paged-list-response-model';

@Component({
  selector: 'app-paged-list',
  templateUrl: './paged-list.component.html',
  styleUrls: ['./paged-list.component.css']
})
export class PagedListComponent implements OnInit, OnDestroy {

  @Input() idPropertyName: string = 'guid';
  @Input() displayPropertyName!: string;
  @Input() page!: PagedListResponseModel<any>;
  @Input() canAdd: boolean = true;
  @Input() canSelect: boolean = true;
  @Input() isEnable: boolean = true;

  @Output() onPageNumberChange = new EventEmitter<number>();
  @Output() onSearchTextChange = new EventEmitter<string>();
  @Output() onAdd = new EventEmitter();
  @Output() onSelectedItemChange = new EventEmitter<any>();

  private _unsubscribeAll: Subject<any> = new Subject();

  selectedItem: any = null;
  searchTextInput: FormControl = new FormControl('');

  constructor() { }

  ngOnInit(): void {
    this.searchTextInput.valueChanges
      .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(150),
        distinctUntilChanged()
      )
      .subscribe(searchText => {
        this.onSearchTextChange.emit(searchText);
      });
  }

  onAddClick(event: any): void {
    this.onAdd.emit();
  }

  onItemSelectionChanged(item: any): void {
    this.selectedItem = item;
    this.onSelectedItemChange.emit(item);
  }

  onPageChanged(page: number) {
    this.onPageNumberChange.emit(page);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next()
  }
}
