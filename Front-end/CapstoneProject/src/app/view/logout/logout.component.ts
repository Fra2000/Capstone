import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/Account/auth.service';

@Component({
  selector: 'app-logout',
  template: '<p>Logging out...</p>'
})
export class LogoutComponent {
  constructor(private authService: AuthService, private router: Router) {
    this.logoutUser();
  }

  logoutUser() {
    this.authService.logout().subscribe({
      next: () => {
        // Rimuovi eventuali dati locali come token o informazioni utente
        localStorage.removeItem('token');  // Se hai salvato il token con la chiave 'token'
        localStorage.removeItem('user');   // Se hai salvato altre informazioni utente

        // Reindirizza alla homepage o alla pagina di login
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Logout error', err);
      }
    });
  }

}
