import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Tag } from '../../../../models';

@Component({
  selector: 'app-dashboard-admin-new',
  templateUrl: 'tag-dialog.component.html'
})
export class TagDialogComponent {
  ngForm: FormGroup;

  @Input() title = '';
  @Input() submitBtnText = '';

  @Output() onSubmit = new EventEmitter<Tag>();

  constructor(
    public dialogRef: MatDialogRef<TagDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data,
  ) {
    this.ngForm = new FormGroup({
      "name": new FormControl(data.tag.name, [Validators.required, Validators.maxLength(32)]),
      "color": new FormControl(data.tag.color, [
        Validators.required, Validators.minLength(7), Validators.maxLength(7),
        Validators.pattern('^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$')
      ])
    });
  }

  onSubmitEvent() {
    if(this.ngForm.valid) {
      this.onSubmit.emit(new Tag(
        this.data.tag.id,
        this.ngForm.controls['name'].value,
        this.ngForm.controls['color'].value
      ));
    }
  }
}