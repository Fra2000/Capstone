import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DetailGenreRoutingModule } from './detail-genre-routing.module';
import { DetailGenreComponent } from './detail-genre.component';


@NgModule({
  declarations: [
    DetailGenreComponent
  ],
  imports: [
    CommonModule,
    DetailGenreRoutingModule
  ]
})
export class DetailGenreModule { }
