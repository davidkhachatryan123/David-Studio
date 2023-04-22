import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { WebsiteRoutingModule } from './website-routing.module';
import { SharedModule } from './shared-module/shared.module';
import { WebSiteComponent } from './website.component';

@NgModule({
  imports: [ WebsiteRoutingModule,
             SharedModule,
             RouterModule ],
  declarations: [ WebSiteComponent ],
  providers: [],
})
export class WebSiteModule { }
