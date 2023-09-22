import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ContactService } from 'src/app/website/services/notifications';
import { PageData } from 'src/app/website/models';
import { MessageListItemDto } from 'src/app/website/dto';
import { TableOptions } from 'src/app/shared-module/dashboard/table/models';

@Component({
  selector: 'app-dashboard-notifications-messages',
  templateUrl: 'messages.component.html'
})
export class MessagesComponent implements AfterViewInit {
  messages: PageData<MessageListItemDto>;
  tableOptions: TableOptions = new TableOptions('sentDate', 'desc', 0, 0);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private contactService: ContactService) { }

  ngAfterViewInit() {
    this.paginator.page.subscribe(() => this.onChangeEvent());
    this.onChangeEvent();
  }

  reloadData() {
    this.contactService.getMessagesList(this.tableOptions)
    .subscribe((messagesItems: PageData<MessageListItemDto>) => {
      this.messages = messagesItems;
    });
  }

  onChangeEvent() {
    this.tableOptions.pageIndex = this.paginator.pageIndex;
    this.tableOptions.pageSize = this.paginator.pageSize;

    this.reloadData();
  }
}
