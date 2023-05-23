import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';
import { AuthComponent } from './component/auth.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: AppRoutes.AUTH_LOGIN
  },
  {
    path: AppRoutes.AUTH_LOGIN,
    component: AuthComponent,
    loadChildren: () => import('./routing/login/login.module').then(module => module.LoginModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule { }
