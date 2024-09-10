
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', loadChildren: () => import('./view/home/home.module').then(m => m.HomeModule)},
  { path: 'registration', loadChildren: () => import('./view/registration/registration.module').then(m => m.RegistrationModule) },
  { path: 'login', loadChildren: () => import('./view/login/login.module').then(m => m.LoginModule) },
  { path: 'logout', loadChildren: () => import('./view/logout/logout.module').then(m => m.LogoutModule) }




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
