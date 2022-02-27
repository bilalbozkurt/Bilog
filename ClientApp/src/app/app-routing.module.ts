import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SinglePostComponent } from './single-post/single-post.component';
import { SearchPageComponent } from './search-page/search-page.component';
import { HashtagPageComponent } from './hashtag-page/hashtag-page.component';
import { ContactComponent } from './contact/contact.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'post/:postLink', component: SinglePostComponent },
  { path: 'search/:searchInput', component: SearchPageComponent },
  { path: 'hashtag/:searchInput', component: HashtagPageComponent },
  { path: 'contact', component: ContactComponent },


]
@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
