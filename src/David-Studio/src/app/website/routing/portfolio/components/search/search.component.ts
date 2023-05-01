import { Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { Subject, Subscription, debounceTime } from 'rxjs';
import { AppColors, AppTags } from 'src/app/website/consts';
import { Tag } from 'src/app/website/routing/portfolio/models';

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: [ 'search.component.css' ]
})
export class SearchComponent implements OnDestroy {
  isShow: boolean = false;

  private searchSubject = new Subject<string>();
  private searchSubscription: Subscription;

  text_result: Array<string> = [
    'David Studio',
    'Study-Control-Software'
  ];
  tag_result: Array<Tag> = [
    new Tag(AppTags.aspnet, AppColors.aspnet),
    new Tag(AppTags.cs, AppColors.cs),
    new Tag(AppTags.angular, AppColors.angular)
  ];

  focusedIndex: number = -1;
  mySearchValue: string | undefined;

  @ViewChild("search", {static: false})
  searchElement: ElementRef | undefined;


  ngOnDestroy() {
    this.searchSubscription.unsubscribe();
  }


  onSearchFocus() {
    this.searchSubscription = this.searchSubject
    .pipe(debounceTime(500))
    .subscribe((searchText: string) => {
      this.search(searchText);
    });
  }

  onSearchChange($event: any) {
    this.searchSubject.next($event.target.value);
  }

  onSearchKeyDown($event: KeyboardEvent) {
    if ($event.key === 'Enter') {
      this.submitSearch(this.searchElement.nativeElement.value);
    }
    else if ($event.key === 'ArrowUp') {
      this.focusedIndex > 0
        ? this.focusedIndex--
        : this.focusedIndex = this.text_result.length - 1;
    } else if ($event.key === 'ArrowDown') {
      this.focusedIndex < this.text_result.length - 1
        ? this.focusedIndex++
        : this.focusedIndex = 0;
    } else this.focusedIndex = -1;

    let searchValue = this.text_result[this.focusedIndex];
    
    if (searchValue)
      this.searchElement.nativeElement.value = searchValue;
  }


  submitSearchByClickingListItem($event) {
    if ($event.target.closest('li'))
      this.submitSearch($event.target.innerHTML);
  }

  search(text: string) {
    if (text)
      console.log(text);
  }

  submitSearch(searchText: string) {
    if (searchText) {
      if (this.searchSubscription)
        this.searchSubscription.unsubscribe();
      this.searchElement.nativeElement.blur();

      console.log('Search submitted with value: ', searchText.trim());
    } else {
      this.searchElement.nativeElement.focus();
    }
  }
}