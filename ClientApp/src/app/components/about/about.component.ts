import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { IndexService } from 'src/app/services/index-service/index.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {
  
  isAdmin: boolean = false;
  description: string = "";
  newDescription: string = "";

  constructor(private authService: AuthService, private indexService: IndexService) {
    this.authService.isAdmin.subscribe(
      isAdmin => this.isAdmin = isAdmin
    );

    this.indexService.getDescription().subscribe(
      (description: any) => this.description = description.value
    );
  }

  changeDescription() {
    this.indexService.changeDescription(this.newDescription).subscribe(
      (newDescription: any) => this.description = newDescription.value
    );
  }
}
