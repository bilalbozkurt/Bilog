import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPost } from '../post-list/post-model';
import { SearchPageService } from '../search-page/search-page.service';
import { HashtagPageService } from './hashtag-page.service';

@Component({
  selector: 'app-hashtag-page',
  templateUrl: './hashtag-page.component.html',
  styleUrls: ['./hashtag-page.component.css']
})
export class HashtagPageComponent implements OnInit {
  searchText: string = "";
  blogPosts: BlogPost[] = [];

  constructor(private _hashtagPageService: HashtagPageService, private _route: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this._route.paramMap.subscribe((params) => {
      this.searchText = params.get('searchInput') ?? '';
      let retFlag = false;
      let notAllowedChars = ['!', ',', '=', '-', '<', '>', '*', '/']
      notAllowedChars.forEach(element => { // A very advanced super-duper sanitizer
        if (this.searchText.indexOf(element) != -1) {
          retFlag = true;
        }
      });

      if (retFlag) {
        this._router.navigate(["/"]);
        return;
      }
      else {
        this.searchPosts();
      }
    })
  }

  searchPosts() {
    if (this.searchText.length <= 0) {
      this.blogPosts = [];
      return;
    }
    this._hashtagPageService.SearchHashtags(this.searchText).subscribe((response) => {
      if (response.success) {
        this.blogPosts = response.data;
      }
      else {
        this.blogPosts = [];
        console.log(response.message);
      }
    })
  }
}
