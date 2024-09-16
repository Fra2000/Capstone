import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/Account/auth.service';
import { User } from '../../../interfaces/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-admin',
  templateUrl: './registration-admin.component.html',
  styleUrls: ['./registration-admin.component.scss']
})
export class RegistrationAdminComponent {
  registrationForm: FormGroup;
  selectedFile: File | null = null; // Variabile per l'immagine selezionata

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  // Gestisce la selezione dell'immagine
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  onSubmit(): void {
    if (this.registrationForm.valid) {
      const newAdmin: User = this.registrationForm.value;

      // Invia l'admin e il file selezionato al servizio
      this.authService.registerAdmin(newAdmin, this.selectedFile).subscribe({
        next: (admin) => {
          console.log('Admin registration successful', admin);
          this.router.navigate(['/']);
        },
        error: (error) => {
          console.error('Admin registration failed', error);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
