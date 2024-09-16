import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UpdateGenreRoutingModule } from './update-genre-routing.module';
import { UpdateGenreComponent } from './update-genre.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    UpdateGenreComponent
  ],
  imports: [
    CommonModule,
    UpdateGenreRoutingModule,
    FormsModule
  ]
})
export class UpdateGenreModule { }
