import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Api } from 'src/app/data/api';
import { Constants } from 'src/app/data/constants';
import { Message } from 'src/app/data/message';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { MessageService } from 'src/app/services/message-service/message.service';
import { SettingsService } from 'src/app/services/settings-service/settings.service';
import { UrlService } from 'src/app/services/url-service/url.service';

@Component({
  selector: 'app-shortener',
  templateUrl: './shortener.component.html',
  styleUrls: ['./shortener.component.css']
})
export class ShortenerComponent {
  
  originalUrl: string = "";
  shortenedUrl: string = "";
  hashLength: number = 0;

  isAuthenticated: boolean = false;

  constructor(private urlService: UrlService, private settingsService: SettingsService, private authService: AuthService, private messageService: MessageService, private router: Router) {
    this.settingsService.hashLength.subscribe(
      hashLength => this.hashLength = hashLength
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

  checkUrl() {
    this.urlService.checkUrl(this.originalUrl, this.hashLength).subscribe(
      (shortenedUrl: any) => this.shortenedUrl = shortenedUrl.shortenedUrl as string,
      error => {
        this.originalUrl = "";
        this.shortenedUrl = "";
      }
    );
  }

  addUrl() {
    if (this.originalUrl && this.shortenedUrl) {
      this.urlService.addUrl(this.originalUrl, this.shortenedUrl).subscribe(
        ok => {
          this.originalUrl = "";
          this.shortenedUrl = "";

          this.messageService.message.next(ok as Message);
        },
        error => this.messageService.message.next(error.error as Message)
      )
    }
  }
}
