import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, finalize } from 'rxjs';

import { LoadingService } from './loading.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  private totalRequests = 0;
  private excludeUrls: Array<string>;

  constructor(private loadingService: LoadingService) {
    this.excludeUrls = [
      `${environment.identity.authority}/.+`
    ];
   }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isValidRequestForInterceptor(req.url)) {
      this.totalRequests++;
      this.loadingService.setLoading(true);

      return next.handle(req).pipe(
        finalize(() => {
          this.totalRequests--;

          if (this.totalRequests == 0) {
            this.loadingService.setLoading(false);
          }
        })
      );
    }

    return next.handle(req);
  }

  private isValidRequestForInterceptor(requestuUrl: string): boolean {
    for (let address of this.excludeUrls) {
      if (new RegExp(address).test(requestuUrl)) {
        return false;
      }
    }
    
    return true;
  }
}