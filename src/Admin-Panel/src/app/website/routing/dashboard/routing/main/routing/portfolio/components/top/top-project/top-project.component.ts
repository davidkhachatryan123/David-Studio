import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Project } from '../../../../../models';

@Component({
  selector: 'app-dashboard-main-portfolio-top-project',
  templateUrl: 'top-project.component.html',
  styleUrls: ['top-project.component.css']
})
export class TopProjectComponent {
  @Input() project: Project = new Project();

  @Output() isChecked = new EventEmitter<boolean>();
}