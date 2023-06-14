import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';
import { PortfolioComponent } from './portfolio.component';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent
  },
  {
    path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_SETUP_PROJECT_WIZARD,
    loadChildren: () => import('./wizards/setup-project-wizard/setup-project-wizard.module').then(module => module.SetupProjectWizardModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PortfolioRoutingModule { }
