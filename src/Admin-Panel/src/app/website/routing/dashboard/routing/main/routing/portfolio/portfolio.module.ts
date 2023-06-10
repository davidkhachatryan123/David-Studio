import { NgModule } from '@angular/core';

import { PortfolioComponent } from './portfolio.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ProjectComponent } from './components/project/project.component';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [
    PortfolioComponent,
    ProjectComponent
  ],
})
export class PortfolioModule { }
