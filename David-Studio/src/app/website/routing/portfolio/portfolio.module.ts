import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PortfolioRoutingModule } from './portfolio-routing.module';
import { SharedModule } from '../../shared-module/shared.module';

import { PortfolioComponent } from './portfolio.component';
import { SearchComponent } from './components/search/search.component';
import { FilterTagComponent } from './components/filter-tag/filter-tag.component';
import { PortfolioProjectComponent } from './components/project/project.component';
import { LoaderComponent } from '../../loader/loader.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { PaginatorComponent } from './components/paginator/paginator.component';

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
    FilterTagComponent,
    PortfolioProjectComponent,
    LoaderComponent,
    NotFoundComponent,
    PaginatorComponent
  ],
  providers: [
    PortfolioService
  ]
})
export class PortfolioModule { }
