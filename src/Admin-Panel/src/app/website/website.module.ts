import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { WebsiteRoutingModule } from './website-routing.module';

import { ServerConfigService, ThemeService } from './services';

@NgModule({
  imports: [
    WebsiteRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [
    ThemeService,
    ServerConfigService
  ],
})
export class WebSiteModule { }
