import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {ProjTask} from "../../Models/ProjTask.model";

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient) { }

  get(id: number): Observable<ProjTask> {
    return this.http.get<ProjTask>(`https://localhost:7252/api/Task/Get/${id}`)
  }
  getPage(id: number): Observable<ProjTask[]> {
    return this.http.get<ProjTask[]>(`https://localhost:7252/api/Task/GetPage/${id}`)
  }
  post(project: ProjTask): Observable<ProjTask> {
    return this.http.post<ProjTask>('https://localhost:7252/api/Task/Post', project);
  }
  put(index: number, project: ProjTask): Observable<ProjTask> {
    return this.http.put<ProjTask>(`https://localhost:7252/api/Task/Put/${index}`, project);
  }
  delete(index: number): Observable<any> {
    return this.http.delete<void>(`https://localhost:7252/api/Task/Delete/${index}`);
  }

}
