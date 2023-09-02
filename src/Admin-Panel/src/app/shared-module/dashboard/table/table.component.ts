import { AfterViewInit, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';

import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

import { TableButton, TableCellConfiguration, TableColor, TableImage, TableOptions, TableText } from './models';

@Component({
  selector: 'app-dashboard-table',
  templateUrl: 'table.component.html',
  styleUrls: ['table.component.css']
})
export class TableComponent implements AfterViewInit {
  @Input() tableConfiguration: Array<TableCellConfiguration>;
  @Input() showSelect = true;

  @Input() data: Array<any> = [];
  @Input() totalCount = 0;

  @Input() tableOptions = new TableOptions('id', 'asc', 1, 1);
  @Output() tableOptionsChange = new EventEmitter<TableOptions>();
  
  @Output() selectedRowsChange = new EventEmitter();

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  selection = new SelectionModel(true, []);

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
    .filter(cols => cols);

    return this.showSelect && this.data.length ? ['select', ...displayedColumns] : displayedColumns;
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

  resetSeletions() {
    this.selection.clear();
  }

  protected checkboxLabel(row?): string {
    if (!row)
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;

    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  protected getTableDataElementProperties(): string[] {
    if(this.data.length != 0)
      return Object.getOwnPropertyNames(this.data[0]);
    else
      return [];
  }

  protected isTypeOf(obj: TableText | TableText | TableButton | TableImage, type: string): boolean {
    return type === 'TableText' ? obj instanceof TableText :
           type === 'TableButton' ? obj instanceof TableButton :
           type === 'TableImage' ? obj instanceof TableImage :
           type === 'TableColor' ? obj instanceof TableColor :
           false;
  }

  protected getTableButton(obj) {
    return obj as TableButton;
  }
}