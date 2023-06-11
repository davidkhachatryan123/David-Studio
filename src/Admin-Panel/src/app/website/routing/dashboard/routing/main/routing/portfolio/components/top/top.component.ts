import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Project, Tag } from '../../../../models';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-dashboard-main-portfolio-top',
  templateUrl: 'top.component.html',
  styleUrls: ['top.component.css']
})
export class TopComponent {
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
    ])
  ];

  selectedItems: Array<Project> = [];

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService
  ) { }

  save() {
    
  }

  removeFromTop() {
    this.deleteDialogService.show(this.selectedItems.map(proj => proj.title))
    ?.afterClosed().subscribe((result: boolean) => {
      if(result) {
        console.log('Remove project(s) from TOP: ', this.selectedItems);

        this.showSnackBar('Removed project(s) from TOP');
      }
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.projects, event.previousIndex, event.currentIndex);
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
