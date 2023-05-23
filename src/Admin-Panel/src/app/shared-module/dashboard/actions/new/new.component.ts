import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-dashboard-actions-new',
  templateUrl: 'new.component.html'
})
export class ActionsNewComponent {
  @Output() onNew = new EventEmitter();

  onNewEvent() {
    this.onNew.emit();
  }
}