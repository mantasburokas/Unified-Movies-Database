import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { MovieComponent } from './movie/movie.component';

import { OVERLAY_PROVIDERS } from '@angular2-material/core';
import { MdButtonModule } from '@angular2-material/button';
import { MdInputModule } from '@angular2-material/input';
import { MdMenuModule } from '@angular2-material/menu';
import { MdIconModule, MdIconRegistry } from '@angular2-material/icon';

import {AccordionModule} from "ng2-bootstrap";

import {GenreService} from "./services/genre.service";
import {MovieService} from "./services/movie.service";

import {SearchEmitter} from "./emitters/search.emitter";
import { SearchComponent } from './search/search.component';
import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';

@NgModule({
  declarations: [
    AppComponent,
    MovieComponent,
    SearchComponent,
    AdvancedSearchComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MdButtonModule,
    MdInputModule,
    MdMenuModule,
    MdIconModule,
    AccordionModule
  ],
  providers: [
    OVERLAY_PROVIDERS,
    MdIconRegistry,
    GenreService,
    MovieService,
    SearchEmitter
  ],
  bootstrap: [
    AppComponent
  ]
})

export class AppModule {
}
