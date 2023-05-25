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
  ActionsNewComponent,
  TableComponent
} from './dashboard';

@NgModule({
  declarations: [
    ToolBarComponent,
    UserAccountComponent,
    SidenavComponent,
    DeleteDialogComponent,
    ActionsNewComponent,
    TableComponent
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
    ActionsNewComponent,
    TableComponent
  ],
})
export class SharedModule { }
