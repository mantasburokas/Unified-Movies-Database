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

  protected movies: Movie[] = new Array<Movie>();

  protected movie: Movie;

  protected inProgress: boolean = false;

  protected showAlert: boolean = false;

  protected parameters: AdvancedSearch;

  protected scrolledFlag: boolean = false;

  constructor(private movieService: MovieService, private searchEmitter: SearchEmitter, private advancedSearchEmitter: AdvancedSearchEmitter) {
    searchEmitter.getObservable().subscribe(
      searchParams => {
        this.reset();

        this.search(searchParams);
      },
      err => {
        console.log(err)
      }
    );

    advancedSearchEmitter.getObservable().subscribe(
      advancedSearchParams => {
        this.reset();

        this.advancedSearch(advancedSearchParams);
      },
      err => {
        console.log(err)
      }
    );
  }

  protected reset(): void {
    this.inProgress = true;

    this.movies = new Array<Movie>();

    this.movie = null;

    this.showAlert = false;

    this.scrolledFlag = false;
  }

  ngOnInit() {

  }

  protected advancedSearch(advancedSearchParams: AdvancedSearch) {
    this.inProgress = true;

    let path = "?from=" + this.movies.length
          + "&genre=" + advancedSearchParams.genre
          + "&imdb=" + advancedSearchParams.imdb
          + "&tomatometer=" + advancedSearchParams.tomatometer
          + "&metacritic=" + advancedSearchParams.metacritic
          + "&votes=" + advancedSearchParams.votes;

    this.movieService.getMoviesByFilter(path).subscribe(
      movies => {
        this.movies = this.movies.concat(movies);

        this.inProgress = false;

        this.parameters = advancedSearchParams;

        this.scrolledFlag = false;
      },
      err => {
        console.log(err);
      }
    );
  }

  protected search(searchParams: Search) {
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
    if (!this.scrolledFlag) {
      this.scrolledFlag = true;

      this.advancedSearch(this.parameters);
    }
  }
}
