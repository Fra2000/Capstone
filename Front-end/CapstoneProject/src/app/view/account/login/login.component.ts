import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.isLoading = true;
      const loginData = this.loginForm.value;


      this.authService.login(loginData).subscribe({
        next: (response) => {
          console.log('Login successful', response);
          this.isLoading = false;
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error('Login failed', error);
          this.isLoading = false;
          this.errorMessage = 'Invalid email or password';
        }
      });
    } else {
      this.errorMessage = 'Please enter a valid email and password';
    }
  }
}
