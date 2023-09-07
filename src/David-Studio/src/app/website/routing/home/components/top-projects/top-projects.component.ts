import { Component, OnInit } from '@angular/core';
import { ProjectReadDto } from 'src/app/website/dto';
import { TopProjectsService } from 'src/app/website/services';

@Component({
  selector: 'home-top-projects',
  templateUrl: 'top-projects.component.html',
  styleUrls: [ 'top-projects.component.css' ]
})
export class TopProjectsComponent implements OnInit {
  topProjects: Array<ProjectReadDto>;

  constructor(private topProjectsService: TopProjectsService) { }

  ngOnInit() {
    this.topProjectsService.getAll(3)
    .subscribe(
      (projects : Array<ProjectReadDto>) => this.topProjects = projects
    );
  }
}