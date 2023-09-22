import { Component, Input } from '@angular/core';
import { AnswerDto } from 'src/app/website/dto';

@Component({
  selector: 'app-dashboard-message-page-answer',
  templateUrl: 'answer.component.html',
  styleUrls: [ 'answer.component.css' ]
})
export class AnswerComponent {
  @Input() data: AnswerDto;
}
