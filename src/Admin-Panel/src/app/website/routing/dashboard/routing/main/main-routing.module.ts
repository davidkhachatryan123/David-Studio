import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: AppRoutes.DASHBOARD_MAIN_PORTFOLIO
  },
  {
    path: AppRoutes.DASHBOARD_MAIN_PORTFOLIO,
    loadChildren: () => import('./routing/portfolio/portfolio.module').then(module => module.PortfolioModule)
  },
  {
    path: AppRoutes.DASHBOARD_MAIN_TAGS,
    loadChildren: () => import('./routing/tags/tags.module').then(module => module.TagsModule)
  },
  {
    path: AppRoutes.DASHBOARD_MAIN_SERVICES,
    loadChildren: () => import('./routing/services/services.module').then(module => module.ServicesModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
