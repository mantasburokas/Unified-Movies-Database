import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  protected showMovies: boolean = false;

  protected advancedSearchVisible: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  protected search() : void {
    this.showMovies = true;
  }

  protected showAdvancedSearch(): void {
    if (this.advancedSearchVisible) {
      this.advancedSearchVisible = false;
    } else {
      this.advancedSearchVisible = true;
    }
  }
}
