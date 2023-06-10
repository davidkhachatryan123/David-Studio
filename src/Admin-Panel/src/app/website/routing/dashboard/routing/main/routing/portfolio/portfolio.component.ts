import { Component, ViewChild } from '@angular/core';
import { ProjectsComponent } from './components/projects/projects.component';
import { MatTabGroup } from '@angular/material/tabs';
import { TagsComponent } from './components/tags/tags.component';
import { TopComponent } from './components/top/top.component';

@Component({
  selector: 'app-dashboard-main-portfolio',
  templateUrl: 'portfolio.component.html'
})
export class PortfolioComponent {
  @ViewChild(ProjectsComponent) projectsComponent: ProjectsComponent;
  @ViewChild(TagsComponent) tagsComponent: TagsComponent;
  @ViewChild(TopComponent) topComponent: TopComponent;

  @ViewChild(MatTabGroup) tabGroup: MatTabGroup;

  action(action: string) {
    switch(action) {
      case 'new':
        this.tabGroup.selectedIndex == 0 ? this.projectsComponent.newProject() :
        this.tabGroup.selectedIndex == 1 ? this.tagsComponent.newTag() : null;
        break;
      case 'edit':
        this.tabGroup.selectedIndex == 0 ? this.projectsComponent.editProject() :
        this.tabGroup.selectedIndex == 1 ? this.tagsComponent.editTag() : null;
        break;
      case 'delete':
        this.tabGroup.selectedIndex == 0 ? this.projectsComponent.deleteProject() :
        this.tabGroup.selectedIndex == 1 ? this.tagsComponent.deleteTag() : null;
        break;
    }
  }
}
