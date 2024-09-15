import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailGenreComponent } from './detail-genre.component';

const routes: Routes = [{ path: '', component: DetailGenreComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DetailGenreRoutingModule { }
