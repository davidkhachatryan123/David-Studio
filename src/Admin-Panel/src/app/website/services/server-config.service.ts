import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ServerConfigService {
  getApiUrl(): string {
    return `${environment.server.protocol}://${environment.server.domain + environment.server.api_uri}`;
  }

  getResourcesUrl(): string {
    return `${environment.server.protocol}://${environment.server.domain}`;
  }
}