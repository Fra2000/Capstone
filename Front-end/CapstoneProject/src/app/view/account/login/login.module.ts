import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    LoginComponent // Dichiara il componente di login
  ],
  imports: [
    CommonModule, // Modulo per le direttive comuni di Angular
    ReactiveFormsModule, // Modulo per la gestione dei form reattivi
    RouterModule.forChild([
      { path: '', component: LoginComponent } // Rotta per il login
    ])
  ]
})
export class LoginModule { }
