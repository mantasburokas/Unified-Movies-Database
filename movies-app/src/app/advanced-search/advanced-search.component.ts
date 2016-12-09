import { Component, OnInit } from '@angular/core';

import {GenreService} from "../services/genre.service";
import {Genre} from "../models/genre";

import {AdvancedSearchEmitter} from "../emitters/avanced.search.emitter";

import {AdvancedSearch} from "../models/advanced.search";

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent implements OnInit {

  private genres: Genre[];

  private selectedGenre: string = "Select Genre";

  protected showAlert: boolean = false;

  protected votes: number = 0;

  protected imdb: number = 0.0;

  protected tomatometer: number = 0;

  protected metacritic: number = 0;

  constructor(private genreService: GenreService, private advancedSearchEmitter: AdvancedSearchEmitter) {

  }

  ngOnInit() {
    this.genreService.getGenres().subscribe(
      genres => {
        this.genres = genres;
      },
      err => {
        console.log(err);
      }
    );
  }

  protected setSelectedGenre(genre: string): void {
    this.selectedGenre = genre;

    this.showAlert = false;
  }

  public findMovie() : void {
    if (this.selectedGenre != "Select Genre") {
      this.advancedSearchEmitter
        .getSubject()
        .next(new AdvancedSearch(this.selectedGenre, this.imdb, this.tomatometer, this.metacritic));
    } else {
     this.showAlert = true;
    }
  }
}
