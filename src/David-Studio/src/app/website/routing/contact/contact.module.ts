import { NgModule } from '@angular/core';

import { ContactComponent } from './contact.component';
import { MessageComponent } from './components/message/message.component';

import { SharedModule } from '../../shared-module/shared.module';
import { ContactRoutingModule } from './contact-routing.module';

@NgModule({
  imports: [ ContactRoutingModule,
             SharedModule ],
  declarations: [ ContactComponent,
                  MessageComponent ],
})
export class ContactModule { }
