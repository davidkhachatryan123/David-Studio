import { Component } from '@angular/core';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';
import { Tile } from 'src/app/website/shared-module/components/services/tiles/models';

@Component({
  selector: 'services-arduino',
  templateUrl: 'arduino.component.html',
  styleUrls: [ 'arduino.component.css' ]
})
export class ArduinoComponent {
  translateSectionName: string = 'arduino';

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


  arduino_circles: Array<Tag> = [
    new Tag('Arduino', AppColors.arduino),
    new Tag('IoT', AppColors.iot),
    new Tag('PCB', AppColors.pcb)
  ];
}