import { Component } from '@angular/core';
import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'home-services',
  templateUrl: 'services.component.html',
  styleUrls: [ 'services.component.css' ]
})

export class ServicesComponent {
  appRoutes: typeof AppRoutes = AppRoutes;
}