import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-toolbar',
  templateUrl: 'toolbar.component.html'
})
export class ToolBarComponent {
  @Input() isMenuOpened = true;
  @Input() title = '';

  @Input() toggleDarkTheme = false;
  @Output() toggleDarkThemeChange = new EventEmitter<boolean>();

  @Output() isShowSidebar = new EventEmitter<boolean>();

  constructor(private router: Router) { }

  openMenu(): void {
    this.isMenuOpened = !this.isMenuOpened;

    this.isShowSidebar.emit(this.isMenuOpened);
  }

  toggleTheme($evnet) {
    this.toggleDarkTheme = false;
    this.toggleDarkThemeChange.emit($evnet);
  }
}
