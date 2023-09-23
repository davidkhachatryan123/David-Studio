import { Component, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';

import { TableButton, TableCellConfiguration, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { EntityDialogService } from './services/entity-dialog.service';
import { PageData } from 'src/app/website/models';
import { AdminReadDto, ConfirmEmailRequestDto } from 'src/app/website/dto';
import { AdminsService, ManageUsersService } from 'src/app/website/services';
import { TableComponent } from 'src/app/shared-module/dashboard';

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
      new TableButton(
        'Send Confirmation Email',
        value => !value,
        id => this.onSendConfirmationEmailClick(id.toString())
      ),
      'Email Confirmed',
      true
    ),
    new TableCellConfiguration(
      new TableText(),
      'Phone Number',
      true
    )
  ];

  admins: PageData<AdminReadDto> = new PageData<AdminReadDto>([], 0);
  tableOptions = new TableOptions('username', 'asc', 1, 30);
  selectedRows: Array<AdminReadDto> = [];

  @ViewChild(TableComponent) table: TableComponent;

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    private entityDialogService: EntityDialogService,
    private adminsService: AdminsService,
    private manageUsersService: ManageUsersService
  ) { }

  reloadData() {
    this.adminsService.getAll(this.tableOptions)
    .subscribe((admins: PageData<AdminReadDto>) => this.admins = admins);

    this.table.resetSeletions();
  }

  tableOptionsChanged($event: TableOptions) {
    this.tableOptions = $event;
    this.reloadData();
  }

  onSendConfirmationEmailClick(id: string) {
    this.manageUsersService.sendConfirmationEmail(
      new ConfirmEmailRequestDto(id, window.location.origin)
    ).subscribe(
      _ => {
        this.showSnackBar('Email confirmation requested!');
        this.reloadData();
      },
      (error: HttpErrorResponse) => {
        this.showSnackBar('Error has occurred!');
      }
    );
  }

  newAdmin() {
    const dialogRef = this.entityDialogService.showNew();

    dialogRef.componentInstance.onSubmit.subscribe(({ id, user }) => {
      this.adminsService.create(user)
        .subscribe(
          _ => {
            this.reloadData();
            this.showSnackBar('Admin created successfully!');
            dialogRef.close();
          },
          (error: HttpErrorResponse) => {
            this.showSnackBar('Unknown error has occurred!');
          }
        );
    });
  }

  editAdmin() {
    const dialogRef = this.entityDialogService.showEdit(this.selectedRows);

    if(dialogRef) {
      dialogRef.componentInstance.onSubmit.subscribe(({ id, user }) => {
        this.adminsService.update(id, user).subscribe(
          _ => {
            this.reloadData();
            this.showSnackBar('Admin edited successfully!');
            dialogRef.close();
          },
          (error: HttpErrorResponse) => {
            this.showSnackBar('Unknown error has occurred!');
          }
        );
      });
    }
  }

  deleteItems() {
    this.deleteDialogService.show(this.selectedRows.map(row => row.username))
    ?.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.selectedRows.map(user => this.adminsService.delete(user.id)
        .subscribe(_ => this.reloadData()));

        this.showSnackBar('User(s) deleted');
      }
    });
  }

  private showSnackBar(message: string, duration: number = 5000) {
    this._snackBar.open(message, 'Ok', {
      duration: duration,
    });
  }
}
