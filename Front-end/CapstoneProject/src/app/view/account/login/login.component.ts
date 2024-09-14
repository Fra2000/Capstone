import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/Account/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false; // Aggiunto per il caricamento

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.isLoading = true; // Imposta il caricamento su true
      const loginData = this.loginForm.value;

      // Chiama il servizio di autenticazione
      this.authService.login(loginData).subscribe({
        next: (response) => {
          console.log('Login successful', response);
          this.isLoading = false; // Disabilita il caricamento
          this.router.navigate(['/']); // Reindirizza alla home page o alla dashboard
        },
        error: (error) => {
          console.error('Login failed', error);
          this.isLoading = false; // Disabilita il caricamento
          this.errorMessage = 'Invalid email or password'; // Mostra un messaggio di errore
        }
      });
    } else {
      this.errorMessage = 'Please enter a valid email and password';
    }
  }
}
