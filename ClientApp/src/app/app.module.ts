import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PostListComponent } from './post-list/post-list.component';
import { SinglePostComponent } from './single-post/single-post.component';
import { AppRoutingModule } from './app-routing.module';
import { SearchPageComponent } from './search-page/search-page.component';
import { HashtagPageComponent } from './hashtag-page/hashtag-page.component';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';

// Localization. To change to another language register the language.
// example for Spanish,
// import localeEs from '@angular/common/locales/es';
// registerLocaleData(localeEs, 'es'); 
// then use it at your pipe
// {{ dateVariable | date:'longDate':'':'es'}}

registerLocaleData(localeTr, 'tr'); 

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PostListComponent,
    SinglePostComponent,
    SearchPageComponent,
    HashtagPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
