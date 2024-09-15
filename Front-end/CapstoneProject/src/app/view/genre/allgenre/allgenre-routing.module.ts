import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllGenreComponent } from './allgenre.component';

const routes: Routes = [{ path: '', component: AllGenreComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AllgenreRoutingModule { }
