import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AllRoutingModule } from './all-routing.module';
import { AllAuthorsComponent } from './all.component';


@NgModule({
  declarations: [
    AllAuthorsComponent
  ],
  imports: [
    CommonModule,
    AllRoutingModule,
    CommonModule
  ]
})
export class AllModule { }
