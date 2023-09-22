import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';
import { SetupProjectWizardComponent } from './wizards/setup-project-wizard/setup-project-wizard.component';
import { PortfolioComponent } from './portfolio.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { TagsComponent } from './components/tags/tags.component';
import { TopComponent } from './components/top/top.component';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent,
    children: [
      {
        path: '',
        pathMatch: 'prefix',
        redirectTo: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_PROJECTS,
      },
      {
        path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_PROJECTS,
        component: ProjectsComponent
      },
      {
        path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_TAGS,
        component: TagsComponent
      },
      {
        path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_TOP_PROJECTS,
        component: TopComponent
      }
    ]
  },
  {
    path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_SETUP_PROJECT_WIZARD,
    component: SetupProjectWizardComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PortfolioRoutingModule { }
