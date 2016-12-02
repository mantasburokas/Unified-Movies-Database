import {Component, OnInit, Input, HostListener} from '@angular/core';

import {SearchEmitter} from "../emitters/search.emitter";

import {MovieService} from "../services/movie.service";

import {Movie} from "../models/movie";

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
@HostListener('click', ['$event'])

export class MovieComponent implements OnInit {

  protected movies: Movie[];

  constructor(private movieService: MovieService, private searchEmitter: SearchEmitter) {
    searchEmitter.getObservable().subscribe(
      search => {
        this.search(search);
      },
      err => {
        console.log(err)
      }
    )
  }

  search(genre: string) {
    this.movieService.getMoviesByGenre(genre).subscribe(
      movies => {
        this.movies = movies;
      },
      err => {
        console.log(err);
      }
    );
  }

  ngOnInit() {
  }
}
