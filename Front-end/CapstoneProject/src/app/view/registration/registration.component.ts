import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/Account/auth.service';
import { User } from '../../interfaces/user';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {
  registrationForm: FormGroup;
  selectedFile: File | null = null; // Variabile per l'immagine selezionata

  constructor(private fb: FormBuilder, private authService: AuthService) {
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
      const newUser: User = this.registrationForm.value;

      // Invia l'utente e il file selezionato al servizio
      this.authService.register(newUser, this.selectedFile).subscribe({
        next: (user) => {
          console.log('Registration successful', user);
          // Puoi aggiungere una logica di navigazione o un messaggio di successo
        },
        error: (error) => {
          console.error('Registration failed', error);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
