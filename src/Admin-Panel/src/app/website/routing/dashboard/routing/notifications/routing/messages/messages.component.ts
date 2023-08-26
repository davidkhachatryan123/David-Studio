import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Message } from '../../models';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-dashboard-notifications-messages',
  templateUrl: 'messages.component.html'
})
export class MessagesComponent implements AfterViewInit {
  messages: Array<Message>;
  pagesCount = 3;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor() {
    this.messages = [
      new Message(3, 'David Khachatryan', 'xdavit7@gmail.com', '+37441214803', 'Hello World!', false, false, '26-July-2023'),
      new Message(2, 'Hayk Khachatryan', 'xhayk7@gmail.com', '+37494214803', 'Hello! How are you?', true, false, '22-July-2023'),
      new Message(1, 'Ashot Khachatryan', 'xashot7@gmail.com', '+37494204803', 'Hello world! How are you? Do you have a good team for developmen? I want to create one very large project and I needed big team with professionals.', true, true, '18-July-2023'),
    ];
  }

  ngAfterViewInit() {
    this.paginator.page.subscribe(() => {
      // Get data from back-end using it: this.paginator.pageIndex
    });
  }
}
