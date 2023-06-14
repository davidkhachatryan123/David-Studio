import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { SidenavMenuItem } from './models';

@Component({
  selector: 'app-dashboard-sidenav',
  templateUrl: 'sidenav.component.html',
  styleUrls: ['sidenav.component.css']
})
export class SidenavComponent {
  @Input() sidenavMenuItems: Array<SidenavMenuItem> = [];

  constructor(
    public router: Router,
  ) { }

  isExpanded(menuItem: SidenavMenuItem): boolean {
    return menuItem.listItems.find(item => this.router.url.includes(item.route))
    ? true
    : false;
  }
}
