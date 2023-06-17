import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from 'src/app/shared-module/shared.module';
import { ServicesComponent } from './services.component';
import { CommonModule } from '@angular/common';
import { EditPriceDialogComponent } from './dialogs/edit/edit-price-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditPriceDialogService } from './services/edit-price-dialog.service';

const routes: Routes = [
  {
    path: '',
    component: ServicesComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    ServicesComponent,
    EditPriceDialogComponent
  ],
  providers: [
    EditPriceDialogService
  ]
})
export class ServicesModule { }
