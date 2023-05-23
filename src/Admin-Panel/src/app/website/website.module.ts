import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { SharedModule } from 'src/app/shared-module/shared.module';
import { WebsiteRoutingModule } from './website-routing.module';

import { AuthStorageService } from './services';

import { environment } from 'src/environments/environment';

@NgModule({
  imports: [
    WebsiteRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: AuthStorageService.getToken,
        allowedDomains: [environment.config.domain],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    AuthStorageService
  ],
})
export class WebSiteModule { }
