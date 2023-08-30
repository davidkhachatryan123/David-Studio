import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';
import { AuthGuard } from './guards';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: AppRoutes.DASHBOARD
  },
  {
    path: AppRoutes.DASHBOARD,
    loadChildren: () => import('./routing/dashboard/dashboard.module').then(module => module.DashboardModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WebsiteRoutingModule { }
