import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { TableCellConfiguration, TableColor, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { TagDialogService } from '../../services/tag-dialog.service';
import { TagReadDto } from 'src/app/website/dto';
import { TagsService } from 'src/app/website/services';
import { PageData } from 'src/app/website/models';
import { TableComponent } from 'src/app/shared-module/dashboard';

@Component({
  selector: 'app-dashboard-main-portfolio-tags',
  templateUrl: 'tags.component.html'
})
export class TagsComponent implements AfterViewInit {
  tableConfiguration: Array<TableCellConfiguration> = [
    new TableCellConfiguration(
      new TableText(),
      'Id',
      false
    ),
    new TableCellConfiguration(
      new TableText(),
      'Name',
      true
    ),
    new TableCellConfiguration(
      new TableColor(),
      'Color',
      true
    )
  ];

  data: PageData<TagReadDto> = new PageData<TagReadDto>([], 0);
  selectedRows: Array<TagReadDto> = [];
  tableOptions = new TableOptions('name', 'asc', 1, 30);

  @ViewChild(TableComponent) table: TableComponent; 

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    private tagDialogService: TagDialogService,
    private tagsService: TagsService
  ) { }

  ngAfterViewInit() {
    this.reloadData();
  }

  reloadData() {
    this.tagsService.getAll(this.tableOptions)
    .subscribe((tags: PageData<TagReadDto>) => this.data = tags);

    this.table.resetSeletions();
  }

  tableOptionsChanged($event: TableOptions) {
    this.tableOptions = $event;
    this.reloadData();
  }

  newTag() {
    const dialogRef = this.tagDialogService.showNew();

    dialogRef.componentInstance.onSubmit.subscribe(({ id, tag }) => {
      this.tagsService.create(tag).subscribe(_ => this.reloadData());

      dialogRef.close();
      this.showSnackBar('Tag created successfully!');
    });
  }

  editTag() {
    const dialogRef = this.tagDialogService.showEdit(this.selectedRows);

    if(dialogRef) {
      dialogRef.componentInstance.onSubmit.subscribe(({ id, tag }) => {
        this.tagsService.update(id, tag).subscribe(_ => this.reloadData());

        dialogRef.close();
        this.showSnackBar('Tag edited successfully!');
      });
    }
  }

  deleteTag() {
    this.deleteDialogService.show(this.selectedRows.map(row => row.name))
    ?.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.selectedRows.map(tag => this.tagsService.delete(tag.id)
        .subscribe(_ => this.reloadData()));

        this.showSnackBar('Tag(s) deleted');
      }
    });
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
