import {Http, Response} from "@angular/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {Genre} from "../models/genre";

@Injectable()
export class GenreService {

  private genresUrl = "http://localhost:5000/api/genres";

  constructor (private http: Http) {

  }

  public getGenres() : Observable<Genre[]> {
    let observable = this.http.get(this.genresUrl)
      .map((res:Response) => res.json())
      .catch((error:any) => Observable.throw(error.json().error || 'Server error'));
    return observable;
  }
}
