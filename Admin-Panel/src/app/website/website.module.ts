import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { WebsiteRoutingModule } from './website-routing.module';

import { ThemeService } from './services';
import { AuthGuard } from './guards';

@NgModule({
  imports: [
    WebsiteRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [
    ThemeService,
    AuthGuard
  ],
})
export class WebSiteModule { }
