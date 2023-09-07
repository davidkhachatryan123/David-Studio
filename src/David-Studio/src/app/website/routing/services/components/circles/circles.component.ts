import { Component, Input } from '@angular/core';
import { TagReadDto } from 'src/app/website/dto';
import { Tag } from '../../models';

@Component({
  selector: 'services-circles',
  templateUrl: 'circles.component.html',
  styleUrls: [ 'circles.component.css' ]
})

export class CirclesComponent {
  @Input() title = '';
  @Input() circles: Array<Tag> = [];
}