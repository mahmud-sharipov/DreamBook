import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PostCategoryRequestModel, PostCategoryTranslationRequestModel } from 'src/app/models/requests/post-category-request-models';
import { PostCategoryWithTranslationsResponseModel } from 'src/app/models/responses/post-category-response-models';
import { PostCategoryService } from 'src/app/services/post-category-service';

@Component({
  selector: 'app-post-category-form',
  templateUrl: './post-category-form.component.html',
  styleUrls: ['./post-category-form.component.css']
})
export class PostCategoryFormComponent implements OnInit {

  @Input() entity!: PostCategoryWithTranslationsResponseModel;
  @Output() changeSubmitted = new EventEmitter();
  @Output() changeCanceled = new EventEmitter();

  constructor(private service: PostCategoryService) {
  }

  ngOnInit(): void {
  }

  onChangeSubmit(event: Event): void {
    var requestModel = new PostCategoryRequestModel(this.entity.guid, this.entity.translations
      .map(t =>
        new PostCategoryTranslationRequestModel(
          t.name,
          t.description,
          t.language.guid))
    );

    this.service.updatePostCategory(requestModel).subscribe((response: any) => {
      this.changeSubmitted.emit();
    }, (error: any) => {
    });
  }

  onChangeCancel(event: Event): void {
    this.changeCanceled.emit();
  }

}
