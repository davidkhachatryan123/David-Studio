import { Component } from '@angular/core';
import { SidenavMenuItem } from 'src/app/shared-module/dashboard/sidenav/models';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'dashboard.component.html',
  styleUrls: [ 'dashboard.component.css' ]
})
export class DashboardComponent {
  sidenavMenuItems: Array<SidenavMenuItem> = [
    {
      title: 'Main',
      material_icon: 'donut_large',
      listItems: [
        {
          title: 'Portfolio',
          description: 'Manage portfolio projects',
          material_icon: 'business_center',
          route: `/${AppRoutes.DASHBOARD}/${AppRoutes.DASHBOARD_MAIN}/${AppRoutes.DASHBOARD_MAIN_PORTFOLIO}`
        },
        {
          title: 'Services',
          description: 'Manage services',
          material_icon: 'price_check',
          route: `/${AppRoutes.DASHBOARD}/${AppRoutes.DASHBOARD_MAIN}/${AppRoutes.DASHBOARD_MAIN_SERVICES}`
        }
      ]
    },
    {
      title: 'Users',
      material_icon: 'account_box',
      listItems: [
        {
          title: 'Admins',
          description: 'Manage admin panel users',
          material_icon: 'verified_user',
          route: `/${AppRoutes.DASHBOARD}/${AppRoutes.DASHBOARD_USERS}/${AppRoutes.DASHBOARD_USERS_ADMINS}`
        }
      ]
    }
  ];
}
