import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { NewPostComponent } from './components/new-post/new-post.component';
import { PostComponent } from './components/post/post.component';
import { CommentComponent } from './components/comment/comment.component';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { HeaderComponent } from './components/header/header.component';
import { AvatarComponent } from './components/avatar/avatar.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ChatsBoxComponent } from './components/chats-box/chats-box.component';
import { ReactComponent } from './components/react/react.component';
import { AddCommentComponent } from './components/add-comment/add-comment.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { BioComponent } from './components/bio/bio.component';
import { ProfileHeaderComponent } from './components/profile-header/profile-header.component';
import { LoginDataComponent } from './components/login-data/login-data.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SignupPageComponent } from './pages/signup-page/signup-page.component';
import { SignupInputComponent } from './components/signup-input/signup-input.component';
import { ReactionListComponent } from './components/reaction-list/reaction-list.component';


@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    HomeComponent,
    NewPostComponent,
    PostComponent,
    CommentComponent,
    SideNavComponent,
    HeaderComponent,
    AvatarComponent,
    ChatsBoxComponent,
    ReactComponent,
    AddCommentComponent,
    ProfileComponent,
    ProfileHeaderComponent,
    BioComponent,
    LoginDataComponent,
    LoginPageComponent,
    SignupPageComponent,
    SignupInputComponent,
    ReactionListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
