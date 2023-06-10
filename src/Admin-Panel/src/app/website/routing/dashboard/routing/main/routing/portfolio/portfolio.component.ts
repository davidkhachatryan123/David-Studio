import { Component, ViewChild } from '@angular/core';
import { ProjectsComponent } from './components/projects/projects.component';
import { MatTabGroup } from '@angular/material/tabs';
import { TagsComponent } from './components/tags/tags.component';

@Component({
  selector: 'app-dashboard-main-portfolio',
  templateUrl: 'portfolio.component.html'
})
export class PortfolioComponent {
  @ViewChild(ProjectsComponent) projectsComponent: ProjectsComponent;
  @ViewChild(TagsComponent) tagsComponent: TagsComponent;

  @ViewChild(MatTabGroup) tabGroup: MatTabGroup;

  action(action: string) {
    switch(action) {
      case 'new':
        this.tabGroup.selectedIndex == 0 ? this.projectsComponent.newProject() : null;
        break;
      case 'edit':
        this.tabGroup.selectedIndex == 0 ? this.projectsComponent.editProject() : null;
        break;
      case 'delete':
        this.tabGroup.selectedIndex == 0 ? this.projectsComponent.deleteProject() : null;
        break;
    }
  }
}
