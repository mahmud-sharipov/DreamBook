import { Component, Inject, OnInit } from '@angular/core';
import { AdResponseModel } from 'src/app/models/responses/ad-response.models';
import { PagedListResponseModel } from 'src/app/models/responses/paged-list-response-model';
import { AdService } from 'src/app/services/ad-service';

@Component({
  selector: 'app-ad',
  templateUrl: './ad.component.html',
  styleUrls: ['./ad.component.css']
})
export class AdComponent implements OnInit {

  ads: AdResponseModel[] = [];
  currentPage: number = 1;
  hasMore: boolean = false;

  constructor(private service: AdService) { }

  ngOnInit(): void {
    this.service.getAds()
      .subscribe((result: PagedListResponseModel<AdResponseModel>) => {
        this.ads.push(...result.items)
        this.hasMore = result.hasNextPage;
        this.currentPage = result.pageIndex;
      })
  }

  onMore(): void {
    this.service.getAds(this.currentPage + 1)
      .subscribe((result: PagedListResponseModel<AdResponseModel>) => {
        this.ads.push(...result.items)
        this.hasMore = result.hasNextPage;
        this.currentPage = result.pageIndex;
      })
  }

}
