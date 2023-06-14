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
import { TopProjectComponent } from './components/top/top-project/top-project.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { SetupProjectWizardComponent } from './wizards/setup-project-wizard/setup-project-wizard.component';
import { AppRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent
  },
  {
    path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_SETUP_PROJECT_WIZARD,
    component: SetupProjectWizardComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    SharedModule,
    DragDropModule
  ],
  declarations: [
    PortfolioComponent,
    ProjectsComponent,
    ProjectComponent,
    TagsComponent,
    TopComponent,
    TopProjectComponent,
    TagDialogComponent,
    SetupProjectWizardComponent
  ],
  providers: [
    TagDialogService
  ]
})
export class PortfolioModule { }
