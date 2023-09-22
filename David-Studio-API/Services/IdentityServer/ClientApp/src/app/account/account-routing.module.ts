import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MfaComponent } from './mfa/mfa.component';
import { LogoutComponent } from './logout/logout.component';
import { ConfirmLogoutComponent } from './confirm-logout/confirm-logout.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: 'login'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'mfa',
    component: MfaComponent
  },
  {
    path: 'logout',
    component: LogoutComponent
  },
  {
    path: 'confirm-logout',
    component: ConfirmLogoutComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
