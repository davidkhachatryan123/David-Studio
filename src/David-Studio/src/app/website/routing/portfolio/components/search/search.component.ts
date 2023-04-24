import { Component } from '@angular/core';
import { Tag } from 'src/app/website/routing/portfolio/models';

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: [ 'search.component.css' ]
})
export class SearchComponent {
  tags: Array<Tag> = [
    
  ];
}