import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-mfa',
  templateUrl: './mfa.component.html'
})
export class MfaComponent {
  mfaForm: FormGroup;

  constructor(
    // private authService: AuthService,
    private _snackBar: MatSnackBar,
    // private _router: Router,
    // private _route: ActivatedRoute,
    ) {
    this.mfaForm = new FormGroup({
        "mfa": new FormControl('', [
          Validators.required, Validators.minLength(6), Validators.maxLength(6),
          Validators.pattern('^[0-9]*$')
        ])
    });
  }

  submit(){
    if(this.mfaForm.valid) {
      console.log(this.mfaForm);

      // this.authService.login(new LoginDto(
      //   this.loginForm.controls['username'].value,
      //   this.loginForm.controls['password'].value
      // ))
      // .subscribe({
      //   next: () => {

      //     this._router.navigate([], {
      //       relativeTo: this._route,
      //       queryParams: {
      //         username: this.loginForm.controls['username'].value
      //       },
      //       queryParamsHandling: 'merge',
      //       skipLocationChange: false
      //     });

      //     this.nextEvent.emit();
      //   },
      //   error: (error: HttpErrorResponse) => {
      //       this._snackBar.open(error.error.errorMessage, 'Ok', {
      //         duration: 10000,
      //       });
      //   }
      // });
    }
  }
}