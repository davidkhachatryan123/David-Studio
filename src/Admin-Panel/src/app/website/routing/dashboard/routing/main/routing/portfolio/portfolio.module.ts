import { NgModule } from '@angular/core';

import { PortfolioComponent } from './portfolio.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: PortfolioComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [],
  declarations: [PortfolioComponent],
})
export class PortfolioModule { }
