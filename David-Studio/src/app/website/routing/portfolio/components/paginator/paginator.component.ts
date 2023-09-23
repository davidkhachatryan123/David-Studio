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
  isShow = true;

  ngOnInit() {
    this.changePage(1);
  }

  updatePaginator() {
    if (this.pagination.totalPages <= 5) {
      this.pagesForDraw = this.range(1, this.pagination.totalPages);
    } else if (this.pagination.activePage >= this.pagination.totalPages - 3) {
      this.pagesForDraw = this.range(this.pagination.totalPages - 4, this.pagination.totalPages);
    } else if (this.pagination.activePage > 1) {
      this.pagesForDraw = this.range(this.pagination.activePage - 1, this.pagination.activePage + 1);
    } else if (this.pagination.activePage == 1) {
      this.pagesForDraw = this.range(1, 3);
    }

    if(this.pagesForDraw.length != 0)
      this.isShow = true;
    else
      this.isShow = false;
  }

  setPage(pageNumber: number) {
    this.pagination.activePage = pageNumber;
    this.updatePaginator();
  }

  changePage(pageNumber: number) {
    this.setPage(pageNumber);
    this.pageChanged.emit(this.pagination.activePage);
  }

  private range(from: number, to: number): Array<number> {
    return [...Array(to+1).keys()].slice(from);
  }
}