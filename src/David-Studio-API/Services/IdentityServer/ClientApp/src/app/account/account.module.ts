import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { MfaComponent } from './mfa/mfa.component';
import { LogoutComponent } from './logout/logout.component';
import { AccountRoutingModule } from './account-routing.module';
import { AngularMaterialModule } from '../angular-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { ConfirmLogoutComponent } from './confirm-logout/confirm-logout.component';

@NgModule({
  imports: [
    CommonModule,
    AccountRoutingModule,
    AngularMaterialModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    LoginComponent,
    MfaComponent,
    LogoutComponent,
    ConfirmLogoutComponent
  ],
  providers: [
    AuthService
  ]
})
export class AccountModule { }
