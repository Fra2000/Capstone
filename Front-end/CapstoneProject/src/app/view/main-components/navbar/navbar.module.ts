import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar.component';

import { NavbarRoutingModule } from './navbar-routing.module';


@NgModule({
  declarations: [
    NavbarComponent
  ],
  imports: [
    CommonModule,
    NavbarRoutingModule
  ],
  exports: [
    NavbarComponent
  ]
})
export class NavbarModule { }
