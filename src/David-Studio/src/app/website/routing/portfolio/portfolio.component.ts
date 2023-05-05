import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { Pagintaion, Project, Search, Tag } from './models';
import { AppColors } from '../../consts';
import { FilterTagComponent } from './components/filter-tag/filter-tag.component';

@Component({
  selector: 'portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: [ 'portfolio.component.css' ]
})
export class PortfolioComponent implements AfterViewInit {
  translateSectionName: string = 'portfolio';

  projects: Array<Project> = [
    new Project('Smart Home', [
      new Tag('C#', AppColors.cs),
      new Tag('Arduino', AppColors.arduino),
      new Tag('IoT', AppColors.iot),
      new Tag('PCB', AppColors.pcb),
      new Tag('MySQL', AppColors.mysql)
    ], 'assets/img/Projects/proj1.jpg', ''),
    new Project('David Studio', [
      new Tag('HTML', AppColors.html),
      new Tag('CSS', AppColors.css),
      new Tag('Bootstrap', AppColors.bootstrap),
      new Tag('JS', AppColors.js),
      new Tag('TS', AppColors.ts),
      new Tag('Angular', AppColors.angular),
      new Tag('C#', AppColors.cs),
      new Tag('ASP.NET Core', AppColors.aspnet),
      new Tag('MSSQL', AppColors.mssql),
    ], 'assets/img/Projects/proj2.jpg', ''),
    new Project('Text Analyzer', [
      new Tag('C++', AppColors.cpp),
      new Tag('Bash', AppColors.bash),
    ], 'assets/img/Projects/proj3.jpg', ''),
  ];
  pagination: Pagintaion = new Pagintaion(1, 22);
  latestSearchTextValue: string = "";

  @ViewChild(FilterTagComponent, {static: false})
  private filterTagsElement: FilterTagComponent | undefined;

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

  submitSearchOnlyTags(tags: Array<Tag>) {
    let searchModel = new Search(this.latestSearchTextValue, tags, this.pagination.activePage);

    console.log(searchModel);
  }
}
