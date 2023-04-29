import { Component, Input } from '@angular/core';
import { Project } from '../../models/project';

@Component({
  selector: 'portfolio-project',
  templateUrl: 'project.component.html',
  styleUrls: [ 'project.component.css' ]
})
export class PortfolioProjectComponent {
  @Input() projectModel: Project = new Project('', [], '', '');
}