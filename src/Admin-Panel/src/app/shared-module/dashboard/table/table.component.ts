import { AfterViewInit, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';

import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

import { TableButton, TableConfiguration, TableImage, TableOptions, TableText } from './models';
import { ServerConfigService } from 'src/app/website/services';

@Component({
  selector: 'app-dashboard-table',
  templateUrl: 'table.component.html',
  styleUrls: ['table.component.css']
})
export class TableComponent implements AfterViewInit {
  @Input() tableConfiguration: Array<TableConfiguration> = [];

  @Input() data: Array<any> = [];
  @Input() pagesCount = 0;

  @Input() tableOptions = new TableOptions('id', 'asc', 1, 1);
  @Output() tableOptionsChange = new EventEmitter<TableOptions>();

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(public serverConfig: ServerConfigService) {}

  ngAfterViewInit() {
    this.onTableOptionsChange();

    this.sort.sortChange.subscribe(() => this.onTableOptionsChange());
    this.paginator.page.subscribe(() => this.onTableOptionsChange());
  }

  private onTableOptionsChange() {
    this.tableOptions.sort = this.sort.active;
    this.tableOptions.sortDirection = this.sort.direction;
    this.tableOptions.pageIndex = this.paginator.pageIndex;
    this.tableOptions.pageSize = this.paginator.pageSize;

    this.tableOptionsChange.emit(this.tableOptions);

    console.log(this.tableOptions);
  }

  getDisplayedColumns(): Array<string> {
    return this.tableConfiguration
    .filter(conf => conf.displayed == true)
    .map(conf => conf.name);
  }

  isTypeOf(obj: TableText | TableButton | TableImage, type: string): boolean {
    return type === 'TableText' ? obj instanceof TableText :
           type === 'TableButton' ? obj instanceof TableButton :
           type === 'TableImage' ? obj instanceof TableImage :
           false;
  }

  getTableButton(obj: TableText | TableButton | TableImage) {
    return obj as TableButton;
  }
}