import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeleteGenreComponent } from './delete-genre.component';

const routes: Routes = [{ path: '', component: DeleteGenreComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeleteGenreRoutingModule { }
