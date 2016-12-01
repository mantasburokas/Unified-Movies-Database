import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';

import { OVERLAY_PROVIDERS } from '@angular2-material/core';
import { MdButtonModule } from '@angular2-material/button';
import { MdInputModule } from '@angular2-material/input';
import { MdMenuModule } from '@angular2-material/menu';
import { MdIconModule, MdIconRegistry } from '@angular2-material/icon';
import {GenreService} from "./services/genre.service";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MdButtonModule,
    MdInputModule,
    MdMenuModule,
    MdIconModule
  ],
  providers: [
    OVERLAY_PROVIDERS,
    MdIconRegistry,
    GenreService
  ],
  bootstrap: [
    AppComponent
  ]
})

export class AppModule {
}
