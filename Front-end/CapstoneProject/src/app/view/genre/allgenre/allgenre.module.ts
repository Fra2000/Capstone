import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AllgenreRoutingModule } from './allgenre-routing.module';
import { AllGenreComponent } from './allgenre.component';


@NgModule({
  declarations: [
    AllGenreComponent
  ],
  imports: [
    CommonModule,
    AllgenreRoutingModule
  ]
})
export class AllgenreModule { }
