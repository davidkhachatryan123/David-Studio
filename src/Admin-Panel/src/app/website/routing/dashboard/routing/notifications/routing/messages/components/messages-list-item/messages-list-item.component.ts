import { Component, Input } from '@angular/core';
import { Message } from '../../../../models';
import { Router } from '@angular/router';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'app-dashboard-messages-list-item',
  templateUrl: 'messages-list-item.component.html',
  styleUrls: [ 'messages-list-item.component.css' ]
})
export class MessagesListItemComponent {
  @Input() data: Message;

  constructor(private router: Router) { }

  showMessage(messageId: number) {
    this.router.navigate(
      [
        '/',
        AppRoutes.DASHBOARD,
        AppRoutes.DASHBOARD_NOTIFICATIONS,
        AppRoutes.DASHBOARD_NOTIFICATIONS_MESSAGES,
        AppRoutes.DASHBOARD_NOTIFICATIONS_MESSAGES_MESSAGE
      ],
      {
        queryParams: {
          id: messageId,
          returnUrl: location.href
        }
      }
    );
  }
}
