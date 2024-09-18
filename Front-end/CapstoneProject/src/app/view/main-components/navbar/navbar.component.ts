import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../../services/Account/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  isAuthenticated: boolean = false;
  isAdminOrSuperAdmin: boolean = false;
  isUser: boolean = false; // Variabile per controllare se l'utente ha il ruolo "User"
  private authSubscription!: Subscription;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    // Sottoscrivi allo stato di autenticazione e ai ruoli dell'utente
    this.authSubscription = this.authService.isAuthenticated().subscribe((isAuth: boolean) => {
      this.isAuthenticated = isAuth;
      this.isAdminOrSuperAdmin = this.authService.hasRole(['Admin', 'SuperAdmin']);
      this.isUser = this.authService.hasRole(['User']); // Controlla se l'utente ha il ruolo "User"
    });
  }

  onLogout(): void {
    this.authService.logout();
    this.isAuthenticated = false;
    this.isAdminOrSuperAdmin = false;
    this.isUser = false; // Resetta anche lo stato dell'utente "User"
    this.router.navigate(['/login']);
  }

  ngOnDestroy(): void {
    // Unsubscribe per evitare memory leaks
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  isLoginOrRegisterPage(): boolean {
    return this.router.url === '/login' || this.router.url === '/registration';
  }
}
