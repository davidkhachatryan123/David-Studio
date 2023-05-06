import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  translateSectionName: string = 'home';

  constructor(
    private title: Title,
    private translate: TranslateService) {

    translate.get(`title.${this.translateSectionName}`).subscribe(text => title.setTitle(text));
  }
}
