import { Component } from '@angular/core';
import { TableButton, TableCellConfiguration, TableOptions, TableText } from 'src/app/shared-module/dashboard/table/models';
import { Admin } from '../../models';

@Component({
  selector: 'app-dashboard-main-admins',
  templateUrl: 'admins.component.html'
})
export class AdminsComponent {
  tableConfiguration: Array<TableCellConfiguration> = [
    new TableCellConfiguration(
      new TableText(),
      'Id',
      false
    ),
    new TableCellConfiguration(
      new TableText(),
      'Username',
      true
    ),
    new TableCellConfiguration(
      new TableText(),
      'Email',
      true
    ),
    new TableCellConfiguration(
      new TableButton('Send Confirmation Email', value => !value, id => this.onSendConfirmationEmailClick(id)),
      'Email Confirmed',
      true
    ),
    new TableCellConfiguration(
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

  tableOptionsChange($event: TableOptions) {
    console.log($event);
  }

  selectedRowsChange($event: Array<number | string>) {
    console.log($event);
  }

  onSendConfirmationEmailClick(id: number | string) {
    console.log(`SendConfirmationEmail: ${id}`);
  }
}
