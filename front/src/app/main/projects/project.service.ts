import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {Project} from "../../Models/Project.model";

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  get(id: number): Observable<Project> {
    return this.http.get<Project>(`https://localhost:7252/api/Project/Get/${id}`)
  }
  getPage(id: number): Observable<Project[]> {
    return this.http.get<Project[]>(`https://localhost:7252/api/Project/GetPage/${id}`)
  }
  post(project: Project): Observable<Project> {
    return this.http.post<Project>('https://localhost:7252/api/Project/Post', project);
  }
  put(index: number, project: Project): Observable<Project> {
    return this.http.put<Project>(`https://localhost:7252/api/Project/Put/${index}`, project);
  }
  delete(index: number): Observable<any> {
    return this.http.delete<void>(`https://localhost:7252/api/Project/Delete/${index}`);
  }

}
