import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserBookComponent } from './user-book.component';

const routes: Routes = [{ path: '', component: UserBookComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserBookRoutingModule { }
