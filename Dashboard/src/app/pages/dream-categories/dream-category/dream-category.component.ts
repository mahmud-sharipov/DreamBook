import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DreamCategoryWithTranslationsResponseModel } from 'src/app/models/responses/dream-caregory-response-models';
import { DreamCategoryService } from 'src/app/services/dream-category-service';

@Component({
  selector: 'app-dream-category',
  templateUrl: './dream-category.component.html',
  styleUrls: ['./dream-category.component.css']
})
export class DreamCategoryComponent implements OnInit {

  @Input() entityGuid!: string;
  @Input() isEditing: boolean = false;
  @Output() onEntityChanged = new EventEmitter<string>();
  @Output() onDisplayModeChanged = new EventEmitter<boolean>();

  entity: DreamCategoryWithTranslationsResponseModel;
  names: string = '';

  constructor(private service: DreamCategoryService) {
    this.entity = { guid: '', color: '', translations: [] };
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

  onEntityChangeSubmitted(event: Event): void {
    this.isEditing = false;
    this.onEntityChanged.emit(this.entityGuid);
    this.onDisplayModeChanged.emit(this.isEditing);
    this.init();
  }

  onEntityChangeCanceled(event: Event): void {
    this.isEditing = false;
    this.onDisplayModeChanged.emit(this.isEditing);
  }

  init(): void {
    this.service.getDreamCategory(this.entityGuid).subscribe((result: DreamCategoryWithTranslationsResponseModel) => {
      this.entity = result;
      this.names = result.translations.map(t => t.name).join('/');
    }, (error: any) => {
      console.log(error);
    });
  }

}
