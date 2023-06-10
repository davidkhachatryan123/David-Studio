import { Component, Input } from '@angular/core';
import { Project } from '../../../../models';

@Component({
  selector: 'app-dashboard-main-portfolio-project',
  templateUrl: 'project.component.html',
  styleUrls: ['project.component.css']
})
export class ProjectComponent {
  @Input() project: Project;
}