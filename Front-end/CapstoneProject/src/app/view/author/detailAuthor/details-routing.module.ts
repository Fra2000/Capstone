import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailAuthorComponent } from './details.component';

const routes: Routes = [{ path: '', component: DetailAuthorComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DetailsRoutingModule { }
