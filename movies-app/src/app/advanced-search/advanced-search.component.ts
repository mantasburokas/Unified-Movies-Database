import { Component, OnInit } from '@angular/core';

import {GenreService} from "../services/genre.service";
import {Genre} from "../models/genre";

import {AdvancedSearchEmitter} from "../emitters/avanced.search.emitter";

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent implements OnInit {

  private genres: Genre[];

  private selectedGenre: string = "Select Genre";

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
  }

  public findMovie() : void {
    console.log("asd");
  }
}
