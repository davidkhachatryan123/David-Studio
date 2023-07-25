import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirm-logout',
  templateUrl: './confirm-logout.component.html'
})
export class ConfirmLogoutComponent {
  logoutId: string | null = this.route.snapshot.queryParamMap.get('logoutId');

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute
  ) { }

  yes() {
    if(this.logoutId != null) {
      this.authService.confirmLogout(this.logoutId).subscribe({
        next: ({ postLogoutRedirectUri }) => {
          window.location.href = postLogoutRedirectUri;
        }
      });
    }
  }
}
