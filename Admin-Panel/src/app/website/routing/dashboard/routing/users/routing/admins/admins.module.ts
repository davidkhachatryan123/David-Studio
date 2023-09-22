import { NgModule } from '@angular/core';

import { AdminsComponent } from './admins.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EntityDialogService } from './services/entity-dialog.service';
import { AdminsService } from 'src/app/website/services';
import { EntityDialogComponent } from './dialogs/user/entity-dialog.component';

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
  providers: [
    EntityDialogService,
    AdminsService
  ]
})
export class AdminsModule { }
