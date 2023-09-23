import { Component, ViewChild } from '@angular/core';
import { Pagintaion } from './models';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { ProjectReadDto, SearchModelDto, TagReadDto } from '../../dto';
import { PortfolioService } from '../../services/portfolio.service';
import { PageData } from '../../models';
import { PaginatorComponent } from './components/paginator/paginator.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: [ 'portfolio.component.css' ]
})
export class PortfolioComponent {
  translateSectionName = 'portfolio';

  projects: Array<ProjectReadDto> = [];
  pagination: Pagintaion = new Pagintaion(1, 1);
  searchModel = new SearchModelDto(1, environment.config.maxProjectsCountInPortfolioPage);
  
  hideNotFound = true;

  @ViewChild(PaginatorComponent, { static: false })
  paginator: PaginatorComponent | undefined;

  constructor(
    private title: Title,
    private translate: TranslateService,
    private portfolioService: PortfolioService
  ) {
    this.translate.get(`title.${this.translateSectionName}`)
    .subscribe(text => this.title.setTitle(text));
  }

  submitSearch($event: string) {
    this.searchModel.searchText = $event;
    this.search();
  }

  updateTags($event: Array<TagReadDto>) {
    this.searchModel.tagIds = $event.map(t => t.id);
    this.search();
  }

  changePage($event: number) {
    this.searchModel.page = $event;
    this.search();
  }


  // ###################
  //  Submit search
  // ###################

  search() {
    this.portfolioService.search(this.searchModel)
    .subscribe((result: PageData<ProjectReadDto>) => {
      this.projects = result.entities;
      this.pagination.totalPages =
        Math.ceil(result.totalCount / this.searchModel.count);

      this.paginator.updatePaginator();

      if (!result.entities)
        this.hideNotFound = false;
    });
  }
}
