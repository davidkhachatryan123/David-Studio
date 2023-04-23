import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import { AppRoutes } from 'src/app/website/consts';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: 'navbar.component.html',
  styleUrls: [ 'navbar.component.css' ],
  providers: [ CookieService ]
})
export class NavbarComponent implements OnInit {
  language: string = "";
  appRoutes: typeof AppRoutes = AppRoutes;

  constructor(
    private cookie: CookieService,
    private translate: TranslateService
  ) { }

  ngOnInit() {
    this.setSiteLanguage();

    this.translate.addLangs(['hy-AM', 'ru-RU', 'en-US']);
  }

  setSiteLanguage() {
    this.language = this.cookie.get(environment.config.languageCookieName);

    this.changeSiteLanguage(this.language !== '' ? this.language : 'hy-AM');
  }

  changeSiteLanguage(language: string): void {
    this.language = language;
    this.cookie.set(environment.config.languageCookieName, language, 1);

   
    this.translate.use(language);
  }
}