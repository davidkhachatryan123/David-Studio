import { NgModule } from '@angular/core';

import { PortfolioComponent } from './portfolio.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ProjectComponent } from './components/projects/project/project.component';
import { CommonModule } from '@angular/common';
import { TagsComponent } from './components/tags/tags.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { TopComponent } from './components/top/top.component';
import { TagDialogComponent } from './dialogs/tag/tag-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TagDialogService } from './services/tag-dialog.service';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [
    PortfolioComponent,
    ProjectsComponent,
    ProjectComponent,
    TagsComponent,
    TopComponent,
    TagDialogComponent
  ],
  providers: [
    TagDialogService
  ]
})
export class PortfolioModule { }
