import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeleteAuthorRoutingModule } from './delete-author-routing.module';
import { DeleteAuthorComponent } from './delete-author.component';


@NgModule({
  declarations: [
    DeleteAuthorComponent
  ],
  imports: [
    CommonModule,
    DeleteAuthorRoutingModule
  ]
})
export class DeleteAuthorModule { }
