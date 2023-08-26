import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from '../../../../models';
import { Answer } from '../../../../models/answer';

@Component({
  selector: 'app-dashboard-message',
  templateUrl: 'message-page.component.html'
})
export class MessagePageComponent {
  returnUrl: string;
  message: Message;
  answer: Answer | null;

  constructor(private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.returnUrl = params['returnUrl'];
    });

    this.message = new Message(1, 'Ashot Khachatryan', 'xashot7@gmail.com', '+37494204803', 'Hello world! How are you? Do you have a good team for developmen? I want to create one very large project and I needed big team with professionals.', true, true, '18-July-2023');
    this.answer = null;
  }

  reloadAnswer() {
    this.answer = new Answer(1, "Hello! Thank you for contacting with us. Yes we have a large and really good team and I think we can work and develop together.", '19-July-2023');
  }
}