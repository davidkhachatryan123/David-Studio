import { AfterViewChecked, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { ProjectsComponent } from './components/projects/projects.component';
import { MatTabGroup } from '@angular/material/tabs';
import { TagsComponent } from './components/tags/tags.component';
import { TopComponent } from './components/top/top.component';

@Component({
  selector: 'app-dashboard-main-portfolio',
  templateUrl: 'portfolio.component.html'
})
export class PortfolioComponent implements AfterViewChecked {
  @ViewChild(ProjectsComponent) projectsComponent: ProjectsComponent;
  @ViewChild(TagsComponent) tagsComponent: TagsComponent;
  @ViewChild(TopComponent) topComponent: TopComponent;

  @ViewChild(MatTabGroup) tabGroup: MatTabGroup;

  constructor(private cdRef : ChangeDetectorRef) { }

  ngAfterViewChecked() {
    this.cdRef.detectChanges();
  }
}
