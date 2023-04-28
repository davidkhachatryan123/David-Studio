import { NgModule } from '@angular/core';

import { ServicesHostingRoutingModule } from './hosting-routing.module';

import { HostingComponent } from './components/hosting.component';
import { SharedModule } from 'src/app/website/shared-module/shared.module';

@NgModule({
  imports: [ ServicesHostingRoutingModule,
             SharedModule ],
  declarations: [ HostingComponent ],
})
export class HostingModule { }
