import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationAdminComponent } from './registration-admin.component';

const routes: Routes = [{ path: '', component: RegistrationAdminComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegistrationAdminRoutingModule { }
