import { Injectable } from '@angular/core';
import { AppRoutes } from 'src/app/website/consts';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SetupProjectWizardService {
  constructor(private router: Router) { }

  show(projectId: number = -1) {
    this.router.navigate(
      [
        '/',
        AppRoutes.DASHBOARD,
        AppRoutes.DASHBOARD_MAIN,
        AppRoutes.DASHBOARD_MAIN_PORTFOLIO,
        AppRoutes.DASHBOARD_MAIN_PORTFOLIO_SETUP_PROJECT_WIZARD
      ],
      {
        queryParams: {
          id: projectId,
          returnUrl: this.router.routerState.snapshot.url
        }
      }
    );
  }
}