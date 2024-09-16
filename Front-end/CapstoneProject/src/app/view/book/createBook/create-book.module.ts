import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateBookRoutingModule } from './create-book-routing.module';
import { CreateBookComponent } from './create-book.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CreateBookComponent
  ],
  imports: [
    CommonModule,
    CreateBookRoutingModule,
    FormsModule
  ]
})
export class CreateBookModule { }
