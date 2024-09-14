
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', loadChildren: () => import('./view/home/home.module').then(m => m.HomeModule) },
  { path: 'registration', loadChildren: () => import('./view/account/registration/registration.module').then(m => m.RegistrationModule) },
  { path: 'login', loadChildren: () => import('./view/account/login/login.module').then(m => m.LoginModule) },
  { path: 'logout', loadChildren: () => import('./view/account/logout/logout.module').then(m => m.LogoutModule) },
  { path: 'detail/:id', loadChildren: () => import('./view/book/detail/detail.module').then(m => m.DetailModule) },
  { path: 'update', loadChildren: () => import('./view/book/update/update.module').then(m => m.UpdateModule) },
  { path: 'authors', loadChildren: () => import('./view/author/all/all.module').then(m => m.AllModule) },
  { path: 'detailsAuthor/:id', loadChildren: () => import('./view/author/detail/details.module').then(m => m.DetailsModule) }




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
