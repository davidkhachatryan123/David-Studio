import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard-message-page-answer-field',
  templateUrl: 'answer-field.component.html',
  styleUrls: [ 'answer-field.component.css' ]
})
export class AnswerFieldComponent {
  @Input() messageId: number;
  @Output() onSubmit = new EventEmitter();

  messageFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(10),
    Validators.maxLength(500)
  ]);

  submit() {
    if(this.messageFormControl.valid) {
      this.onSubmit.emit();
    }
  }
}
