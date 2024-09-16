import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UpdateAuthorComponent } from './update-author.component';

const routes: Routes = [{ path: '', component: UpdateAuthorComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UpdateAuthorRoutingModule { }
