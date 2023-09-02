import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { TagDialogComponent } from '../dialogs/tag/tag-dialog.component';
import { TagCreateDto, TagReadDto } from 'src/app/website/dto';

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
      data: { tag: new TagCreateDto(null, '#00FF00') }
    });

    dialogRef.componentInstance.title = "Create new Tag";
    dialogRef.componentInstance.submitBtnText = "Create";

    return dialogRef;
  }

  showEdit(tags: Array<TagReadDto>) {
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