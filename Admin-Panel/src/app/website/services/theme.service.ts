import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  setTheme(isDark: boolean): string {
    const theme = isDark ? 'theme-dark' : 'theme-light';
    window.localStorage.setItem('theme', theme);

    return theme;
  }

  getTheme(): string {
    return window.localStorage.getItem('theme');
  }

  isDark(): boolean {
    return this.getTheme() == 'theme-dark' ? true : false;
  }
}