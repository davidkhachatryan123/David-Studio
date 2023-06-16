import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ImageCropDialogComponent } from '../dialogs/image-crop/image-crop-dialog.component';

@Injectable()
export class ImageCropDialogService {
  constructor(public dialog: MatDialog) { }

  show(image) {
    const dialogRef = this.dialog.open(ImageCropDialogComponent, {
      width: '800px',
      disableClose: true,
      data: { image: image }
    });

    return dialogRef;
  }
}