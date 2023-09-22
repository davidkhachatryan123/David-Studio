import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { AppColors, AppTags } from 'src/app/website/consts';
import { Tag } from './models';

@Component({
  selector: 'services-web',
  templateUrl: 'services.component.html',
  styleUrls: [ 'services.component.css' ]
})
export class ServicesComponent {
  translateSectionName = 'services';

  constructor(
    private title: Title,
    private translate: TranslateService) {

    translate.get(`title.${this.translateSectionName}`).subscribe(text => title.setTitle(text));
  }

  front_circles: Array<Tag> = [
    new Tag(AppTags.html, AppColors.html),
    new Tag(AppTags.css, AppColors.css),
    new Tag(AppTags.js, AppColors.js),
    new Tag(AppTags.ts, AppColors.ts),
    new Tag(AppTags.bootstrap, AppColors.bootstrap),
    new Tag(AppTags.angular, AppColors.angular)
  ];

  back_circles: Array<Tag> = [
    new Tag(AppTags.aspnet, AppColors.aspnet),
    new Tag(AppTags.cs, AppColors.cs),
    new Tag(AppTags.ef, AppColors.ef),
    new Tag(AppTags.mssql, AppColors.mssql),
    new Tag(AppTags.mysql, AppColors.mysql)
  ];
}