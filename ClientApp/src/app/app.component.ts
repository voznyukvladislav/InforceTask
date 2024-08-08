import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth-service/auth.service';
import { SettingsService } from './services/settings-service/settings.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private authService: AuthService, private settingService: SettingsService) {
    
  }

  ngOnInit(): void {
    this.authService.isAuthenticatedQuery().subscribe(
      isAuthenticated => this.authService.isAuthenticated.next(isAuthenticated as boolean),
      error => console.log(error)
    );

    this.authService.isAdminQuery().subscribe(
      isAdmin => this.authService.isAdmin.next(isAdmin as boolean),
      error => console.log(error)
    );
  }
}
