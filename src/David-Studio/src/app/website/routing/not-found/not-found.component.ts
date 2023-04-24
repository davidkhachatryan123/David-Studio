import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'not-found',
  templateUrl: 'not-found.component.html',
  styleUrls: [ 'not-found.component.css' ]
})

export class NotFoundComponent implements OnInit {
  constructor(
    private titleService: Title,
    private translate: TranslateService) { }

  ngOnInit() {
    this.translate.get('not_found.title').subscribe(title => {
      this.titleService.setTitle(title);
    });
  }
}