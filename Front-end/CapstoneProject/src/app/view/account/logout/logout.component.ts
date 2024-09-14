import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/Account/auth.service';

@Component({
  selector: 'app-logout',
  template: '',
  styleUrls: []
})
export class LogoutComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    // Effettua il logout dell'utente
    this.authService.logout();

    // Reindirizza l'utente alla pagina di login dopo il logout
    this.router.navigate(['/login']);
  }
}
