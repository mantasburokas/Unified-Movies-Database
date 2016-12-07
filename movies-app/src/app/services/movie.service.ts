import {Http, Response} from "@angular/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

import {Movie} from "../models/movie";
import {STATUS_CODES} from "http";

@Injectable()
export class MovieService {

  private moviesByGenreUrl = "http://localhost:5000/api/movies/genre/";
  private movieByTitleUrl = "http://localhost:5000/api/movies/";

  constructor (private http: Http) {

  }

  public getMoviesByGenre(genre: string) : Observable<Movie[]> {
    let observable = this.http.get(this.moviesByGenreUrl + genre)
      .map((res:Response) => res.json())
      .catch((error:any) => Observable.throw(error.json().error || 'Server error'));

    return observable;
  }

  public getMovieByTitle(title: string) : Observable<any> {
    let observable = this.http.get(this.movieByTitleUrl + title)
      .map((res:any) => res)
      .catch((error:any) => Observable.throw(error));

    return observable;
  }
}
