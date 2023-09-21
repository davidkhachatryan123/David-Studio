import { Component } from '@angular/core';
import { Pagintaion, Search } from './models';
import { AppColors } from '../../consts';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { ProjectReadDto, TagReadDto } from '../../dto';

@Component({
  selector: 'portfolio',
  templateUrl: 'portfolio.component.html',
  styleUrls: [ 'portfolio.component.css' ]
})
export class PortfolioComponent {
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

  searchModel = new Search('', [], this.pagination.activePage);

  constructor(
    private title: Title,
    private translate: TranslateService
  ) {
    this.translate.get(`title.${this.translateSectionName}`)
    .subscribe(text => this.title.setTitle(text));
  }

  changePage($event: number) {
    this.searchModel.page = $event;
    this.search();
  }

  submitSearch($event) {
    this.searchModel.text = $event;
    this.search();
  }

  updateTags($event) {
    this.searchModel.tags = $event;
    this.search();
  }


  // ###################
  //  Submit search
  // ###################

  search() {
    console.log(this.searchModel);
  }
}
