import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RegistrationAdminRoutingModule } from './registration-admin-routing.module';
import { RegistrationAdminComponent } from './registration-admin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    RegistrationAdminComponent
  ],
  imports: [
    CommonModule,
    RegistrationAdminRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class RegistrationAdminModule { }
