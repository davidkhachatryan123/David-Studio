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
  ActionsBarComponent,
  ActionBarButtonDirective,
  ActionBarDropDownComponent,
  TableComponent,
  CardComponent
} from './dashboard';
import { DeleteDialogService } from './dashboard/dialogs/delete/services/delete-dialog.service';

@NgModule({
  declarations: [
    ToolBarComponent,
    UserAccountComponent,
    SidenavComponent,
    DeleteDialogComponent,
    ActionsBarComponent,
    ActionBarButtonDirective,
    ActionBarDropDownComponent,
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
    ActionsBarComponent,
    ActionBarButtonDirective,
    ActionBarDropDownComponent,
    TableComponent,
    CardComponent
  ],
  providers: [
    DeleteDialogService
  ]
})
export class SharedModule { }
