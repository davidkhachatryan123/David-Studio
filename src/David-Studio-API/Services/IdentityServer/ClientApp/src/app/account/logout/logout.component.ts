import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent {
  logoutId: string | null = this.route.snapshot.queryParamMap.get('logoutId');

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router) {
      if (this.logoutId != null) {
        authService.logout(this.logoutId).subscribe({
          next: ({ prompt, postLogoutRedirectUri }) => {
            if(prompt)
              this.router.navigate(['account', 'confirmLogout'],
                { queryParams: { logoutId: this.logoutId } });
            else
              window.location.href = postLogoutRedirectUri;
          }
        });
      }
    }

  rediredtToLogin() {
    this.router.navigate(['account', 'login'])
  }
}
