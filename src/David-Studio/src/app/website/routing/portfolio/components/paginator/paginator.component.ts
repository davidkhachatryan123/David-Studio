import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Pagintaion } from '../../models';

@Component({
  selector: 'portfolio-paginator',
  templateUrl: 'paginator.component.html',
  styleUrls: ['paginator.component.css']
})
export class PaginatorComponent implements OnInit {
  @Input() pagination: Pagintaion = new Pagintaion(1, 1);

  @Output() pageChanged = new EventEmitter<number>();

  pagesForDraw: Array<number> = [];

  ngOnInit() {
    this.setPage(1);
  }

  setPage(pageNumber: number) {
    this.pagination.activePage = pageNumber;

    if (this.pagination.totalPages <= 5) {
      this.pagesForDraw = this.range(1, this.pagination.totalPages);
    } else if (pageNumber >= this.pagination.totalPages - 3) {
      this.pagesForDraw = this.range(this.pagination.totalPages - 4, this.pagination.totalPages);
    } else if (pageNumber > 1) {
      this.pagesForDraw = this.range(pageNumber - 1, pageNumber + 1);
    } else if (pageNumber == 1) {
      this.pagesForDraw = this.range(1, 3);
    }

    this.pageChanged.emit(pageNumber);
  }

  range(from: number, to: number): Array<number> {
    return [...Array(to+1).keys()].slice(from);
  }
}