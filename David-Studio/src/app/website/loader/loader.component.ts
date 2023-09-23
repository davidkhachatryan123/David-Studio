import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-loader',
  templateUrl: 'loader.component.html',
  styleUrls: ['loader.component.css']
})

export class LoaderComponent {
  @Input() size: string = '120px';
  @Input() thickness: string = '12px';

  @Input() backgroundColor: string = 'transparent';
  @Input() spinnerColor: string = 'white';

  @Input() speed: number = 1;
}