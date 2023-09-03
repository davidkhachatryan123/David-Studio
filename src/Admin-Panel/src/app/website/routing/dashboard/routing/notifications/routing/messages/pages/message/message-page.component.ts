import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnswerDto, MessageDto } from 'src/app/website/dto';
import { ContactService } from 'src/app/website/services';

@Component({
  selector: 'app-dashboard-message',
  templateUrl: 'message-page.component.html'
})
export class MessagePageComponent {
  returnUrl: string;
  message: MessageDto;
  answer: AnswerDto;

  constructor(
    private route: ActivatedRoute,
    private contactService: ContactService
  ) {
    this.route.queryParams.subscribe(params => {
      this.returnUrl = params['returnUrl'];

      this.contactService.readMessage(params['id'])
      .subscribe((message: MessageDto) => this.message = message);

      this.reloadAnswer(params['id']);
    });
  }

  reloadAnswer(messageId: number) {
    this.contactService.readAnswer(messageId)
      .subscribe((answer: AnswerDto) => this.answer = answer);
  }
}