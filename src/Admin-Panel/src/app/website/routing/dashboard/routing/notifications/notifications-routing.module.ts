import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'prefix',
    redirectTo: AppRoutes.DASHBOARD_NOTIFICATIONS_MESSAGES
  },
  {
    path: AppRoutes.DASHBOARD_NOTIFICATIONS_MESSAGES,
    loadChildren: () => import('./routing/messages/messages.module').then(module => module.MessagesModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NotificationsRoutingModule { }
