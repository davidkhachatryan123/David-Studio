import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

import { AppRoutes } from 'src/app/website/consts';

// TODO: Need to create user logic

@Component({
  selector: 'app-dashboard-toolbar-user-account',
  templateUrl: 'user-account.component.html',
  styleUrls: [ 'user-account.component.css' ]
})
export class UserAccountComponent implements OnInit {
  @Input() isDarkTheme = false;
  @Output() isDarkThemeChange = new EventEmitter<boolean>();
  routers: typeof AppRoutes = AppRoutes;
  // user = new AppUser('', '');

  constructor(private router: Router) { }

  ngOnInit() {
    // this.user = this.storageService.getUser();
  }

  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
    this.isDarkThemeChange.emit(this.isDarkTheme);
  }

  signOut() {
    // this.storageService.clean();
    this.router.navigate([this.routers.AUTH]);
  }
}
