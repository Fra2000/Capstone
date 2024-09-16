import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CreateGenreRoutingModule } from './create-genre-routing.module';
import { CreateGenreComponent } from './create-genre.component';


@NgModule({
  declarations: [
    CreateGenreComponent
  ],
  imports: [
    CommonModule,
    CreateGenreRoutingModule,
    FormsModule
  ]
})
export class CreateGenreModule { }
