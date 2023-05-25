import { NgModule } from '@angular/core';

import { AdminsComponent } from './admins.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';

const routes: Routes = [
  {
    path: '',
    component: AdminsComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [AdminsComponent],
})
export class AdminsModule { }
