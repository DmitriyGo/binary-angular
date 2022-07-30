import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {User} from "../../Models/User.model";

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  get(id: number): Observable<User> {
    return this.http.get<User>(`https://localhost:7252/api/User/Get/${id}`)
  }
  getPage(id: number): Observable<User[]> {
    return this.http.get<User[]>(`https://localhost:7252/api/User/GetPage/${id}`)
  }
  post(project: User): Observable<User> {
    return this.http.post<User>('https://localhost:7252/api/User/Post', project);
  }
  put(index: number, project: User): Observable<User> {
    return this.http.put<User>(`https://localhost:7252/api/User/Put/${index}`, project);
  }
  delete(index: number): Observable<any> {
    return this.http.delete<void>(`https://localhost:7252/api/User/Delete/${index}`);
  }

}
