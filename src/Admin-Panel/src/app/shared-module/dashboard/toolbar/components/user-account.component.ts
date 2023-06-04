import { Component, OnInit, Input, HostBinding, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AppUser } from 'src/app/website/models';

import { AppRoutes } from 'src/app/website/consts';
import { AuthStorageService } from 'src/app/website/services';

@Component({
  selector: 'app-dashboard-toolbar-user-account',
  templateUrl: 'user-account.component.html',
  styleUrls: [ 'user-account.component.css' ]
})

export class UserAccountComponent implements OnInit {
  @Input() isDarkTheme = false;
  @Output() isDarkThemeChange = new EventEmitter<boolean>();

  routers: typeof AppRoutes = AppRoutes;
  user = new AppUser('', '');

  constructor(
    private storageService: AuthStorageService,
    private router: Router
  ) { }

  ngOnInit() {
    this.user = this.storageService.getUser();
  }

  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
    this.isDarkThemeChange.emit(this.isDarkTheme);
  }

  signOut() {
    this.storageService.clean();
    this.router.navigate([this.routers.AUTH]);
  }
}
