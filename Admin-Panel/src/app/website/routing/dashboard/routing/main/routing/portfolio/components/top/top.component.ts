import { AfterViewInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { ProjectReadDto } from 'src/app/website/dto';
import { TopProjectsService } from 'src/app/website/services';

@Component({
  selector: 'app-dashboard-main-portfolio-top',
  templateUrl: 'top.component.html',
  styleUrls: ['top.component.css']
})
export class TopComponent implements AfterViewInit {
  projects: Array<ProjectReadDto>;
  selectedItems: Array<ProjectReadDto> = [];

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    private topProjectsService: TopProjectsService
  ) { }

  ngAfterViewInit() {
    this.reloadData();
  }

  reloadData() {
    this.topProjectsService.getAll()
    .subscribe((projects: Array<ProjectReadDto>) => this.projects = projects);
  }

  save() {
    const projectIds = this.projects.map(project => project.id);

    this.topProjectsService.reorder(projectIds)
    .subscribe(_ => this.reloadData());
  }

  removeFromTop() {
    this.deleteDialogService.show(this.selectedItems.map(proj => proj.name))
    ?.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.selectedItems.map(project =>
          this.topProjectsService.remove(project.id)
          .subscribe(_ => this.reloadData()));

        this.showSnackBar('Removed project(s) from TOP');
      }
    });
  }

  protected drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.projects, event.previousIndex, event.currentIndex);
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
