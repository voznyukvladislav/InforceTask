import { Component } from '@angular/core';
import { Message } from 'src/app/data/message';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { SeedService } from 'src/app/services/seed-service/seed.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  
  showSeed: boolean = false;

  isAuthenticated: boolean = false;

  constructor(private seedService: SeedService, private authService: AuthService) {
    this.authService.isAuthenticated.subscribe(
      isAuthenticated => this.isAuthenticated = isAuthenticated
    );
  }

  seed() {
    this.seedService.seedRoles().subscribe(
      ok => console.log(ok),
      error => console.log(error)
    );
    this.seedService.seedAdmin().subscribe(
      ok => console.log(ok),
      error => console.log(error)
    );
  }

  logout() {
    this.authService.logout().subscribe(
      ok => {
        this.authService.isAuthenticated.next(false);
        this.authService.isAdmin.next(false);
        this.authService.userId.next(0);

        alert(Message.getString(ok as Message));
      },
      error => alert(Message.getString(error.error as Message))
    );
  }
}
