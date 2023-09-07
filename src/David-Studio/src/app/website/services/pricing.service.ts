import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ServicesPricingReadDto } from '../dto';

@Injectable()
export class PricingService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.api}/pricing/servicesPricing`;
  }

  getPrices() {
    return this.http.get<ServicesPricingReadDto>(this.apiUrl);
  }
}