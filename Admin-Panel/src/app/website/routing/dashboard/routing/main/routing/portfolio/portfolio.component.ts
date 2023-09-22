import { Component } from '@angular/core';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'app-dashboard-main-portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: [ 'portfolio.component.css' ]
})
export class PortfolioComponent {
  tabsConfig: {
    title: string,
    icon: string,
    route: string
  }[] = [
    { 
      title: 'Projects',
      icon: 'apps',
      route: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_PROJECTS
    },
    {
      title: 'Tags',
      icon: 'tags',
      route: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_TAGS
    },
    {
      title: 'Top Projects',
      icon: 'call_made',
      route: AppRoutes.DASHBOARD_MAIN_PORTFOLIO_TOP_PROJECTS
    }
  ];
}
