import { Component } from '@angular/core';
import { Project, Tag } from '../../models';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';

@Component({
  selector: 'app-dashboard-main-portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: ['portfolio.component.css']
})
export class PortfolioComponent {
  projectsCount = 4;
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
    ]),
    new Project(4, 'Smart Bomb', 'assets/proj4.jpg', 'https://github.com/davidkhachatryan123/SmartBomb', [

    ])
  ];

  selectedItems: Array<Project> = [];

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService
  ) { }

  newProject() {

  }

  editProject(id: number = undefined) {

  }

  deleteProject(id: number = undefined) {
    this.deleteDialogService.show(id
      ? [this.selectedItems.find(proj => proj.id == id).title]
      : this.selectedItems.map(proj => proj.title))
    .afterClosed().subscribe((result: boolean) => {
      if(result) {
        console.log('Delete project(s): ', id ? id : this.selectedItems);

        this.showSnackBar('Project(s) was deleted');
      }
    });
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
