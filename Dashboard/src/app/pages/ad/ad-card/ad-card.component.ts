import { Component, Inject, Input, OnInit } from '@angular/core';
import { AdResponseModel } from 'src/app/models/responses/ad-response.models';
import { API_BASE_URL } from 'src/app/services/end-point';

@Component({
  selector: 'app-ad-card',
  templateUrl: './ad-card.component.html',
  styleUrls: ['./ad-card.component.css']
})
export class AdCardComponent implements OnInit {

  @Input() ad!: AdResponseModel;
  imageApiUrl: string = '';

  constructor(@Inject(API_BASE_URL) private hostUrl: string) {
    this.imageApiUrl = hostUrl + 'files';
  }

  ngOnInit(): void {
  }
}
