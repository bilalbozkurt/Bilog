import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPost } from '../post-list/post-model';
import { SearchPageService } from '../search-page/search-page.service';

@Component({
  selector: 'app-hashtag-page',
  templateUrl: './hashtag-page.component.html',
  styleUrls: ['./hashtag-page.component.css']
})
export class HashtagPageComponent implements OnInit {
  searchText: string = "";
  blogPosts: BlogPost[] = [];

  constructor(private _searchPageService: SearchPageService, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._route.paramMap.subscribe((params) => {
      this.searchText = params.get('searchInput') ?? '';
      this.searchPosts();
    })
  }

  searchPosts() {
    if (this.searchText.length <= 0) {
      this.blogPosts = [];
      return;
    }
    this._searchPageService.SearchHashtags(this.searchText).subscribe((response) => {
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
