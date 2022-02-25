import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
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
  constructor(private _router: Router, private _sanitizer: DomSanitizer) {

  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  search() {
    let retFlag = false;
    let notAllowedChars = ['!', ',', '=', '-', '<', '>', '*', '/']
    notAllowedChars.forEach(element => { // A very advanced super-duper sanitizer
      if (this.searchInput.indexOf(element) != -1) {
        retFlag = true;
      }
    });

    if (retFlag) {
      return;
    }
    else {
      if (this.searchInput[0] == '#') {
        this.searchInput = this.searchInput.substring(1, this.searchInput.length);
        this._router.navigate(["/hashtag/" + this.searchInput]);
      }
      else {
        this._router.navigate(["/search/" + this.searchInput]);
      }
    }


  }

}
