import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Api } from 'src/app/data/api';

@Injectable({
  providedIn: 'root'
})
export class SeedService {

  constructor(private http: HttpClient) { }

  seedRoles() {
    return this.http.post(`${Api.api}/${Api.seed}/${Api.roles}`, {});
  }

  seedAdmin() {
    return this.http.post(`${Api.api}/${Api.seed}/${Api.admin}`, {});
  }
}
