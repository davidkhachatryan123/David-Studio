import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ProjectReadDto } from 'src/app/website/dto';

@Component({
  selector: 'app-dashboard-main-portfolio-top-project',
  templateUrl: 'top-project.component.html',
  styleUrls: ['top-project.component.css']
})
export class TopProjectComponent {
  @Input() project: ProjectReadDto;
  @Output() isChecked = new EventEmitter<boolean>();
}