import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ProjectReadDto } from 'src/app/website/dto';

@Component({
  selector: 'app-dashboard-main-portfolio-projects-project',
  templateUrl: 'project.component.html',
  styleUrls: ['project.component.css']
})
export class ProjectComponent {
  @Input() project: ProjectReadDto;

  @Output() isChecked = new EventEmitter<boolean>();

  @Output() onEdit = new EventEmitter();
  @Output() onDelete = new EventEmitter();
}