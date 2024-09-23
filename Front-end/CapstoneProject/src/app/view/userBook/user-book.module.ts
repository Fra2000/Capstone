import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserBookRoutingModule } from './user-book-routing.module';
import { UserBookComponent } from './user-book.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    UserBookComponent
  ],
  imports: [
    CommonModule,
    UserBookRoutingModule,
    FormsModule
  ]
})
export class UserBookModule { }
