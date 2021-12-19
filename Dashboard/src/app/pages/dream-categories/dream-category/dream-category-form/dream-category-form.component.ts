import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { COLOR_PICKER_SCRIPT } from 'src/app/helpers/scrip-model';
import { DreamCategoryRequestModel, DreamCategoryTranslationRequestModel } from 'src/app/Models/Requests/dream-caregory-request-models';
import { DreamCategoryWithTranslationsResponseModel } from 'src/app/models/responses/dream-caregory-response-models';
import { DreamCategoryService } from 'src/app/services/dream-category-service';

@Component({
  selector: 'app-dream-category-form',
  templateUrl: './dream-category-form.component.html',
  styleUrls: ['./dream-category-form.component.css']
})
export class DreamCategoryFormComponent implements OnInit {

  @Input() entity!: DreamCategoryWithTranslationsResponseModel;
  @Output() changeSubmitted = new EventEmitter();
  @Output() changeCanceled = new EventEmitter();

  constructor(private service: DreamCategoryService) {
  }

  ngOnInit(): void {
  }

  onChangeSubmit(event: Event): void {
    var requestModel = new DreamCategoryRequestModel(this.entity.guid, this.entity.color, this.entity.translations
      .map(t =>
        new DreamCategoryTranslationRequestModel(
          t.name,
          t.description,
          t.language.guid))
    );

    this.service.updateDreamCategory(requestModel).subscribe((response: any) => {
      this.changeSubmitted.emit();
    }, (error: any) => {
    });
  }

  onChangeCancel(event: Event): void {
    this.changeCanceled.emit();
  }
}
