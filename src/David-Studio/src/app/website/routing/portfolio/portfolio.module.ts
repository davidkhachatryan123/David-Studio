import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PortfolioRoutingModule } from './portfolio-routing.module';
import { SharedModule } from '../../shared-module/shared.module';

import { PortfolioComponent } from './portfolio.component';
import { SearchComponent } from './components/search/search.component';
import { PortfolioProjectComponent } from './components/project/project.component';
import { FilterTagComponent } from './components/filter-tag/filter-tag.component';
import { PaginatorComponent } from './components/paginator/paginator.component';
import { NotFoundComponent } from './not-found/not-found.component';

import { PortfolioService } from '../../services/portfolio.service';

@NgModule({
  imports: [
    CommonModule,
    PortfolioRoutingModule,
    SharedModule
  ],
  declarations: [
    PortfolioComponent,
    SearchComponent,
    PortfolioProjectComponent,
    FilterTagComponent,
    PaginatorComponent,
    NotFoundComponent
  ],
  providers: [
    PortfolioService
  ]
})
export class PortfolioModule { }
