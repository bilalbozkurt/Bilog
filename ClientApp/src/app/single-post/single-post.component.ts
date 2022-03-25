import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'oidc-client';
import { BlogPost } from '../post-list/post-model';
import { SinglePostService } from './single-post.service';

@Component({
  selector: 'app-single-post',
  templateUrl: './single-post.component.html',
  styleUrls: ['./single-post.component.css']
})
export class SinglePostComponent implements OnInit {

  constructor(private _singlePostService: SinglePostService, private _route: ActivatedRoute) { }
  postLink: string = '';
  blogPost: BlogPost = {} as BlogPost;
  ngOnInit(): void {
    this._route.paramMap.subscribe((params) => {
      this.postLink = params.get('postLink') ?? '';
    })
    this.getAllBlogPosts();
  }

  getAllBlogPosts() {
    this._singlePostService.GetPostByLink(this.postLink).subscribe(
      (response) => {
        if (!response.success) {
        }
        else {
          this.blogPost = response.data;
        }
      }
    )
  }

}
