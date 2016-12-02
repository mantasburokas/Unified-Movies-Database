import {Subject, Observable} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable()
export class SearchEmitter {

  protected stream: Subject<string>;

  constructor() {
    this.stream = new Subject<string>();
  }

  public getSubject(): Subject<string> {
    return this.stream;
  }

  public getObservable(): Observable<string> {
    return this.stream.asObservable();
  }
}
