import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PortfolioComponent } from './portfolio.component';

import { SharedModule } from '../../shared-module/shared.module';
import { PortfolioRoutingModule } from './portfolio-routing.module';
import { SearchComponent } from './components/search/search.component';
import { PortfolioProjectComponent } from './components/project/project.component';
import { FilterTagComponent } from './components/filter-tag/filter-tag.component';
import { PaginatorComponent } from './components/paginator/paginator.component';

@NgModule({
  imports: [
    CommonModule,
    PortfolioRoutingModule,
    SharedModule ],
  declarations: [
    PortfolioComponent,
    SearchComponent,
    PortfolioProjectComponent,
    FilterTagComponent,
    PaginatorComponent ]
})
export class PortfolioModule { }
