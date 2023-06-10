import { NgModule } from '@angular/core';

import { PortfolioComponent } from './portfolio.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ProjectComponent } from './components/projects/project/project.component';
import { CommonModule } from '@angular/common';
import { TagsComponent } from './components/tags/tags.component';
import { ProjectsComponent } from './components/projects/projects.component';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [
    PortfolioComponent,
    ProjectsComponent,
    ProjectComponent,
    TagsComponent
  ],
})
export class PortfolioModule { }
