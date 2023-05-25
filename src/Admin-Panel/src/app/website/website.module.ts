import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { WebsiteRoutingModule } from './website-routing.module';

import { AuthStorageService, ServerConfigService } from './services';

import { environment } from 'src/environments/environment';

@NgModule({
  imports: [
    WebsiteRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: AuthStorageService.getToken,
        allowedDomains: [environment.server.domain],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    AuthStorageService,
    ServerConfigService
  ],
})
export class WebSiteModule { }
