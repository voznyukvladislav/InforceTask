import { Component } from '@angular/core';
import { Constants } from 'src/app/data/constants';
import { Message } from 'src/app/data/message';
import { MessageService } from 'src/app/services/message-service/message.service';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.css']
})
export class BodyComponent {

  messages: Message[] = [];

  constructor(private messageService: MessageService) {
    this.messageService.message.subscribe(
      newMessage => {
        this.messages.push(newMessage);
        setTimeout(() => {
          this.messages.shift();
        }, Constants.messageLifetime + 1000);
      }
    );
  }


  
}
