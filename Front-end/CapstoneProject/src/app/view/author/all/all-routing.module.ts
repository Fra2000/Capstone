import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllAuthorsComponent } from './all.component';

const routes: Routes = [{ path: '', component: AllAuthorsComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AllRoutingModule { }
