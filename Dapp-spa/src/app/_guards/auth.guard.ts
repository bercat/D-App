import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_servises/auth.service';
import { AlertifyService } from '../_servises/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService) {}

  canActivate(): boolean {/*sjekke om brukeren er logget inn*/
    if (this.authService.loggedIn()) {
      return true;
    } else {

      this.alertify.error('you shall not pass!!'); /* gi vedkommende en notifikasjon */
      this.router.navigate(['/home']); /*send brukeren tilbake til home*/
      return false;
    }
  }
}
