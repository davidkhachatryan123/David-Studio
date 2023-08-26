import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesComponent } from './messages.component';
import { MessagesListItemComponent } from './components/messages-list-item/messages-list-item.component';
import { CommonModule } from '@angular/common';
import { AppRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    component: MessagesComponent
  },
  {
    path: AppRoutes.DASHBOARD_NOTIFICATIONS_MESSAGES_MESSAGE,
    loadChildren: () => import('./pages/message/message-page.module').then(module => module.MessagePageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    SharedModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    MessagesComponent,
    MessagesListItemComponent
  ]
})
export class MessagesModule { }
