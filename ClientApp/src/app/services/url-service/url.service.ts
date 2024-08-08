import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Api } from 'src/app/data/api';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  constructor(private http: HttpClient) { }

  checkUrl(originalUrl: string, hashLength: number) {
    return this.http.post(`${Api.api}/${Api.url}/${Api.checkUrl}?originalUrl=${originalUrl}&hashLength=${hashLength}`, { }, {withCredentials: true});
  }

  addUrl(originalUrl: string, shortenedUrl: string) {
    return this.http.post(`${Api.api}/${Api.url}/${Api.addUrl}?originalUrl=${originalUrl}&shortenedUrl=${shortenedUrl}`, { }, {withCredentials: true});
  }

  urls() {
    return this.http.get(`${Api.api}/${Api.url}/${Api.urls}`);
  }

  deleteUrl(urlId: number, userId: number) {
    return this.http.delete(`${Api.api}/${Api.url}/${Api.deleteUrl}?urlId=${urlId}&userId=${userId}`, { withCredentials: true });
  }
}
