import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-mfa',
  templateUrl: './mfa.component.html'
})
export class MfaComponent {
  mfaForm: FormGroup;

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    ) {
    this.mfaForm = new FormGroup({
        "code": new FormControl('', [
          Validators.required, Validators.minLength(6), Validators.maxLength(6),
          Validators.pattern('^[0-9]*$')
        ]),
        "rememberMe": new FormControl(Boolean(route.snapshot.queryParamMap.get('rememberMe'))),
        "returnUrl": new FormControl(route.snapshot.queryParamMap.get('returnUrl'))
    });
  }

  submit(){
    if(this.mfaForm.valid) {
      this.authService.mfaLogin(this.mfaForm.value).subscribe({
        next: ({ returnUrl }) => {
          window.location.href = returnUrl;
        },
        error: (ex: any) => {
          this.snackBar.open(ex.error, 'Ok', {
            duration: 10000,
          });
        }
      });
    }
  }
}