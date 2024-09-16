import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeleteGenreRoutingModule } from './delete-genre-routing.module';
import { DeleteGenreComponent } from './delete-genre.component';


@NgModule({
  declarations: [
    DeleteGenreComponent
  ],
  imports: [
    CommonModule,
    DeleteGenreRoutingModule
  ]
})
export class DeleteGenreModule { }
