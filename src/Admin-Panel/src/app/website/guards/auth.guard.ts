import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from "@angular/router";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { Observable, map, take } from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private oidcSecurityService: OidcSecurityService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    return this.oidcSecurityService.checkAuth().pipe(
      map(({ isAuthenticated }) => {
        // allow navigation if authenticated
        if (isAuthenticated) return true;

        // challenge authentication
        this.oidcSecurityService.authorize();
        return false;
      })
    );
  }
}