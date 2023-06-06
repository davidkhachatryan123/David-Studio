import { Component, Inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dashboard-delete-dialog',
  templateUrl: 'delete.component.html'
})
export class DeleteDialogComponent {
  deleteConfirmFormControl = new FormControl('', [Validators.required, Validators.pattern('delete')]);

  constructor(@Inject(MAT_DIALOG_DATA) public data) { }
}
