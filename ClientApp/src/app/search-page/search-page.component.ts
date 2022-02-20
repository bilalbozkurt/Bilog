import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPost } from '../post-list/post-model';
import { SearchPageService } from './search-page.service';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent implements OnInit {
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
    console.log(this.searchText);
    if (this.searchText.length <= 0) {
      return;
    }

    this._searchPageService.SearchPosts(this.searchText).subscribe((response) => {
      if (response.success) {
        this.blogPosts = response.data;
        console.log(response.data);
      }
      else {
        console.log(response.message);
      }
    })
  }
}
