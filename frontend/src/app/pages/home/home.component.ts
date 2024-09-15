import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  posts: Post[] = [
    {
      name:"Ahmed Mubarak",
      react:1,
      reactions:{
        1:1,
        2:0,
        3:0,
        4:0,
        5:0
      },
      date: "two hours ago",
      content:"im new hereeeeee",
      image:"assets/images/post-img.png",
      profile:"assets/images/profile.png",
      comments: [
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
    
    },
    {
      react:2,
      name:"Hacker Two",
      reactions:{
        1:0,
        2:1,
        3:0,
        4:0,
        5:0
      },
      date: "two minute ago",
      content:"im gonna hack this website",
      image:"assets/images/Login.jpeg",
      profile:"assets/images/wow.png",
      comments: [
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
    
    }
  ]

  handleNewPost(postData: any) {
    const post = {
      name:"Ahmed Mubarak",
      react:null,
      reactions:{
        1:0,
        2:0,
        3:0,
        4:0,
        5:0
      },
      date: "two hours ago",
      content:postData.content,
      image:postData.image,
      profile:"assets/images/profile.png",
      comments: []
    }

    this.posts.unshift(post);
  }
  constructor(private postService: PostService) { }
  
  ngOnInit(): void {
    this.postService.getPosts().subscribe((res: any) => {
      this.posts = [...res]
      console.log(res);
      
    })
    
  }
}
