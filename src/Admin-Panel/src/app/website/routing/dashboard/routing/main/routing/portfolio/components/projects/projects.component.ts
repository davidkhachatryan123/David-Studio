import { Component } from '@angular/core';
import { Project, Tag } from '../../../../models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { SetupProjectWizardService } from '../../wizards/services/setup-project-wizard.service';

@Component({
  selector: 'app-dashboard-main-portfolio-projects',
  templateUrl: 'projects.component.html',
  styleUrls: ['projects.component.css']
})
export class ProjectsComponent {
  projectsCount = 5;
  projects: Array<Project> = [
    new Project(1, 'David Studio', 'assets/proj1.jpg', 'https://github.com/davidkhachatryan123/David-Studio', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')
    ]),
    new Project(2, 'Study Control Software', 'assets/proj2.jpg', 'https://github.com/davidkhachatryan123/Study-Control-Software', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')
    ]),
    new Project(3, 'Customs Clearance Car', 'assets/proj3.jpg', 'https://github.com/davidkhachatryan123/Customs-Clearance-Car', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')
    ]),
    new Project(4, 'Smart Bomb', 'assets/proj4.jpg', 'https://github.com/davidkhachatryan123/SmartBomb', [

    ]),
    new Project(5, 'Test', 'assets/proj3.jpg', 'https://github.com/davidkhachatryan123/Customs-Clearance-Car', [
      new Tag(1, 'C#', '#8d3aa3'),
      new Tag(2, 'ASP.NET Core', '#6c429c'),
      new Tag(3, 'Angular', '#e23237')
    ])
  ];

  selectedItems: Array<Project> = [];

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    private setupProjectWizard: SetupProjectWizardService
  ) { }

  newProject() {
    this.setupProjectWizard.show();
  }

  editProject(id: number = undefined) {
    this.setupProjectWizard.show(id ? id : this.selectedItems[0].id);
  }

  deleteProject(id: number = undefined) {
    this.deleteDialogService.show(id
      ? [this.projects.find(proj => proj.id == id).title]
      : this.selectedItems.map(proj => proj.title))
    ?.afterClosed().subscribe((result: boolean) => {
      if(result) {
        console.log('Delete project(s): ', id ? id : this.selectedItems);

        this.showSnackBar('Project(s) was deleted');
      }
    });
  }

  markAsTop() {
    
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
