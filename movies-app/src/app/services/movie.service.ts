import {Http, Response} from "@angular/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";

import {Movie} from "../models/movie";

@Injectable()
export class MovieService {

  private moviesByGenreUrl = "http://localhost:5000/api/movies/genre/";
  private movieByTitleUrl = "http://localhost:5000/api/movies/";
  private moviesByFilterUrl = "http://localhost:5000/api/movies/filter/";

  constructor (private http: Http) {

  }

  public getMoviesByGenre(genre: string) : Observable<any> {
    let observable = this.http.get(this.moviesByGenreUrl + genre)
      .map((res:any) => res)
      .catch((error:any) => Observable.throw(error));

    return observable;
  }

  public getMoviesByFilter(path: string) : Observable<Movie[]> {
    let observable = this.http.get(this.moviesByFilterUrl + path)
      .map((res:Response) => res.json())
      .catch((error:any) => Observable.throw(error));

    return observable;
  }

  public getMovieByTitle(title: string) : Observable<any> {
    let observable = this.http.get(this.movieByTitleUrl + title)
      .map((res:any) => res)
      .catch((error:any) => Observable.throw(error));

    return observable;
  }
}
