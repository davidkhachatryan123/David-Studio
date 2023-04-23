import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';
import { WebSiteComponent } from './website.component';

const routes: Routes = [
  {
    path: '',
    component: WebSiteComponent,
    children: [
      {
        path: '',
        pathMatch: 'prefix',
        redirectTo: AppRoutes.HOME
      },
      {
        path: AppRoutes.HOME,
        loadChildren: () => import('./routing/home/home.module').then(module => module.HomeModule),
        data: { animation: AppRoutes.HOME }
      },
      {
        path: AppRoutes.PORTFOLIO,
        loadChildren: () => import('./routing/portfolio/portfolio.module').then(module => module.PortfolioModule),
        data: { animation: AppRoutes.PORTFOLIO }
      },
      {
        path: AppRoutes.SERVICES,
        loadChildren: () => import('./routing/services/services.module').then(module => module.ServicesModule),
        data: { animation: AppRoutes.SERVICES }
      },
      {
        path: AppRoutes.CONTACT,
        loadChildren: () => import('./routing/contact/contact.module').then(module => module.ContactModule),
        data: { animation: AppRoutes.CONTACT }
      }
    ]
  },
  {
    path: '**',
    loadChildren: () => import('./routing/not-found/not-found.module').then(module => module.NotFoundModule),
    data: { animation: "not-found" }
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class WebsiteRoutingModule { }