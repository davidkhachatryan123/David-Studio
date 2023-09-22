import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ContactFormDto } from 'src/app/website/dto';
import { ContactService } from 'src/app/website/services';

@Component({
  selector: 'contact-message',
  templateUrl: 'message.component.html',
  styleUrls: [ 'message.component.css' ]
})
export class MessageComponent {
  isShow = false;
  contactForm: FormGroup;

  constructor(private contactService: ContactService) {
    this.contactForm = new FormGroup({
      "fullName": new FormControl('', [Validators.required, Validators.maxLength(64)]),
      "email": new FormControl('', [Validators.required, Validators.email]),
      "phone": new FormControl('', [Validators.minLength(3), Validators.maxLength(64)]),
      "body": new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(1000)])
    });
  }

  onSubmit() {
    if(this.contactForm.valid) {
      this.contactService.sendMessage(new ContactFormDto(
        this.contactForm.controls['fullName'].value,
        this.contactForm.controls['email'].value,
        this.contactForm.controls['phone'].value,
        this.contactForm.controls['body'].value
      ))
      .subscribe(_ => {
        this.contactForm.reset();

        this.isShow = true;
        setTimeout(_ => {this.isShow = false}, 5000);
      });
    }
  }
}