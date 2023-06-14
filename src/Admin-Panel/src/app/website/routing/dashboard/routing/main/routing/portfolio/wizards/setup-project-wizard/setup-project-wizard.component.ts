import { Component } from '@angular/core';
import { Project, Tag } from '../../../../models';

@Component({
  selector: 'app-dashboard-main-portfolio-setup-project-wizard',
  templateUrl: 'setup-project-wizard.component.html',
  styleUrls: ['setup-project-wizard.component.css']
})
export class SetupProjectWizardComponent {
 project: Project;

  constructor() {
    this.project = new Project(1, 'David-Studio', 'assets/proj1.jpg', '', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')]
    );
  }
}