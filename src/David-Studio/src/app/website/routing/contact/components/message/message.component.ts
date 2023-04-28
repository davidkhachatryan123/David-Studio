import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'contact-message',
  templateUrl: 'message.component.html',
  styleUrls: [ 'message.component.css' ]
})
export class MessageComponent {
  isShow: boolean = false;
}