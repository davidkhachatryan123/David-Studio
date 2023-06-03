import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AngularMaterialModule } from './angular-material.module';

import {
  ToolBarComponent,
  UserAccountComponent,
  SidenavComponent,
  DeleteDialogComponent,
  TableComponent
} from './dashboard';
import { CardComponent } from './dashboard/card/card.component';

@NgModule({
  declarations: [
    ToolBarComponent,
    UserAccountComponent,
    SidenavComponent,
    DeleteDialogComponent,
    TableComponent,
    CardComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule
  ],
  exports: [
    AngularMaterialModule,
    ToolBarComponent,
    SidenavComponent,
    DeleteDialogComponent,
    TableComponent,
    CardComponent
  ],
})
export class SharedModule { }
