import { Component, OnInit } from '@angular/core';

import {Movie} from "../../models/movie";

@Component({
  selector: 'app-information',
  templateUrl: './information.component.html',
  styleUrls: ['./information.component.css'],
  inputs: ['movie']
})
export class InformationComponent implements OnInit {

  protected movie: Movie;

  constructor() { }

  ngOnInit() {
  }

}
