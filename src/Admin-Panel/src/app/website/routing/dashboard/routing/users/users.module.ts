import { NgModule } from '@angular/core';

import { UsersRoutingModule } from './users-routing.module';
import { ManageUsersService } from 'src/app/website/services';

@NgModule({
  imports: [
    UsersRoutingModule
  ],
  providers: [
    ManageUsersService
  ]
})
export class UsersModule { }
