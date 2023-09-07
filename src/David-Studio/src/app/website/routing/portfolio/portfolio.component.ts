import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { Pagintaion, Search } from './models';
import { AppColors } from '../../consts';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { ProjectReadDto, TagReadDto } from '../../dto';
import { FilterTagComponent } from './components/filter-tag/filter-tag.component';

@Component({
  selector: 'portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: [ 'portfolio.component.css' ]
})
export class PortfolioComponent implements AfterViewInit {
  translateSectionName = 'portfolio';

  projects: Array<ProjectReadDto> = [
    new ProjectReadDto(1, 'Smart Home', '', 'assets/img/Projects/proj1.jpg',
    [
      new TagReadDto(1, 'C#', AppColors.cs),
      new TagReadDto(2, 'Arduino', AppColors.arduino),
      new TagReadDto(3, 'IoT', AppColors.iot),
      new TagReadDto(4, 'PCB', AppColors.pcb),
      new TagReadDto(5, 'MySQL', AppColors.mysql)
    ])
  ];
  pagination: Pagintaion = new Pagintaion(1, 22);
  latestSearchTextValue = '';

  @ViewChild(FilterTagComponent, {static: false})
  private filterTagsElement: FilterTagComponent | undefined;

  constructor(
    private title: Title,
    private translate: TranslateService) {

    translate.get(`title.${this.translateSectionName}`).subscribe(text => title.setTitle(text));
  }

  ngAfterViewInit() {
    this.submitSearch(this.latestSearchTextValue);
  }


  //####################################################
  // Page functions
  //####################################################

  changePage($event: number) {
    console.log(`Page are chnaged: ${$event}`);

    if (this.filterTagsElement)
      this.submitSearch(this.latestSearchTextValue);
  }


  //####################################################
  // Submit search
  //####################################################

  submitSearch(searchText: string) {
    console.log(new Search(searchText, this.filterTagsElement.tags, this.pagination.activePage));

    this.latestSearchTextValue = searchText;
  }

  submitSearchOnlyTags(tags: Array<TagReadDto>) {
    const searchModel = new Search(this.latestSearchTextValue, tags, this.pagination.activePage);

    console.log(searchModel);
  }
}
