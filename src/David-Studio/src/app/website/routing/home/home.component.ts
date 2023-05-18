import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { Service } from './models';
import { AppRoutes } from '../../consts';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  translateSectionName = 'home';
  translateServiceSectionName = 'template.home.services';

  services: Array<Service> = [
    new Service(
      `${this.translateServiceSectionName}.web_development.title`,
      `${this.translateServiceSectionName}.web_development.body`,
      'assets/img/Service/web.jpg',
      'left',
      AppRoutes.SERVICES),
    new Service(
      `${this.translateServiceSectionName}.auth_and_db.title`,
      `${this.translateServiceSectionName}.auth_and_db.body`,
      'assets/img/Service/auth.jpg',
      'right',
      AppRoutes.SERVICES),
    new Service(
      `${this.translateServiceSectionName}.admin_panel.title`,
      `${this.translateServiceSectionName}.admin_panel.body`,
      'assets/img/Service/admin.jpg',
      'left',
      AppRoutes.SERVICES),
    new Service(
      `${this.translateServiceSectionName}.payment.title`,
      `${this.translateServiceSectionName}.payment.body`,
      'assets/img/Service/payment.jpg',
      'right',
      AppRoutes.SERVICES),
    new Service(
      `${this.translateServiceSectionName}.support.title`,
      `${this.translateServiceSectionName}.support.body`,
      'assets/img/Service/support.jpg',
      'left',
      AppRoutes.SERVICES)
  ]

  constructor(
    private title: Title,
    private translate: TranslateService) {

    translate.get(`title.${this.translateSectionName}`).subscribe(text => title.setTitle(text));
  }
}
