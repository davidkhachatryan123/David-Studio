import { Component, Input } from '@angular/core';
import { Answer } from '../../../../../../models/answer';

@Component({
  selector: 'app-dashboard-message-page-answer',
  templateUrl: 'answer.component.html',
  styleUrls: [ 'answer.component.css' ]
})
export class AnswerComponent {
  @Input() data: Answer;
}
