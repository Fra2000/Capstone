import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UpdateAuthorRoutingModule } from './update-author-routing.module';
import { UpdateAuthorComponent } from './update-author.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    UpdateAuthorComponent
  ],
  imports: [
    CommonModule,
    UpdateAuthorRoutingModule,
    FormsModule
  ]
})
export class UpdateAuthorModule { }
