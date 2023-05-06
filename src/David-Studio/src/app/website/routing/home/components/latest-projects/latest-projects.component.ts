import { Component } from '@angular/core';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'home-latest-projects',
  templateUrl: 'latest-projects.component.html',
  styleUrls: [ 'latest-projects.component.css' ]
})

export class LatestProjectsComponent {
  appRoutes: typeof AppRoutes = AppRoutes;
}