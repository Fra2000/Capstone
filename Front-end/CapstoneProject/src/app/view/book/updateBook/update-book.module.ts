import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UpdateBookRoutingModule } from './update-book-routing.module';
import { UpdateBookComponent } from './update-book.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    UpdateBookComponent
  ],
  imports: [
    CommonModule,
    UpdateBookRoutingModule,
    FormsModule
  ]
})
export class UpdateBookModule { }
