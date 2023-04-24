import { Component, Input } from '@angular/core';
import { Tile } from './models';

@Component({
  selector: 'services-tiles',
  templateUrl: 'tiles.component.html',
  styleUrls: [ 'tiles.component.css' ]
})

export class TilesComponent {
  @Input() starter: Tile;
  @Input() standart: Tile;
  @Input() pro: Tile;
}