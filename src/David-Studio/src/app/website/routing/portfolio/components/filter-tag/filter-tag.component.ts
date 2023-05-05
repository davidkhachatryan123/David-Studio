import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Tag } from '../../models';
import { AppColors } from 'src/app/website/consts';

@Component({
  selector: 'portfolio-filter-tag',
  templateUrl: 'filter-tag.component.html',
  styleUrls: [ 'filter-tag.component.css' ]
})
export class FilterTagComponent {
  @Output() update = new EventEmitter<Array<Tag>>;

  tags: Array<Tag> = [];

  xOnClick(indexOfTag: number) {
    this.tags.splice(indexOfTag, 1);
    this.update.emit(this.tags);
  }
}