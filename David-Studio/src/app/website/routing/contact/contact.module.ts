import { NgModule } from '@angular/core';

import { ContactRoutingModule } from './contact-routing.module';
import { SharedModule } from '../../shared-module/shared.module';

import { ContactComponent } from './contact.component';
import { MessageComponent } from './components/message/message.component';

import { ContactService } from '../../services';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    ContactRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    ContactComponent,
    MessageComponent
  ],
  providers: [
    ContactService
  ]
})
export class ContactModule { }
