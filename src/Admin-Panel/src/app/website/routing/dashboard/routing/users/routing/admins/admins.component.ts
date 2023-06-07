import { Component } from '@angular/core';

import { MatSnackBar } from '@angular/material/snack-bar';

import { TableButton, TableCellConfiguration, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { Admin } from '../../models';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from 'src/app/shared-module/dashboard';
import { EntityDialogComponent } from './components/entity-dialog.component';
import { AdminDto } from 'src/app/website/dto/admin-dto';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { EntityDialogService } from './services/entity-dialog.service';

@Component({
  selector: 'app-dashboard-main-admins',
  templateUrl: 'admins.component.html'
})
export class AdminsComponent {
  tableConfiguration: Array<TableCellConfiguration> = [
    new TableCellConfiguration(
      new TableText(),
      'Id',
      false
    ),
    new TableCellConfiguration(
      new TableText(),
      'Username',
      true
    ),
    new TableCellConfiguration(
      new TableText(),
      'Email',
      true
    ),
    new TableCellConfiguration(
      new TableButton('Send Confirmation Email', value => !value, id => this.onSendConfirmationEmailClick(id)),
      'Email Confirmed',
      true
    ),
    new TableCellConfiguration(
      new TableText(),
      'Phone Number',
      true
    )
  ];

  data: Array<Admin> = [
    new Admin('1', 'david', 'xdavit7@gmail.com', true, '+374 41 21-48-03'),
    new Admin('2', 'hayk', 'xhayk7@gmail.com', false, null)
  ];

  tableOptions = new TableOptions('username', 'asc', 1, 1);

  selectedRows: Array<Admin> = [];

  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    private entityDialogService: EntityDialogService
  ) { }

  tableOptionsChanged($event: TableOptions) {
    this.tableOptions = $event;
  }

  onSendConfirmationEmailClick(id: number | string) {
    console.log(`SendConfirmationEmail: ${id}`);

    this.showSnackBar('Email confirmation requested!');
  }

  newAdmin() {
    const dialogRef = this.entityDialogService.showNew();

    dialogRef.componentInstance.onSubmit.subscribe((admin: AdminDto) => {
      console.log('Create admin: ', admin);

      dialogRef.close();
      this.showSnackBar('Admin created successfully!');
    });
  }

  editAdmin() {
    const dialogRef = this.entityDialogService.showEdit(this.selectedRows);

    if(dialogRef) {
      dialogRef.componentInstance.onSubmit.subscribe((admin: AdminDto) => {
        console.log('Edit admin: ', admin);

        dialogRef.close();
        this.showSnackBar('Admin edited successfully!');
      });
    }
  }

  deleteItems() {
    this.deleteDialogService.show(this.selectedRows.map(row => row.username))
    .afterClosed().subscribe((result: boolean) => {
      if(result) {
        console.log('Delete admin(s): ', this.selectedRows);

        this.showSnackBar('Admin(s) deleted');
      }
    });
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
