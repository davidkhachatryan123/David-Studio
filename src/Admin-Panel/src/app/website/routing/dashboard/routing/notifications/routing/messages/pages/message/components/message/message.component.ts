import { Component, Input } from '@angular/core';
import { Message } from '../../../../../../models';

@Component({
  selector: 'app-dashboard-message-page-message',
  templateUrl: 'message.component.html',
  styleUrls: [ 'message.component.css' ]
})
export class MessageComponent {
  @Input() data: Message;
}
