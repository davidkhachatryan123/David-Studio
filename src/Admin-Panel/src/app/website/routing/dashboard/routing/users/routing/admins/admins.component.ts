import { Component } from '@angular/core';

import { MatSnackBar } from '@angular/material/snack-bar';

import { TableButton, TableCellConfiguration, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { Admin } from '../../models';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from 'src/app/shared-module/dashboard';
import { EntityDialogComponent } from './components/entity-dialog.component';
import { AdminDto } from 'src/app/website/dto/admin-dto';

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
  ) { }

  tableOptionsChanged($event: TableOptions) {
    this.tableOptions = $event;
  }

  onSendConfirmationEmailClick(id: number | string) {
    console.log(`SendConfirmationEmail: ${id}`);

    this.showSnackBar('Email confirm requested!');
  }

  newAdmin() {
    const dialogRef = this.dialog.open(EntityDialogComponent, {
      width: '500px',
      disableClose: true,
      data: { user: new AdminDto() }
    });

    dialogRef.componentInstance.title = "Create new Admin";
    dialogRef.componentInstance.submitBtnText = "Create";

    dialogRef.componentInstance.onSubmit.subscribe((admin: AdminDto) => {
      console.log('Create admin: ', admin);

      dialogRef.close();

      this.showSnackBar('User created successfully!');
    });
  }

  editAdmin() {
    if(this.selectedRows.length == 1) {
      const dialogRef = this.dialog.open(EntityDialogComponent, {
        width: '500px',
        disableClose: true,
        data: { user: this.selectedRows[0] }
      });
  
      dialogRef.componentInstance.title = "Edit Admin";
      dialogRef.componentInstance.submitBtnText = "Edit";
  
      dialogRef.componentInstance.onSubmit.subscribe((admin: AdminDto) => {
        console.log('Edit admin: ', admin);

        dialogRef.close();

        this.showSnackBar('User edited successfully!');
      });
    } else
      this.showSnackBar('Please select one row in table!');
  }

  deleteItems() {
    if(this.selectedRows.length > 0) {
      const dialogRef = this.dialog.open(DeleteDialogComponent, {
        width: `${this.selectedRows.length > 1 ? 500 : 250}px`,
        data: { values: this.selectedRows.map(row => row.username) }
      });
  
      dialogRef.afterClosed().subscribe((result: boolean) => {
        if(result) {
          console.log(this.selectedRows);

          this.showSnackBar('User(s) deleted!');
        }
      });
    } else
    this.showSnackBar('Please select minimum one row in table!');
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
