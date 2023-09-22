import { Component, ElementRef, OnInit, ViewChild, AfterContentInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import { AppRoutes } from 'src/app/website/consts';
import { TranslateService } from '@ngx-translate/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: 'navbar.component.html',
  styleUrls: [ 'navbar.component.css' ],
  providers: [ CookieService ]
})
export class NavbarComponent implements OnInit, AfterContentInit {
  language = '';
  appRoutes: typeof AppRoutes = AppRoutes;

  @ViewChild("navbarToggler", {static: false})
  navbarToggler: ElementRef | undefined;
  @ViewChild("navbarCollapse", {static: false})
  navbarCollapse: ElementRef | undefined;

  constructor(
    private cookie: CookieService,
    private translate: TranslateService,
    private router: Router
  ) { }

  ngOnInit() {
    this.setSiteLanguage();

    this.translate.addLangs(['hy-AM', 'ru-RU', 'en-US']);
  }

  ngAfterContentInit() {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd && this.navbarCollapse.nativeElement.classList.contains("show"))
        this.navbarToggler.nativeElement.click();
    });
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