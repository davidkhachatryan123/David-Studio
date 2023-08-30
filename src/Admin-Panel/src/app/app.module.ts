import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthInterceptor, AuthModule, LogLevel } from 'angular-auth-oidc-client';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoadingInterceptor } from './loading/loading.interceptor';
import { LoadingComponent } from './loading/component/loading.component';
import { WebSiteModule } from './website/website.module';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    WebSiteModule,
    AuthModule.forRoot({
      config: {
        authority: environment.identity.authority,
        redirectUrl: window.location.origin + '/',
        disableIdTokenValidation: false,
        postLogoutRedirectUri: window.location.origin + '/',
        clientId: 'crm',
        scope: environment.identity.scopes.join(' '),
        responseType: 'code',
        disablePkce: false,
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug,
        secureRoutes: [environment.api + '/']
      },
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
