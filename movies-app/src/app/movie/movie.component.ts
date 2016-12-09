import {Component, OnInit, Input, HostListener} from '@angular/core';

import {SearchEmitter} from "../emitters/search.emitter";

import {MovieService} from "../services/movie.service";

import {Movie} from "../models/movie";

import {Search} from "../models/search";

import {AdvancedSearchEmitter} from "../emitters/avanced.search.emitter";
import {AdvancedSearch} from "../models/advanced.search";


@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
@HostListener('click', ['$event'])

export class MovieComponent implements OnInit {

  protected movies: Movie[];

  protected movie: Movie;

  protected inProgress: boolean = false;

  protected showAlert: boolean = false;

  protected parameters: AdvancedSearch;

  constructor(private movieService: MovieService, private searchEmitter: SearchEmitter, private advancedSearchEmitter: AdvancedSearchEmitter) {
    searchEmitter.getObservable().subscribe(
      searchParams => {
        this.search(searchParams);
      },
      err => {
        console.log(err)
      }
    );

    advancedSearchEmitter.getObservable().subscribe(
      advancedSearchParams => {
        this.advancedSearch(advancedSearchParams);
      },
      err => {
        console.log(err)
      }
    );
  }

  ngOnInit() {

  }

  protected advancedSearch(advancedSearchParams: AdvancedSearch) {
    this.inProgress = true;

    this.movie = null;

    this.showAlert = false;

    let path = "?from=0"
          + "&genre=" + advancedSearchParams.genre
          + "&imdb=" + advancedSearchParams.imdb
          + "&tomatometer" + advancedSearchParams.tomatometer
          + "&metacritic" + advancedSearchParams.metacritic;

    this.movieService.getMoviesByFilter(path).subscribe(
      movies => {
        this.movies = movies;

        this.inProgress = false;

        this.parameters = advancedSearchParams;
      },
      err => {
        console.log(err);
      }
    );
  }

  protected search(searchParams: Search) {
    this.inProgress = true;

    this.movie = null;

    this.movies = null;

    this.showAlert = false;

    this.movieService.getMovieByTitle(searchParams.title).subscribe(
      movie => {
        if (movie.status == 204) {
          setTimeout(() => this.search(searchParams), 1000);
        } else {
          this.movie = movie.json();

          this.movies = null;

          this.inProgress = false;
        }
      },
      err => {
        if (err.status == 404) {
          this.inProgress = false;

          this.showAlert = true;
        } else {
          console.log(err);
        }
      }
    );
  }

  protected onScroll(): void {
    this.advancedSearch(this.parameters);
  }
}
