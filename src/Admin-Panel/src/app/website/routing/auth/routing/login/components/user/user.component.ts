import { Component, Output, EventEmitter, } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'login-user',
  templateUrl: 'user.component.html'
})

export class UserComponent {
  @Output() loginForm: FormGroup;
  @Output() nextEvent = new EventEmitter();

  constructor(
    // private authService: AuthService,
    // private _snackBar: MatSnackBar,
    // private _router: Router,
    // private _route: ActivatedRoute,
    ) {
    this.loginForm = new FormGroup({
        "username": new FormControl('', [
          Validators.required, Validators.minLength(5), Validators.maxLength(16),
          Validators.pattern('(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$')
        ]),
        "password": new FormControl('', [
          Validators.required, Validators.minLength(8), Validators.maxLength(64),
          Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&=#])[A-Za-z0-9;@$!%*?&=#]+$')
        ])
    });
  }

  submit(){
    if(this.loginForm.valid) {

      console.log(this.loginForm);
      this.nextEvent.emit();

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
