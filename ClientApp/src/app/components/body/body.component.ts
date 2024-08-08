import { Component } from '@angular/core';
import { Message } from 'src/app/data/message';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {

  messages: Message[] = [];

  constructor() {
    
  }

  
}
