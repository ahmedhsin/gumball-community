import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-reaction-list',
  templateUrl: './reaction-list.component.html',
  styleUrls: ['./reaction-list.component.css']
})
export class ReactionListComponent {
  @Input() reactions: any;

  getReacts(){
    return Object.keys(this.reactions);
  }
}
