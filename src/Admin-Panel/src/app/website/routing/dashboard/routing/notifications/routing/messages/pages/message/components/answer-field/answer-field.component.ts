import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AnswerCreateDto } from 'src/app/website/dto';
import { ContactService } from 'src/app/website/services';

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

  constructor(private contactService: ContactService) { }

  submit() {
    if(this.messageFormControl.valid) {
      this.contactService.answer(
        this.messageId,
        new AnswerCreateDto(this.messageFormControl.value)
      )
      .subscribe(_ => this.onSubmit.emit());
    }
  }
}
