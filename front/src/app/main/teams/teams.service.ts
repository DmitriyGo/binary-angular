import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {Team} from "../../Models/Team.model";

@Injectable({
  providedIn: 'root'
})
export class TeamsService {

  constructor(private http: HttpClient) { }

  get(id: number): Observable<Team> {
    return this.http.get<Team>(`https://localhost:7252/api/Team/Get/${id}`)
  }
  getPage(id: number): Observable<Team[]> {
    return this.http.get<Team[]>(`https://localhost:7252/api/Team/GetPage/${id}`)
  }
  post(project: Team): Observable<Team> {
    return this.http.post<Team>('https://localhost:7252/api/Team/Post', project);
  }
  put(index: number, project: Team): Observable<Team> {
    return this.http.put<Team>(`https://localhost:7252/api/Team/Put/${index}`, project);
  }
  delete(index: number): Observable<any> {
    return this.http.delete<void>(`https://localhost:7252/api/Team/Delete/${index}`);
  }

}
