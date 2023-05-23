import { Component, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-auth-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  @ViewChild('stepper') private stepper: MatStepper;

  next() {
    this.stepper.next();
  }
}