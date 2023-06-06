import { NgModule } from '@angular/core';

import { AdminsComponent } from './admins.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { EntityDialogComponent } from './components/entity-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: AdminsComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    AdminsComponent,
    EntityDialogComponent
  ],
})
export class AdminsModule { }
