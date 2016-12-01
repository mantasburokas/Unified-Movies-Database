import {Component, OnInit} from '@angular/core';
import {GenreService} from "./services/genre.service";
import {Genre} from "./models/genre";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  protected genres: Genre[];

  protected genre: string;

  constructor (private genreService: GenreService) {

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

  protected setGenreInput(genre: string): void {
    this.genre = genre;
  }
}
