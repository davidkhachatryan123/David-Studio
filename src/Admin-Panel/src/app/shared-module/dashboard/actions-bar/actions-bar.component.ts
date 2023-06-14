import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-dashboard-actions-bar',
  templateUrl: 'actions-bar.component.html'
})
export class ActionsBarComponent {
  @Input() width = 'auto';
}