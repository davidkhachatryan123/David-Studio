import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginForm: FormGroup;
  rememberMe: boolean;

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
    ) {
    this.rememberMe = false;

    this.loginForm = new FormGroup({
        "username": new FormControl('', [
          Validators.required, Validators.minLength(5), Validators.maxLength(16),
          Validators.pattern('(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$')
        ]),
        "password": new FormControl('', [
          Validators.required, Validators.minLength(8), Validators.maxLength(64),
          Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[;@$!%*?&=#])[A-Za-z0-9;@$!%*?&=#]+$')
        ]),
        "rememberMe": new FormControl(false, Validators.required),
        "returnUrl": new FormControl(route.snapshot.queryParamMap.get('returnUrl'))
    });
  }

  submit() {
    if(this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: ({ returnUrl } ) => {
          window.location.href = returnUrl;
        },
        error: (error: HttpErrorResponse) => {
          this.loginForm.get("password")?.reset();

          this.snackBar.open(error.message, 'Ok', {
            duration: 10000,
          });
        }
      });
    }
  }
}