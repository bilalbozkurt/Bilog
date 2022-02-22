import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NavMenuService } from './nav-menu.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  /**
   *
   */
  searchInput: string = "";
  constructor(private _router: Router) {

  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  search() {
    if (this.searchInput[0] == '#') {
      this.searchInput = this.searchInput.substring(1, this.searchInput.length);
      this._router.navigate(["/hashtag/" + this.searchInput]);
    }
    else {
      this._router.navigate(["/search/" + this.searchInput]);
    }
  }

}
