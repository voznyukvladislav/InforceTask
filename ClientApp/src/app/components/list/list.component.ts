import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/data/message';
import { UrlListItem } from 'src/app/data/urlListItem';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { UrlService } from 'src/app/services/url-service/url.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  
  urls: UrlListItem[] = [];
  userId: number = 0;
  isAdmin: boolean = false;
  isAuthenticated: boolean = false;
  
  constructor(private urlService: UrlService, private authService: AuthService) {
    this.authService.userId.subscribe(
      userId => this.userId = userId
    );

    this.authService.isAdmin.subscribe(
      isAdmin => this.isAdmin = isAdmin
    );

    this.authService.isAuthenticated.subscribe(
      isAuthenticated => this.isAuthenticated = isAuthenticated
    );
  }

  delete(urlId: number, urlUserId: number) {
    if (this.userId == urlUserId || this.isAdmin) {
      this.urlService.deleteUrl(urlId, this.userId).subscribe(
        ok => {
          this.urlService.urls().subscribe(
            urls => this.urls = urls as UrlListItem[]
          );
          
          alert(Message.getString(ok as Message));
        },
        error => alert(Message.getString(error.error as Message))
      );
    }
  }

  ngOnInit(): void {
    this.urlService.urls().subscribe(
      urls => this.urls = urls as UrlListItem[],
      error => console.log(error)
    );

    this.authService.userIdQuery().subscribe(
      userId => this.authService.userId.next(userId as number)
    );
  }
}
