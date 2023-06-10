import { Component } from '@angular/core';
import { Project, Tag } from '../../models';

@Component({
  selector: 'app-dashboard-main-portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: ['portfolio.component.css']
})
export class PortfolioComponent {
  projects: Array<Project> = [
    new Project(1, 'David Studio', 'assets/proj1.jpg', 'https://github.com/davidkhachatryan123/David-Studio', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(1, 'ASP.NET Core', '#6c429c'),
      new Tag(1, 'Angular', '#e23237')
    ]),
    new Project(2, 'Study Control Software', 'assets/proj2.jpg', 'https://github.com/davidkhachatryan123/Study-Control-Software', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(1, 'ASP.NET Core', '#6c429c'),
      new Tag(1, 'Angular', '#e23237')
    ]),
    new Project(3, 'Customs Clearance Car', 'assets/proj3.jpg', 'https://github.com/davidkhachatryan123/Customs-Clearance-Car', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(1, 'ASP.NET Core', '#6c429c'),
      new Tag(1, 'Angular', '#e23237')
    ])
  ];

  projectsCount = 3;

  newProject() {

  }

  editProject() {

  }

  deleteProject() {

  }
}
