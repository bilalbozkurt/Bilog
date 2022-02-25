import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private _searchPageService: SearchPageService, private _route: ActivatedRoute, private _router: Router) { }

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

    this._searchPageService.SearchPosts(this.searchText).subscribe((response) => {
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
