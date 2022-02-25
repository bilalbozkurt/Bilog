import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ServiceResponse } from 'src/shared/service-response.model';

@Injectable({
  providedIn: 'root'
})
export class SearchPageService {

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  SearchPosts(searchInput: string) {
    return this._http.get<ServiceResponse<any>>(this.baseUrl + environment.searchPosts + searchInput);
  }
}
