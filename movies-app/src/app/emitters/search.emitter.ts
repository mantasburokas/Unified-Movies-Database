import {Subject, Observable} from "rxjs";
import {Injectable} from "@angular/core";

import {Search} from "../models/search";

@Injectable()
export class SearchEmitter {

  protected stream: Subject<Search>;

  constructor() {
    this.stream = new Subject<Search>();
  }

  public getSubject(): Subject<Search> {
    return this.stream;
  }

  public getObservable(): Observable<Search> {
    return this.stream.asObservable();
  }
}
