import { Component, Input } from '@angular/core';
import { Pagintaion } from '../../models';

@Component({
  selector: 'portfolio-paginator',
  templateUrl: 'paginator.component.html',
  styleUrls: ['paginator.component.css']
})
export class PaginatorComponent {
  @Input() pagination: Pagintaion=new Pagintaion(
    1, 10
  );

  pagesForDraw: Array<number> = this.range(1, 3);

  range(from: number, to: number): Array<number> {
    return [...Array(to + 1).keys()].slice(from);
  }
}