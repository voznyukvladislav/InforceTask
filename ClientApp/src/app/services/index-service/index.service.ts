import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Api } from 'src/app/data/api';

@Injectable({
  providedIn: 'root'
})
export class IndexService {

  constructor(private http: HttpClient) { }

  getDescription() {
    return this.http.get(`${Api.shortApi}/${Api.about}`);
  }

  changeDescription(descriptionText: string) {
    return this.http.post(`${Api.shortApi}/${Api.about}?descriptionText=${descriptionText}`, { }, { withCredentials: true })
  }
}
