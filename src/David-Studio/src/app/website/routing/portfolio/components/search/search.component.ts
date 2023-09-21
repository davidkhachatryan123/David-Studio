import { Component, ElementRef, OnDestroy, ViewChild, Renderer2, Input, Output, EventEmitter } from '@angular/core';
import { Subject, Subscription, debounceTime } from 'rxjs';
import { AppColors, AppTags } from 'src/app/website/consts';
import { TagReadDto } from 'src/app/website/dto';

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: [ 'search.component.css' ]
})
export class SearchComponent implements OnDestroy {
  @Input() set addedTags(val: Array<TagReadDto>) {
    this._addedTags = val;
    this.resetAddedTagsArray();
  }
  public get addedTags() {
    return this._addedTags;
  }

  @Output() addedTagsUpdate = new EventEmitter<Array<TagReadDto>>;
  @Output() searchSubmit = new EventEmitter<string>;


  _addedTags: Array<TagReadDto> = [];

  // If it's true then showing search box
  isShow = false;

  // This was created for automatically send search query by timeout
  private searchSubject = new Subject<string>();
  private searchSubscription: Subscription;

  // The following values are associated with the html search box results
  text_result: Array<string> = [
    'David Studio',
    'Study-Control-Software'
  ];
  tag_result: {tag: TagReadDto, selected: boolean}[] = [
    { tag: new TagReadDto(3, AppTags.aspnet, AppColors.aspnet), selected: false },
    { tag: new TagReadDto(2, AppTags.cs, AppColors.cs), selected: false },
    { tag: new TagReadDto(1, AppTags.angular, AppColors.angular), selected: false }
  ];

  focusedIndex = -1;
  mySearchValue: string | undefined;

  @ViewChild("search", {static: false})
  searchElement: ElementRef | undefined;
  @ViewChild("searchResults", {static: false})
  searchResults: ElementRef | undefined;

  constructor(private renderer: Renderer2) {
    this.renderer.listen('window', 'click', (e: Event) => {
      if (e.target !== this.searchElement.nativeElement && !this.searchResults.nativeElement.contains(e.target))
        this.isShow = false;
    });

    this.addedTags = [];
  }

  ngOnDestroy() {
    this.searchSubscription?.unsubscribe();
  }


  //####################################################
  // Searchbox input field events
  //####################################################

  onSearchFocus() {
    this.isShow = true;

    this.searchSubscription = this.searchSubject
    .pipe(debounceTime(500))
    .subscribe((searchText: string) => {
      this.getSearchSuggestions(searchText);
    });
  }

  onSearchBlur() {
    this.searchSubscription?.unsubscribe();
  }

  onSearchChange($event) {
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

    const searchValue = this.text_result[this.focusedIndex];
    
    if (searchValue)
      this.searchElement.nativeElement.value = searchValue;
  }


  //####################################################
  // Searchbox block of tags
  //####################################################

  tagOnClick(indexOfTag: number) {
    this.tag_result[indexOfTag].selected = !this.tag_result[indexOfTag].selected;

    if (this.tag_result[indexOfTag].selected)
      this.addedTags.push(this.tag_result[indexOfTag].tag);
    else
      this.addedTags.splice(this.addedTags.indexOf(this.tag_result[indexOfTag].tag), 1);

    this.addedTagsUpdate.emit(this.addedTags);
  }

  resetAddedTagsArray() {
    this.tag_result.map(result => {
      result.selected = this.addedTags.indexOf(result.tag) != -1 ? true : false;
    });
  }


  //####################################################
  // Submit search
  //####################################################

  submitSearchByClickingListItem($event) {
    if ($event.target.closest('li'))
      this.submitSearch($event.target.innerHTML);
  }

  getSearchSuggestions(text: string) {
    console.log(text.trim());
  }

  submitSearch(searchText: string) {
    this.searchSubscription?.unsubscribe();
    this.searchElement.nativeElement.value = searchText.trim();
    this.searchElement.nativeElement.blur();
    this.isShow = false;

    this.searchSubmit.emit(searchText.trim());
  }
}