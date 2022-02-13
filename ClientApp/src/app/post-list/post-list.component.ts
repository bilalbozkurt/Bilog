import { Component, OnInit } from '@angular/core';
import { PostListService } from './post-list.service';
import { BlogPost } from './post-model';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  constructor(private _postListService: PostListService) { }
  blogPosts: BlogPost[] = [];
  ngOnInit(): void {
    this.getAllBlogPosts();
  }

  getAllBlogPosts() {
    this._postListService.GetAllPosts().subscribe(
      (response) => {
        console.log(response)
        if (!response.success) {
          console.log(response.message);
        }
        else {
          console.log(response.data);
          this.blogPosts = response.data;
        }
      }
    )
  }
}
