import { Component, Input } from '@angular/core';

@Component({
  selector: 'portfolio-not-found',
  templateUrl: 'not-found.component.html'
})
export class NotFoundComponent {
  @Input() hide: boolean = false;
}