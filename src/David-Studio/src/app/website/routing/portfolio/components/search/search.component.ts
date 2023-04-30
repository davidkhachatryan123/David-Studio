import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, Subscription, debounce, debounceTime, interval, map } from 'rxjs';
import { AppColors } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: [ 'search.component.css' ]
})
export class SearchComponent implements OnInit, OnDestroy {
  isShow: boolean = false;

  private searchSubject = new Subject<string>();
  private searchSubscription: Subscription;

  text_result: Array<string> = [];
  tag_result: Array<Tag> = [];

  ngOnInit() {
    this.searchSubscription = this.searchSubject
      .pipe(debounceTime(500))
      .subscribe((searchText: string) => {
        this.search(searchText);
      });
  }

  ngOnDestroy() {
    this.searchSubscription.unsubscribe();
  }

  onSearchChange($event: any) {
    this.searchSubject.next($event.target.value);
  }

  search(text: string) {
    if (text)
      console.log(text);
  }
}