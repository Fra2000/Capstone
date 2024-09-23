
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', loadChildren: () => import('./view/home/home.module').then(m => m.HomeModule) },
  { path: 'registration', loadChildren: () => import('./view/account/registration/registration.module').then(m => m.RegistrationModule) },
  { path: 'login', loadChildren: () => import('./view/account/login/login.module').then(m => m.LoginModule) },
  { path: 'logout', loadChildren: () => import('./view/account/logout/logout.module').then(m => m.LogoutModule) },
  { path: 'detail/:id', loadChildren: () => import('./view/book/detail/detail.module').then(m => m.DetailModule) },
  { path: 'allAuthors', loadChildren: () => import('./view/author/allAuthor/all.module').then(m => m.AllModule) },
  { path: 'detailsAuthor/:id', loadChildren: () => import('./view/author/detailAuthor/details.module').then(m => m.DetailsModule) },
  { path: 'allGenre', loadChildren: () => import('./view/genre/allgenre/allgenre.module').then(m => m.AllgenreModule) },
  { path: 'detailGenre/:id', loadChildren: () => import('./view/genre/detailGenre/detail-genre.module').then(m => m.DetailGenreModule) },
  { path: 'createGenre', loadChildren: () => import('./view/genre/createGenre/create-genre.module').then(m => m.CreateGenreModule) },
  { path: 'updateGenre/:id', loadChildren: () => import('./view/genre/updateGenre/update-genre.module').then(m => m.UpdateGenreModule) },
  { path: 'deleteGenre/:id', loadChildren: () => import('./view/genre/delete-genre/delete-genre.module').then(m => m.DeleteGenreModule) },
  { path: 'updateAuthor/:id', loadChildren: () => import('./view/author/updateAuthor/update-author.module').then(m => m.UpdateAuthorModule) },
  { path: 'deleteAuthor/:id', loadChildren: () => import('./view/author/deleteAuthor/delete-author.module').then(m => m.DeleteAuthorModule) },
  { path: 'createAuthor', loadChildren: () => import('./view/author/create-author/create-author.module').then(m => m.CreateAuthorModule) },
  { path: 'createBook', loadChildren: () => import('./view/book/createBook/create-book.module').then(m => m.CreateBookModule) },
  { path: 'updateBook/:id', loadChildren: () => import('./view/book/updateBook/update-book.module').then(m => m.UpdateBookModule) },
  { path: 'registrationAdmin', loadChildren: () => import('./view/account/registrationAdmin/registration-admin.module').then(m => m.RegistrationAdminModule) },
  { path: 'Cart', loadChildren: () => import('./view/cart/cart.module').then(m => m.CartModule) },
  { path: 'userBook', loadChildren: () => import('./view/userBook/user-book.module').then(m => m.UserBookModule) }




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
