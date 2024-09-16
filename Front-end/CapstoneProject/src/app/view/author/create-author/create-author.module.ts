import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateAuthorRoutingModule } from './create-author-routing.module';
import { CreateAuthorComponent } from './create-author.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CreateAuthorComponent
  ],
  imports: [
    CommonModule,
    CreateAuthorRoutingModule,
    FormsModule
  ]
})
export class CreateAuthorModule { }
