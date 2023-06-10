import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Project } from '../../../../models';

@Component({
  selector: 'app-dashboard-main-portfolio-project',
  templateUrl: 'project.component.html',
  styleUrls: ['project.component.css']
})
export class ProjectComponent {
  @Input() project: Project;

  @Output() isChecked = new EventEmitter<boolean>();

  @Output() onEdit = new EventEmitter();
  @Output() onDelete = new EventEmitter();
}