import { Component, OnInit } from '@angular/core';

import {Search} from "../models/search";

import {SearchEmitter} from "../emitters/search.emitter";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

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
    this.searchEmitter.getSubject().next(new Search(this.title));
  }
}
