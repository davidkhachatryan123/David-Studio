import { Component, OnInit } from '@angular/core';
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
  public routers: typeof AppRoutes = AppRoutes;
  public user: AppUser = new AppUser('', '');

  constructor(
    private storageService: AuthStorageService,
    private router: Router
  ) { }

  ngOnInit() {
    this.user = this.storageService.getUser();
  }

  signOut() {
    this.storageService.clean();
    this.router.navigate([this.routers.AUTH]);
  }
}