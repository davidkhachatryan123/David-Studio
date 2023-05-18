import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';

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
    new Tag('HTML', AppColors.html),
    new Tag('CSS', AppColors.css),
    new Tag('JS', AppColors.js),
    new Tag('TS', AppColors.ts),
    new Tag('Bootstrap', AppColors.bootstrap),
    new Tag('Angular', AppColors.angular)
  ];

  back_circles: Array<Tag> = [
    new Tag('ASP.NET Core', AppColors.aspnet),
    new Tag('C#', AppColors.cs),
    new Tag('EF', AppColors.ef),
    new Tag('MSSQL', AppColors.mssql),
    new Tag('MySQL', AppColors.mysql)
  ];
}