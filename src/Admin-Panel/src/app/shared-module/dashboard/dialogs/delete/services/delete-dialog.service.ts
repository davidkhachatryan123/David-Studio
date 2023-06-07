import { Injectable } from '@angular/core';
import { DeleteDialogComponent } from '../delete.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Injectable()
export class DeleteDialogService {
  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar
  ) { }
  
  show(elements: Array<any>) {
    if(elements.length > 0) {
      const dialogRef = this.dialog.open(DeleteDialogComponent, {
        width: `${elements.length > 1 ? 500 : 250}px`,
        data: { values: elements }
      });
  
      return dialogRef;
    } else {
      this._snackBar.open('Please select minimum one row in table!', 'Ok', {
        duration: 5000
      });
    }

    return undefined;
  }
}