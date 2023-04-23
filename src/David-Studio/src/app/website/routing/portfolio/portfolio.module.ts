import { NgModule } from '@angular/core';

import { PortfolioComponent } from './portfolio.component';
import { ProjectsComponent } from './components/projects/projects.component';

import { SharedModule } from '../../shared-module/shared.module';
import { PortfolioRoutingModule } from './portfolio-routing.module';

@NgModule({
  imports: [
    PortfolioRoutingModule,
    SharedModule ],
  declarations: [
    PortfolioComponent,
    ProjectsComponent ]
})
export class PortfolioModule { }
