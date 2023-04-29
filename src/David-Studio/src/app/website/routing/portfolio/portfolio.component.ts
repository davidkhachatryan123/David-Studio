import { Component } from '@angular/core';
import { Pagintaion, Project, Tag } from './models';
import { AppColors } from '../../consts';

@Component({
  selector: 'portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: [ 'portfolio.component.css' ]
})
export class PortfolioComponent {
  translateSectionName: string = 'portfolio';

  projects: Array<Project> = [
    new Project('Smart Home', [
      new Tag('C#', AppColors.cs),
      new Tag('Arduino', AppColors.arduino),
      new Tag('IoT', AppColors.iot),
      new Tag('PCB', AppColors.pcb),
      new Tag('MySQL', AppColors.mysql)
    ], 'assets/img/Projects/proj1.jpg', ''),
    new Project('David Studio', [
      new Tag('HTML', AppColors.html),
      new Tag('CSS', AppColors.css),
      new Tag('Bootstrap', AppColors.bootstrap),
      new Tag('JS', AppColors.js),
      new Tag('TS', AppColors.ts),
      new Tag('Angular', AppColors.angular),
      new Tag('C#', AppColors.cs),
      new Tag('ASP.NET Core', AppColors.aspnet),
      new Tag('MSSQL', AppColors.mssql),
    ], 'assets/img/Projects/proj2.jpg', ''),
    new Project('Text Analyzer', [
      new Tag('C++', AppColors.cpp),
      new Tag('Bash', AppColors.bash),
    ], 'assets/img/Projects/proj3.jpg', ''),
  ];


  pagination: Pagintaion = new Pagintaion(1, 22);

  changePage($event: number) {
    console.log(`Page are chnaged: ${$event}`);
  }
}
