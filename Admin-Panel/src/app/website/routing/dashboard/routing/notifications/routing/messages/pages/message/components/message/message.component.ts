import { Component, Input } from '@angular/core';
import { MessageDto } from 'src/app/website/dto';

@Component({
  selector: 'app-dashboard-message-page-message',
  templateUrl: 'message.component.html',
  styleUrls: [ 'message.component.css' ]
})
export class MessageComponent {
  @Input() data: MessageDto;
}
