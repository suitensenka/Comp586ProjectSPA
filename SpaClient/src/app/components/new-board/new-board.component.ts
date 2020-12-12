import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Content } from '@angular/compiler/src/render3/r3_ast';
import { Component, Inject, OnInit } from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-new-board',
  templateUrl: './new-board.component.html',
  styleUrls: ['./new-board.component.css']
})
export class NewBoardComponent{

  public boardForm =  new FormGroup({
    user : new FormControl(""),
    subject : new FormControl("New Topic!"),
    content : new FormControl("New Content"),
  });

  constructor(public auth: AuthService, public http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string, private router: Router) {
    auth.user$.subscribe(result => this.boardForm.get('user')?.setValue(result.name));
   };

  addNewBoard()
  {
    if(this.boardForm.get("subject")?.value.trim() == "")
    {
      this.boardForm.get("subject")?.setValue("Default Name: New Topic!");
    }

    if(this.boardForm.get("content")?.value.trim() == "")
    {
      this.boardForm.get("content")?.setValue("Default Content: New Content!");
    }

    var header = new HttpHeaders();
    header.append('content-type', 'application/json;charset=utf-8');

    this.http.post(this.baseUrl + 'api/Boards', this.boardForm.getRawValue(), {headers : header}).subscribe(result => {
      this.reset();
      this.router.navigate(['/boardList']);
    
    },error => console.error(error));
  }

  reset()
  {
    this.boardForm.get("subject")?.setValue("New Topic!");
    this.boardForm.get("content")?.setValue("New Content!");
  }

  




}
