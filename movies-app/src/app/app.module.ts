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
import { MdCardModule } from '@angular2-material/card';
import { Ng2DropdownModule } from 'ng2-material-dropdown';
import { MdProgressCircleModule } from "@angular2-material/progress-circle";

import { AccordionModule, AlertModule } from "ng2-bootstrap";

import { GenreService } from "./services/genre.service";
import { MovieService } from "./services/movie.service";

import { SearchEmitter } from "./emitters/search.emitter";
import { SearchComponent } from './search/search.component';

import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';
import { AdvancedSearchEmitter } from "./emitters/avanced.search.emitter";

import {InfiniteScrollModule} from "angular2-infinite-scroll";
import { InformationComponent } from './movie/information/information.component';

@NgModule({
  declarations: [
    AppComponent,
    MovieComponent,
    SearchComponent,
    AdvancedSearchComponent,
    InformationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MdButtonModule,
    MdInputModule,
    MdMenuModule,
    MdIconModule,
    MdCardModule,
    MdProgressCircleModule,
    Ng2DropdownModule,
    AccordionModule,
    AlertModule,
    InfiniteScrollModule
  ],
  providers: [
    OVERLAY_PROVIDERS,
    MdIconRegistry,
    GenreService,
    MovieService,
    SearchEmitter,
    AdvancedSearchEmitter
  ],
  bootstrap: [
    AppComponent
  ]
})

export class AppModule {
}
