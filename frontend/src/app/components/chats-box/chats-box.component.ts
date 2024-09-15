import { Component } from '@angular/core';

@Component({
  selector: 'app-chats-box',
  templateUrl: './chats-box.component.html',
  styleUrls: ['./chats-box.component.css']
})
export class ChatsBoxComponent {
  users = [
    {
      name:"Ahmed Mubarak",
      img: "assets/images/profile.png",
      id:1
    },
    {
      name:"Ahmed Mubarak 2",
      img: "assets/images/profile.png",
      id:2
    },
    {
      name:"Ahmed Mubarak 3",
      img: "assets/images/profile.png",
      id:3
    },
    {
      name:"Ahmed Mubarak 4",
      img: "assets/images/profile.png",
      id:4
    },
    {
      name:"Ahmed Mubarak 5",
      img: "assets/images/profile.png",
      id:5
    },
    {
      name:"Ahmed Mubarak 6",
      img: "assets/images/profile.png",
      id:6
    }
  ]
}
