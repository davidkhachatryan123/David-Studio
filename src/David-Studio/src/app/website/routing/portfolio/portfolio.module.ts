import { NgModule } from '@angular/core';

import { PortfolioComponent } from './portfolio.component';

import { SharedModule } from '../../shared-module/shared.module';
import { PortfolioRoutingModule } from './portfolio-routing.module';
import { SearchComponent } from './components/search/search.component';

@NgModule({
  imports: [
    PortfolioRoutingModule,
    SharedModule ],
  declarations: [
    PortfolioComponent,
    SearchComponent ]
})
export class PortfolioModule { }
