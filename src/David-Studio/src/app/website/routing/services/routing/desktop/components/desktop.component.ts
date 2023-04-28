import { Component } from '@angular/core';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';
import { Tile } from 'src/app/website/shared-module/components/services/tiles/models';

@Component({
  selector: 'services-desktop',
  templateUrl: 'desktop.component.html',
  styleUrls: [ 'desktop.component.css' ]
})
export class DesktopComponent {
  translateSectionName: string = 'desktop';

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


  windows_circles: Array<Tag> = [
    new Tag('C#', AppColors.cs),
    new Tag('WPF', AppColors.wpf),
    new Tag('Win Forms', AppColors.winforms),
    new Tag('C++', AppColors.cpp)
  ];

  linux_circles: Array<Tag> = [
    new Tag('C++', AppColors.cpp),
    new Tag('Bash Script', AppColors.bash),
  ];
}