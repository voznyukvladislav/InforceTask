import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Message } from 'src/app/data/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  message: BehaviorSubject<Message> = new BehaviorSubject<Message>(new Message());

  constructor() { }

  
}
