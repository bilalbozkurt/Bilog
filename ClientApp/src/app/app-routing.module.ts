import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SinglePostComponent } from './single-post/single-post.component';
import { SearchPageComponent } from './search-page/search-page.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'post/:postLink', component: SinglePostComponent },
  { path: 'search/:searchInput', component: SearchPageComponent },
]
@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
