import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { TagDialogComponent } from '../dialogs/tag/tag-dialog.component';
import { Tag } from '../../../models';

@Injectable()
export class TagDialogService {
  constructor(
    public dialog: MatDialog,
    private _snackBar: MatSnackBar
  ) { }

  showNew() {
    const dialogRef = this.dialog.open(TagDialogComponent, {
      width: '500px',
      disableClose: true,
      data: { tag: new Tag() }
    });

    dialogRef.componentInstance.title = "Create new Tag";
    dialogRef.componentInstance.submitBtnText = "Create";

    return dialogRef;
  }

  showEdit(tags: Array<Tag>) {
    if(tags.length == 1) {
      const dialogRef = this.dialog.open(TagDialogComponent, {
        width: '500px',
        disableClose: true,
        data: { tag: tags[0] }
      });
  
      dialogRef.componentInstance.title = "Edit Tag";
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