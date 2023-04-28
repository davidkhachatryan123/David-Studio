import { Component, Input } from '@angular/core';
import { Tag } from 'src/app/website/routing/portfolio/models';

@Component({
  selector: 'services-circles',
  templateUrl: 'circles.component.html',
  styleUrls: [ 'circles.component.css' ]
})

export class CirclesComponent {
  @Input() title: string = "";
  @Input() circles: Array<Tag> = [];
}