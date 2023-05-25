import { Component } from '@angular/core';
import { TableConfiguration, TableOptions } from 'src/app/shared-module/dashboard/table/models';
import { Admin } from '../../models';

@Component({
  selector: 'app-dashboard-main-admins',
  templateUrl: 'admins.component.html'
})
export class AdminsComponent {
  tableConfiguration: Array<TableConfiguration> = [
    {
      name: 'id',
      title: 'Id',
      displayed: false
    },
    {
      name: 'username',
      title: 'Username',
      displayed: true
    },
    {
      name: 'email',
      title: 'Email',
      displayed: true
    },
    {
      name: 'emailConfirmed',
      title: 'Email Confirmed',
      displayed: true
    },
    {
      name: 'phoneNumber',
      title: 'Phone Number',
      displayed: true
    }
  ];

  data: Array<Admin> = [
    new Admin('1', 'david', 'xdavit7@gmail.com', true, '+374 41 21-48-03'),
    new Admin('2', 'hayk', 'xhayk7@gmail.com', true, null)
  ];

  tableOptions = new TableOptions('username', 'asc', 1, 1);
}
