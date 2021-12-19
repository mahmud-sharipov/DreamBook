import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PostCategoryWithTranslationsResponseModel } from 'src/app/models/responses/post-category-response-models';
import { PostCategoryService } from 'src/app/services/post-category-service';

@Component({
  selector: 'app-post-category',
  templateUrl: './post-category.component.html',
  styleUrls: ['./post-category.component.css']
})
export class PostCategoryComponent implements OnInit {

  @Input() entityGuid!: string;
  @Input() isEditing: boolean = false;
  @Output() onEntityChanged = new EventEmitter<string>();
  @Output() onDisplayModeChanged = new EventEmitter<boolean>();

  entity: PostCategoryWithTranslationsResponseModel;
  names: string = '';

  constructor(private service: PostCategoryService) {
    this.entity = { guid: '', translations: [] };
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
    this.service.getPostCategory(this.entityGuid).subscribe((result: PostCategoryWithTranslationsResponseModel) => {
      this.entity = result;
      this.names = result.translations.map(t => t.name).join('/');
    }, (error: any) => {
    });
  }
}
