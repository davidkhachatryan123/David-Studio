import { Component, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dashboard-admin-new',
  templateUrl: 'edit-price-dialog.component.html'
})
export class EditPriceDialogComponent {
  priceFromControl: FormControl;

  constructor(@Inject(MAT_DIALOG_DATA) public data) {
    this.priceFromControl = new FormControl(data.price, [
      Validators.required,
      Validators.maxLength(8),
      Validators.pattern('^[0-9]*$')]
    );
  }
}