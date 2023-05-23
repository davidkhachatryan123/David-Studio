import { Component, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'login-2fa',
  templateUrl: '2fa.component.html'
})
export class TwoFAComponent {
  @Output() twoFAForm: FormGroup;

  constructor(
    // private authService: AuthService,
    // private storageService: StorageService,
    // private _snackBar: MatSnackBar,
    // private router: Router,
    //private route: ActivatedRoute
    ) {
    this.twoFAForm = new FormGroup({
        "token": new FormControl('', [
          Validators.required, Validators.minLength(6), Validators.maxLength(6),
          Validators.pattern('[0-9]+$')
        ])
    });
  }

  submit(){
    if(this.twoFAForm.valid) {

      console.log(this.twoFAForm);

      // let username = this.route.snapshot.queryParams['username'];

      // this.authService.twoFA(new TwoFADto(
      //   username,
      //   this.twoFAForm.controls['token'].value
      // )).subscribe({
      //   next: (data: AuthResponseDto) => {
      //     if (data.isAuthSuccessful) {
      //       this.storageService.saveUser(new AppUser(username, data.email, data.role));
      //       this.storageService.saveToken(data.token);

      //       this.router.navigate([this.routers.DASHBOARD])
      //     }
      //   },
      //   error: (error: HttpErrorResponse) => {
      //     this._snackBar.open(error.error.errorMessage, 'Ok', {
      //       duration: 10000,
      //     });
      //   }
      // });
    }
  }
}
