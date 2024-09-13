import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {
  @Input() post: any = {}

  comments = [
    {
      img: 'assets/images/profile.png',
      name: 'John Doe',
      comment: 'Nice post!',
      date: '3 hours ago',
      subComments: [],
      showSubComments: false,
      showReplyForm: false
    },
    {
      img: 'assets/images/profile.png',
      name: 'Ahmed Doe',
      comment: 'Nahhhhhh post!',
      date: '3 hours ago',
      subComments: [
        {
          img: 'assets/images/profile.png',
          name: 'Ahmed Doe',
          comment: 'Nahhhhhh post!',
          date: '3 hours ago',
          subComments: [
            {
              img: 'assets/images/profile.png',
              name: 'Deo John',
              comment: 'Blablalba post!',
              date: '3 hours ago',
              subComments: [],
              showSubComments: false,
              showReplyForm: false
            }
          ]
        },
        {
          img: 'assets/images/profile.png',
          name: 'Deo John',
          comment: 'Blablalba post!',
          date: '3 hours ago',
          subComments: [],
          showSubComments: false,
          showReplyForm: false
        }
      ]
    },
    {
      img: 'assets/images/profile.png',
      name: 'Deo John',
      comment: 'Blablalba post!',
      date: '3 hours ago',
      subComments: [],
      showSubComments: false,
      showReplyForm: false
    }
  ]

  isReactVisible = false;
  isCommentsVisible = false;
  toggleReact() {
    this.isReactVisible = !this.isReactVisible;
  }
  toggleSubComments(comment: any) {
    comment.showSubComments = !comment.showSubComments;
  }

  toggleComments(){
    this.isCommentsVisible = !this.isCommentsVisible;
  }

  addComment(comments:any, comment: any){
    comments.push({
      img: 'assets/images/profile.png',
      name: 'Imposter John',
      comment: comment,
      date: '3 hours ago',
      subComments: [],
      showSubComments: false,
      showReplyForm: false
    })
  }

}
