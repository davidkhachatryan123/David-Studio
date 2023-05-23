import { NgModule } from '@angular/core';

import { AdminsComponent } from './admins.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: AdminsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [],
  declarations: [AdminsComponent],
})
export class AdminsModule { }
