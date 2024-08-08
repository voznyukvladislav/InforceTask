import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { Constants } from 'src/app/data/constants';
import { Message } from 'src/app/data/message';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements AfterViewInit {
  @Input() message: Message = new Message();
  @ViewChild("messageElement") messageElement!: ElementRef;

  successful: string = Constants.successful;
  failed: string = Constants.failed;

  ngAfterViewInit(): void {
    if (this.messageElement) {
      setTimeout(() => {
        this.messageElement.nativeElement.style.opacity = "1";
        this.messageElement.nativeElement.style.scale = "1";

        setTimeout(() => {
          this.messageElement.nativeElement.style.opacity = "0";
          this.messageElement.nativeElement.style.scale = "0";
        }, Constants.messageLifetime);
      });
    }
  }

}
