import {Subject, Observable} from "rxjs";
import {Injectable} from "@angular/core";

import {AdvancedSearch} from "../models/advanced.search";

@Injectable()
export class AdvancedSearchEmitter {

  protected stream: Subject<AdvancedSearch>;

  constructor() {
    this.stream = new Subject<AdvancedSearch>();
  }

  public getSubject(): Subject<AdvancedSearch> {
    return this.stream;
  }

  public getObservable(): Observable<AdvancedSearch> {
    return this.stream.asObservable();
  }
}
