import {Component, OnInit} from '@angular/core';

import {GenreService} from "./services/genre.service";
import {MovieService} from "./services/movie.service";

import {Genre} from "./models/genre";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  protected genres: Genre[];

  protected selectedGenreName: string;

  protected showMovies: boolean = false;

  constructor (private genreService: GenreService, private movieService: MovieService) {

  }

  ngOnInit(): void {
    this.genreService.getGenres().subscribe(
      genres => {
        this.genres = genres;
      },
      err => {
        console.log(err);
      }
    );
  }

  protected setGenreInput(genreName: string): void {
    this.selectedGenreName = genreName;
  }
}
