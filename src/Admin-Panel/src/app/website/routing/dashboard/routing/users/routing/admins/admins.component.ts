import { Component } from '@angular/core';
import { TableButton, TableConfiguration, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { Admin } from '../../models';

@Component({
  selector: 'app-dashboard-main-admins',
  templateUrl: 'admins.component.html'
})
export class AdminsComponent {
  tableConfiguration: Array<TableConfiguration> = [
    new TableConfiguration(
      new TableText(),
      'Id',
      false
    ),
    new TableConfiguration(
      new TableText(),
      'Username',
      true
    ),
    new TableConfiguration(
      new TableText(),
      'Email',
      true
    ),
    new TableConfiguration(
      new TableButton('Send Confirmation Email', value => !value, id => this.onSendConfirmationEmailClick(id)),
      'Email Confirmed',
      true
    ),
    new TableConfiguration(
      new TableText(),
      'Phone Number',
      true
    )
  ];

  data: Array<Admin> = [
    new Admin('1', 'david', 'xdavit7@gmail.com', true, '+374 41 21-48-03'),
    new Admin('2', 'hayk', 'xhayk7@gmail.com', false, null)
  ];

  tableOptions = new TableOptions('username', 'asc', 1, 1);

  onSendConfirmationEmailClick(id: number | string) {
    console.log(`SendConfirmationEmail: ${id}`);
  }
}
