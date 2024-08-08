import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { SettingsService } from 'src/app/services/settings-service/settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent {

  hashLength: number = 0;
  
  isAuthenticated: boolean = false;

  constructor(private settingsService: SettingsService, private authService: AuthService, private router: Router) {
    this.settingsService.hashLength.subscribe(
      hashLength => {
        this.hashLength = hashLength;
      }
    );

    this.authService.isAuthenticated.subscribe(
      isAuthenticated => {
        this.isAuthenticated = isAuthenticated;
        if (!this.isAuthenticated) {
          this.router.navigate(['']);
        }
      }
    );
  }

  change() {
    console.log(this.hashLength);

    this.settingsService.hashLength.next(this.hashLength);
    localStorage["hashLength"] = this.hashLength;
  }
}
