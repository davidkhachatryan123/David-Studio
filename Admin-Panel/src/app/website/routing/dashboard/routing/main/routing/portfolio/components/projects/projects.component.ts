import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogService } from 'src/app/shared-module/dashboard/dialogs/delete/services/delete-dialog.service';
import { SetupProjectWizardService } from '../../wizards/services/setup-project-wizard.service';
import { ProjectReadDto } from 'src/app/website/dto';
import { ProjectsService, TopProjectsService } from 'src/app/website/services';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { MatPaginator } from '@angular/material/paginator';
import { PageData } from 'src/app/website/models';

@Component({
  selector: 'app-dashboard-main-portfolio-projects',
  templateUrl: 'projects.component.html',
  styleUrls: ['projects.component.css']
})
export class ProjectsComponent implements AfterViewInit {
  projectsCount: number;
  projects: Array<ProjectReadDto>;
  selectedItems: Array<ProjectReadDto> = [];
  tableOptions: TableOptions = new TableOptions('name', 'asc', 0, 0);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private _snackBar: MatSnackBar,
    private deleteDialogService: DeleteDialogService,
    private setupProjectWizard: SetupProjectWizardService,
    private projectsService: ProjectsService,
    private topProjectsService: TopProjectsService
  ) { }

  ngAfterViewInit() {
    this.onChangeEvent();
    this.paginator.page.subscribe(() => this.onChangeEvent());
  }

  onChangeEvent() {
    this.tableOptions.pageIndex = this.paginator.pageIndex;
    this.tableOptions.pageSize = this.paginator.pageSize;

    this.reloadData();
  }

  reloadData() {
    this.projectsService.getAll(this.tableOptions)
    .subscribe((projects: PageData<ProjectReadDto>) => {
      this.projects = projects.entities;
      this.projectsCount = projects.totalCount;
    });
  }

  newProject() {
    this.setupProjectWizard.show();
  }

  editProject(id: number = undefined) {
    this.setupProjectWizard.show(id ? id : this.selectedItems[0].id);
  }

  deleteProject(id: number = undefined) {
    this.deleteDialogService.show(id
      ? [this.projects.find(proj => proj.id == id).name]
      : this.selectedItems.map(proj => proj.name))
    ?.afterClosed().subscribe((result: boolean) => {
      if(result) {
        if(id) {
          this.projectsService.delete(id).subscribe(_ => this.reloadData());
        } else {
          this.selectedItems.map(
            project => this.projectsService.delete(project.id)
            .subscribe(_ => this.reloadData()));
        }

        this.showSnackBar('Project(s) deleted');
      }
    });
  }

  markAsTop() {
    this.topProjectsService.mark(this.selectedItems).subscribe();
  }

  private showSnackBar(message: string) {
    this._snackBar.open(message, 'Ok', {
      duration: 5000,
    });
  }
}
