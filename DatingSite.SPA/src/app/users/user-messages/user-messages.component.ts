import { Component, OnInit, Input } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit {
  @Input() recipientId: number;
  messages: Message[];
  newMessage: any = {};


  constructor(private userService: UserService,
              private authService: AuthService,
              private alertfiy: AlertifyService) { }

  ngOnInit() {
    this.loadMessages();
  }

  markAsReaded(id: number) {
    this.userService.markAsReaded(id, this.authService.decoderToken.nameid).subscribe();
}

  loadMessages() {
    const currentUserId = Number(this.authService.decoderToken.nameid);
    this.userService.getMessageThread(this.authService.decoderToken.nameid, this.recipientId)
    .pipe(
      tap(messages => {
        // tslint:disable-next-line: prefer-for-of
        for (let i = 0; i < messages.length; i++) {
          const recipientId = messages[i].recipientId;
          if (currentUserId === recipientId ) {
            this.userService.markAsReaded(messages[i].id, currentUserId).subscribe();
          }
        }
      })
    )
    .subscribe(messages => {
      this.messages = messages;
    }, error => {
      this.alertfiy.error(error);
    });
  }

  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.userService.sendMessage(this.authService.decoderToken.nameid, this.newMessage)
    .subscribe((message: Message) => {
    this.messages.unshift(message);
    this.newMessage.content = '';
  }, error => {
    this.alertfiy.error(error);
  });
  }
}
