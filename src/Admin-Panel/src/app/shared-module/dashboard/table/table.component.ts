import { AfterViewInit, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';

import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

import { TableButton, TableCellConfiguration, TableImage, TableOptions, TableText } from './models';
import { ServerConfigService } from 'src/app/website/services';

@Component({
  selector: 'app-dashboard-table',
  templateUrl: 'table.component.html',
  styleUrls: ['table.component.css']
})
export class TableComponent implements AfterViewInit {
  @Input() tableConfiguration: Array<TableCellConfiguration>;
  @Input() showSelect = true;

  @Input() data: Array<any> = [];
  @Input() pagesCount = 0;

  @Input() tableOptions = new TableOptions('id', 'asc', 1, 1);
  @Output() tableOptionsChange = new EventEmitter<TableOptions>();

  @Output() selectedRowsChange = new EventEmitter();

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  selection = new SelectionModel(true, []);

  constructor(public serverConfig: ServerConfigService) {}

  ngAfterViewInit() {
    this.onTableOptionsChange();

    this.sort.sortChange.subscribe(() => this.onTableOptionsChange());
    this.paginator.page.subscribe(() => this.onTableOptionsChange());

    this.selection.changed.subscribe(() =>
      this.selectedRowsChange.emit(this.selection.selected));
  }

  private onTableOptionsChange() {
    this.tableOptions.sort = this.sort.active;
    this.tableOptions.sortDirection = this.sort.direction;
    this.tableOptions.pageIndex = this.paginator.pageIndex;
    this.tableOptions.pageSize = this.paginator.pageSize;

    this.tableOptionsChange.emit(this.tableOptions);
  }

  getDisplayedColumns(): string[] {
    const displayedColumns = this.tableConfiguration
    .filter(conf => conf.displayed)
    .map(conf =>
      this.getTableDataElementProperties()[this.tableConfiguration.indexOf(conf)]
    )

    return this.showSelect ? ['select', ...displayedColumns] : displayedColumns;
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.data.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.data);
  }

  checkboxLabel(row?): string {
    if (!row)
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;

    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  getTableDataElementProperties(): string[] {
    return Object.getOwnPropertyNames(this.data[0]);
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