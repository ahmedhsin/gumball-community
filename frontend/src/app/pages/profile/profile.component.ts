import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  author: any;
  userDetails: any;
  posts: any[] = [];

  private mockUser = {
    name: 'John Doe',
    username: '@johndoe',
    image: 'assets/images/profile.png'
  };

  private mockUserDetails = {
    LiveIn: 'Cairo',
    BirthDate: 'January 1, 1990',
    Followers: 1200,
    Following: 300
  };

  private mockPosts = [
    {
      id: 1,
      content: 'This is the first mock post content.',
      image: 'assets/images/logo.png',
      author: {
        name: 'John Doe',
        username: '@johndoe',
        profile: 'assets/images/profile.png'
      },
      reactions: {},
      react: null,
      comments: [
        {
          img: 'assets/images/profile.png',
          name: 'Commenter 1',
          comment: 'This is a comment on the first post.',
          date: '2 hours ago',
          subComments: [],
          showSubComments: false,
          showReplyForm: false
        }
      ]
    },
    {
      id: 2,
      content: 'This is the second mock post content with some more details.',
      image: 'assets/images/post-img2.png',
      author: {
        name: 'John Doe',
        username: '@johndoe',
        profile: 'assets/images/profile.png'
      },
      reactions: {},
      react: null,
      comments: [
        {
          img: 'assets/images/profile.png',
          name: 'Commenter 2',
          comment: 'Another comment, this time on the second post.',
          date: '5 hours ago',
          subComments: [],
          showSubComments: false,
          showReplyForm: false
        }
      ]
    }
  ];

  constructor(private authService: AuthService) {}

  ngOnInit(): void {


    this.author = this.mockUser;
    this.userDetails = this.mockUserDetails;
    this.posts = this.mockPosts;
    // Uncomment these lines if you need to fetch other data
    /*
    this.authService.getPosts(userId).subscribe(data => {
      this.posts = data;
    });


    this.authService.getAuthor(userId).subscribe(data => {
      this.author = data;
    });

    this.authService.getUserDetails(userId).subscribe(data => {
      this.userDetails = data;
    });
    */

  }
  handleNewPost(newPost: any) {
    newPost.author = {
      name: this.author.name,  // Assign the author's name
      username: this.author.username,  // Assign the author's username
      profileImage: this.author.image  // Assign the author's profile image
    };

    this.posts.unshift(newPost);  // Add new post to the list
  }
}
