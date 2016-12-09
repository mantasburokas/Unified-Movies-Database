import {Component, OnInit, ViewChild} from '@angular/core';

import {Search} from "../models/search";

import {SearchEmitter} from "../emitters/search.emitter";

import {AdvancedSearchComponent} from "../advanced-search/advanced-search.component";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  @ViewChild(AdvancedSearchComponent) advancedSearch: AdvancedSearchComponent;

  protected isInputDisabled: boolean = false;

  protected advancedSearchVisible: boolean = false;

  protected title: string;

  constructor(private searchEmitter: SearchEmitter) {

  }

  ngOnInit() {

  }

  protected showAdvancedSearch(): void {
    if (this.advancedSearchVisible) {
      this.advancedSearchVisible = false;

      this.isInputDisabled = false;
    } else {
      this.advancedSearchVisible = true;

      this.isInputDisabled = true;
    }
  }

  protected findMovie(): void {
    if (!this.advancedSearchVisible) {
      this.searchEmitter.getSubject().next(new Search(this.title));
    } else {
      this.advancedSearch.findMovie();
    }
  }
}
