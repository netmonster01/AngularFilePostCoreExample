import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Project } from '../models/project';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  Project: Project;

  constructor(private http: HttpClient) { }

  postFile(file: File) {
    this.Project = new Project();
    this.Project.projectId = 1;
    this.Project.thumbnail = file;
    var data = new FormData();
    data.append("projectId", this.Project.projectId.toString());
    data.append("avatarImage", this.Project.thumbnail);
    //const httpOptions = {
    //  headers: new HttpHeaders({
    //    'Content-Type': 'application/x-www-form-urlencoded'
    //  })
    //};
    this.http.post('/api/FileUpload/UploadThubmnail', data).subscribe((data: any) => console.log(data));
  }
}
