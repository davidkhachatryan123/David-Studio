import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService, UserDataResult } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';

import { AppRoutes } from 'src/app/website/consts';

@Component({
  selector: 'app-dashboard-toolbar-user-account',
  templateUrl: 'user-account.component.html',
  styleUrls: [ 'user-account.component.css' ]
})
export class UserAccountComponent implements OnInit {
  @Input() isDarkTheme = false;
  @Output() isDarkThemeChange = new EventEmitter<boolean>();

  userData: UserDataResult;

  constructor(private router: Router, private oidc: OidcSecurityService) { }

  ngOnInit() {
    this.oidc.userData$.subscribe(data => {this.userData = data
            console.log(data); 
            });
  }

  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
    this.isDarkThemeChange.emit(this.isDarkTheme);
  }

  signOut() {
    this.oidc.logoff().subscribe();
  }
}
