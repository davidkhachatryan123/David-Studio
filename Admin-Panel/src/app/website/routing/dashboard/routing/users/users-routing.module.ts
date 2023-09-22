import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: AppRoutes.DASHBOARD_USERS_ADMINS
  },
  {
    path: AppRoutes.DASHBOARD_USERS_ADMINS,
    loadChildren: () => import('./routing/admins/admins.module').then(module => module.AdminsModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule { }
