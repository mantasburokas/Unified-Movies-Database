import {Http, Response} from "@angular/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

import {Movie} from "../models/movie";

@Injectable()
export class MovieService {

  private moviesByGenreUrl = "http://localhost:5000/api/movies/genre/";

  constructor (private http: Http) {

  }

  public getMoviesByGenre(genre: string) : Observable<Movie[]> {
    let observable = this.http.get(this.moviesByGenreUrl + genre)
      .map((res:Response) => res.json())
      .catch((error:any) => Observable.throw(error.json().error || 'Server error'));

    return observable;
  }
}
