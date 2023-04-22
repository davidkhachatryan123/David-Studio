import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { StorageService } from "../services";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private storageService: StorageService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    /*const token = this.storageService.getToken();

    if (token) {
      const cloned = req.clone({
        headers: req.headers.set("Authorization",
        "Bearer " + token)
      });

      return next.handle(cloned);
    }
    else*/
      return next.handle(req);
  }
}