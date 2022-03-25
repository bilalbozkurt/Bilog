import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ServiceResponse } from 'src/shared/service-response.model';
import { BlogPost } from './post-model';

@Injectable({
  providedIn: 'root'
})
export class PostListService {

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  GetAllPosts() {
    return this._http.get<ServiceResponse<BlogPost[]>>(this.baseUrl + environment.getAllPosts);
  }
}