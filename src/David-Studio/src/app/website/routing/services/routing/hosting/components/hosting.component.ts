import { Component } from '@angular/core';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';
import { Tile } from 'src/app/website/shared-module/components/services/tiles/models';

@Component({
  selector: 'services-hosting',
  templateUrl: 'hosting.component.html',
  styleUrls: [ 'hosting.component.css' ]
})
export class HostingComponent {
  translateSectionName: string = 'hosting';

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


  hosting_circles: Array<Tag> = [
    new Tag('Dedicated', AppColors.dedicated),
    new Tag('Cloud', AppColors.cloud)
  ];
}