import { Component, EventEmitter, Output } from '@angular/core';
import { TagReadDto } from 'src/app/website/dto';

@Component({
  selector: 'portfolio-filter-tag',
  templateUrl: 'filter-tag.component.html',
  styleUrls: [ 'filter-tag.component.css' ]
})
export class FilterTagComponent {
  @Output() update = new EventEmitter<Array<TagReadDto>>;

  tags: Array<TagReadDto> = [];

  xOnClick(indexOfTag: number) {
    this.tags.splice(indexOfTag, 1);
    this.update.emit(this.tags);
  }
}