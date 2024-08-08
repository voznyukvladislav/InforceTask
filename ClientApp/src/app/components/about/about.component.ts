import { Component } from '@angular/core';
import { Message } from 'src/app/data/message';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { IndexService } from 'src/app/services/index-service/index.service';
import { MessageService } from 'src/app/services/message-service/message.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {
  
  isAdmin: boolean = false;
  description: string = "";
  newDescription: string = "";

  constructor(private authService: AuthService, private indexService: IndexService, private messageService: MessageService) {
    this.authService.isAdmin.subscribe(
      isAdmin => this.isAdmin = isAdmin
    );

    this.indexService.getDescription().subscribe(
      (description: any) => this.description = description.value
    );
  }

  changeDescription() {
    this.indexService.changeDescription(this.newDescription).subscribe(
      ok => {
        this.messageService.message.next(ok as Message);
        this.indexService.getDescription().subscribe(
          (newDescription: any) => this.description = newDescription.value
        );
      }
    );
  }
}
