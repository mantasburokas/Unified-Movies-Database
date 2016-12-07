import {Component, OnInit, Input, HostListener} from '@angular/core';

import {SearchEmitter} from "../emitters/search.emitter";

import {MovieService} from "../services/movie.service";

import {Movie} from "../models/movie";

import {Search} from "../models/search";

import {AdvancedSearchEmitter} from "../emitters/avanced.search.emitter";


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
      search => {
      },
      err => {
        console.log(err)
      }
    );
  }

  ngOnInit() {

  }

  protected advancedSearch(genre: string) {
    this.inProgress = true;

    this.movieService.getMoviesByGenre(genre).subscribe(
      movies => {
        this.movies = movies;

        this.movie = null;

        this.inProgress = false;
      },
      err => {
        console.log(err);
      }
    );
  }

  protected search(searchParams: Search) {
    this.inProgress = true;

    this.movieService.getMovieByTitle(searchParams.title).subscribe(
      movie => {
        if (movie.status == 204) {
          setTimeout(this.search(searchParams), 1000);
        } else if (movie.status == 404) {
          this.inProgress = false;
        } else {
          this.movie = movie.json();

          this.movies = null;

          this.inProgress = false;
        }
      },
      err => {
        console.log(err);
      }
    );
  }
}
