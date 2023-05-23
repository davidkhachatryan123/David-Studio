import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login.component';
import { TwoFAComponent, UserComponent } from './components';
import { AngularMaterialModule } from 'src/app/shared-module/angular-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    AngularMaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    LoginComponent,
    UserComponent,
    TwoFAComponent
  ],
})
export class LoginModule { }
