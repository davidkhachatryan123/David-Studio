import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AdminDto } from 'src/app/website/dto/admin-dto';

@Component({
  selector: 'app-dashboard-admin-new',
  templateUrl: 'entity-dialog.component.html'
})
export class EntityDialogComponent {
  ngForm: FormGroup;

  @Input() title: string = "";
  @Input() submitBtnText: string = "";

  @Output() onSubmit = new EventEmitter<any>();

  constructor(
    public dialogRef: MatDialogRef<EntityDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.ngForm = new FormGroup({
      "username": new FormControl(data.user.username, [
        Validators.required, Validators.maxLength(16),
        Validators.pattern('(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$')
      ]),
      "password": new FormControl('', [
        Validators.required, Validators.minLength(8), Validators.maxLength(64),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[;@$!%*?&=#])[A-Za-z0-9@$!%*?&=#]+$')
      ]),
      "confirmPassword": new FormControl('', [
        Validators.required, Validators.minLength(8), Validators.maxLength(64),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[;@$!%*?&=#])[A-Za-z0-9@$!%*?&=#]+$')
      ]),
      "email": new FormControl(data.user.email, [
        Validators.required, Validators.email
      ]),
      "phoneNumber": new FormControl(data.user.phoneNumber)
    });
  }

  onSubmitEvent() {
    if(this.ngForm.valid) {
      this.onSubmit.emit({
        id: this.data.user.id,
        user: new AdminDto(
          this.data.user.id,
          this.ngForm.controls['username'].value,
          this.ngForm.controls['password'].value,
          this.ngForm.controls['confirmPassword'].value,
          this.ngForm.controls['email'].value,
          this.ngForm.controls['phoneNumber'].value
        )
      });
    }
  }
}