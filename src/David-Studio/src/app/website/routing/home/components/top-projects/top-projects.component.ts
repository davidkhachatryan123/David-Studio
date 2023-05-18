import { Component } from '@angular/core';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'home-top-projects',
  templateUrl: 'top-projects.component.html',
  styleUrls: [ 'top-projects.component.css' ]
})
export class TopProjectsComponent {
  appRoutes: typeof AppRoutes = AppRoutes;
}