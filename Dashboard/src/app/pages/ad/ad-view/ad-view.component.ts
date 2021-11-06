import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdWithTranslationsResponseModel } from 'src/app/models/responses/ad-response.models';
import { AdService } from 'src/app/services/ad-service';
import { API_BASE_URL } from 'src/app/services/end-point';

@Component({
  selector: 'app-ad-view',
  templateUrl: './ad-view.component.html',
  styleUrls: ['./ad-view.component.css']
})
export class AdViewComponent implements OnInit {

  entity: AdWithTranslationsResponseModel;
  imageApiUrl: string = '';
  title: string = '';

  constructor(private service: AdService, public route: ActivatedRoute, private router: Router,
    @Inject(API_BASE_URL) private hostUrl: string) {
    this.entity = { guid: '', image: '', createdAt: '', source: '', isActive: false, translations: [] };
    this.imageApiUrl = hostUrl + 'files';
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      let id = this.route.snapshot.paramMap.get('id');
      if (!id)
        this.router.navigate(["not-fount"]);

      this.service.getAd(id).subscribe((data: AdWithTranslationsResponseModel) => {
        this.entity = data;
        this.title = data.translations.map(t => t.title).join('/')
      }, requestError => {
        console.log(requestError.status);
        this.router.navigate(["not-fount"]);
      });

    }, error => {
      this.router.navigate(["not-fount"]);
    });
  }

}
