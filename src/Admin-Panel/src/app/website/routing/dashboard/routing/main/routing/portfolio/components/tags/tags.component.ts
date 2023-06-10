import { Component } from '@angular/core';
import { TableCellConfiguration, TableColor, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { Tag } from '../../../../models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';

@Component({
  selector: 'app-dashboard-main-portfolio-tags',
  templateUrl: 'tags.component.html'
})
export class TagsComponent {
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

  data: Array<Tag> = [
    new Tag(1, 'C#', '#8d3aa3'),
    new Tag(2, 'ASP.NET Core', '#6c429c'),
    new Tag(3, 'Angular', '#e23237')
  ];

  tableOptions = new TableOptions('name', 'asc', 1, 1);

  selectedRows: Array<Tag> = [];

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    // private entityDialogService: EntityDialogService
  ) { }

  tableOptionsChanged($event: TableOptions) {
    this.tableOptions = $event;
  }

  newTag() {
    // const dialogRef = this.entityDialogService.showNew();

    // dialogRef.componentInstance.onSubmit.subscribe((admin: AdminDto) => {
    //   console.log('Create admin: ', admin);

    //   dialogRef.close();
    //   this.showSnackBar('Admin created successfully!');
    // });
  }

  editTag() {
    // const dialogRef = this.entityDialogService.showEdit(this.selectedRows);

    // if(dialogRef) {
    //   dialogRef.componentInstance.onSubmit.subscribe((admin: AdminDto) => {
    //     console.log('Edit admin: ', admin);

    //     dialogRef.close();
    //     this.showSnackBar('Admin edited successfully!');
    //   });
    // }
  }

  deleteTag() {
    this.deleteDialogService.show(this.selectedRows.map(row => row.name))
    .afterClosed().subscribe((result: boolean) => {
      if(result) {
        console.log('Delete tag(s): ', this.selectedRows);

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
