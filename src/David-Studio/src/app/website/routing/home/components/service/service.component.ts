import { Component, Input } from '@angular/core';
import { AppRoutes } from 'src/app/website/consts';
import { Service } from '../../models';

@Component({
  selector: 'home-service',
  templateUrl: 'service.component.html',
  styleUrls: [ 'service.component.css' ]
})
export class ServicesComponent {
  @Input() service = new Service();

  appRoutes: typeof AppRoutes = AppRoutes;
}
