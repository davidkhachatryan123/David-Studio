import { Component, ElementRef, OnDestroy, ViewChild, Renderer2, Input, Output, EventEmitter } from '@angular/core';
import { Subject, Subscription, debounceTime } from 'rxjs';
import { SearchSuggestionsDto, SearchSuggestionsRequestDto, TagReadDto } from 'src/app/website/dto';
import { PortfolioService } from 'src/app/website/services/portfolio.service';

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

  // If it's true search box is showing
  isShow = false;

  // This was created for automatically send search query by timeout
  private searchSubject = new Subject<string>();
  private searchSubscription: Subscription;

  // The following values are associated with the html search box results
  text_result: Array<string> = [];
  tag_result: { tag: TagReadDto, selected: boolean }[] = [];

  focusedIndex = -1;
  mySearchValue: string | undefined;

  @ViewChild("search", {static: false})
  searchElement: ElementRef | undefined;
  @ViewChild("searchResults", {static: false})
  searchResults: ElementRef | undefined;

  constructor(
    private renderer: Renderer2,
    private portfolioService: PortfolioService
  ) {
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
    this.getSearchSuggestions('');

    this.isShow = true;

    this.searchSubscription = this.searchSubject
    .pipe(debounceTime(500))
    .subscribe((searchText: string) => {
      this.getSearchSuggestions(searchText);
    });
  }

  onSearchBlur() {
    this.searchSubscription?.unsubscribe();

    this.submitSearch(this.searchElement.nativeElement.value);
  }

  onSearchChange($event) {
    this.searchSubject.next($event.target.value);
  }

  onSearchKeyDown($event: KeyboardEvent) {
    if ($event.key === 'Enter' || $event.key === 'Escape') {
      this.searchElement.nativeElement.blur();
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
    this.portfolioService.getSuggestions(
      new SearchSuggestionsRequestDto(
        text.trim(), 5, 5
      )
    ).subscribe((suggestions: SearchSuggestionsDto) => {
      this.text_result = suggestions.projectNames;
      this.tag_result = suggestions.tags.map(function(tag: TagReadDto) {
        return { tag: tag, selected: false };
      });
    });
  }

  submitSearch(searchText: string) {
    this.searchSubscription?.unsubscribe();
    this.searchElement.nativeElement.value = searchText.trim();
    this.searchElement.nativeElement.blur();
    this.isShow = false;

    this.searchSubmit.emit(searchText.trim());
  }
}