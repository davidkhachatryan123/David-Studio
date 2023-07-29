import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent {
  logoutId: string | null = this.route.snapshot.queryParamMap.get('logoutId');
  returnUrl: string = '';

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router) {
      if (this.logoutId != null) {
        authService.logout(this.logoutId).subscribe({
          next: ({ prompt, postLogoutRedirectUri }) => {
            if(prompt == true)
              this.router.navigate(['account', 'confirm-logout'],
                { queryParams: { logoutId: this.logoutId } });
            else
            {
              this.returnUrl = postLogoutRedirectUri;
              window.location.href = postLogoutRedirectUri;
            }
          }
        });
      }
    }
}
