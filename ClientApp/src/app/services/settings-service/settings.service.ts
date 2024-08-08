import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Constants } from 'src/app/data/constants';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  hashLength: BehaviorSubject<number> = new BehaviorSubject<number>(localStorage["hashLength"] ? localStorage["hashLength"] : Constants.hashLength);

  constructor() { }
}
