import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { EntityDialogComponent } from '../components/entity-dialog.component';
import { AdminDto } from 'src/app/website/dto/admin-dto';

@Injectable()
export class EntityDialogService {
  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar
  ) { }

  showNew() {
    const dialogRef = this.dialog.open(EntityDialogComponent, {
      width: '500px',
      disableClose: true,
      data: { user: new AdminDto() }
    });

    dialogRef.componentInstance.title = "Create new Admin";
    dialogRef.componentInstance.submitBtnText = "Create";

    return dialogRef;
  }

  showEdit(admins: Array<any>) {
    if(admins.length == 1) {
      const dialogRef = this.dialog.open(EntityDialogComponent, {
        width: '500px',
        disableClose: true,
        data: { user: admins[0] }
      });
  
      dialogRef.componentInstance.title = "Edit Admin";
      dialogRef.componentInstance.submitBtnText = "Edit";

      return dialogRef;
    } else {
      this._snackBar.open('Please select one row in table!', 'Ok', {
        duration: 5000,
      });
    }

    return undefined;
  }
}