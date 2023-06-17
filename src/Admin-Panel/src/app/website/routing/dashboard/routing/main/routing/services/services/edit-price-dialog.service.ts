import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditPriceDialogComponent } from '../dialogs/edit/edit-price-dialog.component';

@Injectable()
export class EditPriceDialogService {
  constructor(public dialog: MatDialog) { }

  show(price: number) {
    const dialogRef = this.dialog.open(EditPriceDialogComponent, {
      width: '300px',
      disableClose: true,
      data: { price: price }
    });

    return dialogRef;
  }
}