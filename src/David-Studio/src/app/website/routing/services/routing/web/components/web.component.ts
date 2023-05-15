import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';
import { Tile } from 'src/app/website/shared-module/components/services/tiles/models';

@Component({
  selector: 'services-web',
  templateUrl: 'web.component.html',
  styleUrls: [ 'web.component.css' ]
})
export class WebComponent {
  translateSectionName = 'web';

  constructor(
    private title: Title,
    private translate: TranslateService) {

    translate.get(`title.services.${this.translateSectionName}`).subscribe(text => title.setTitle(text));
  }

  starter: Tile = new Tile(
    'Lorem ipsum starter',
    500,
    [
      'Lorem ipsum 1',
      'Lorem ipsum 2',
      'Lorem ipsum 3',
      'Lorem ipsum 4',
      'Lorem ipsum 5'
    ]
  );

  standart: Tile = new Tile(
    'Lorem ipsum standart',
    1000,
    [
      'Lorem ipsum 6',
      'Lorem ipsum 7',
      'Lorem ipsum 8',
      'Lorem ipsum 9',
      'Lorem ipsum 10'
    ]
  );

  pro: Tile = new Tile(
    'Lorem ipsum pro',
    2000,
    [
      'Lorem ipsum 11',
      'Lorem ipsum 12',
      'Lorem ipsum 13',
      'Lorem ipsum 14',
      'Lorem ipsum 15'
    ]
  );


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
    new Tag('MySQL', AppColors.mysql),
    new Tag('MSSQL', AppColors.mssql)
  ];
}