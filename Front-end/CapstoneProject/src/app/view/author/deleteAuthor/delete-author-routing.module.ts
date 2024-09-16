import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeleteAuthorComponent } from './delete-author.component';

const routes: Routes = [{ path: '', component: DeleteAuthorComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeleteAuthorRoutingModule { }
