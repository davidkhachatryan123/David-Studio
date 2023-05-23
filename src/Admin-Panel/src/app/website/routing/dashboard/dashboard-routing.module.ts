import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';
import { DashboardComponent } from './component/dashboard.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: AppRoutes.DASHBOARD_MAIN
  },
  {
    path: AppRoutes.DASHBOARD_MAIN,
    component: DashboardComponent,
    loadChildren: () => import('./routing/main/main.module').then(module => module.MainModule)
  },
  {
    path: AppRoutes.DASHBOARD_USERS,
    component: DashboardComponent,
    loadChildren: () => import('./routing/users/users.module').then(module => module.UsersModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule { }
