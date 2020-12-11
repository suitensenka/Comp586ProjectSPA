import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';


@Component({
  selector: 'app-board-detail',
  templateUrl: './board-detail.component.html',
  styleUrls: ['./board-detail.component.css']
})
export class BoardDetailComponent implements OnInit {
  private bid: number = 0;
  public board = {} as BoardInfoDetail;

  public commentForm=  new FormGroup({
    user : new FormControl(""),
    content : new FormControl("New Comment!"),
  });

  constructor(public auth: AuthService, public http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string, private route: ActivatedRoute, private router: Router) 
  {
    auth.user$.subscribe(result => this.commentForm.get('user')?.setValue(result.name));
  }

  ngOnInit(): void {
    this.bid = this.route.snapshot.params['id'];

    this.http.get<BoardInfoDetail>(this.baseUrl + 'api/Boards'+"/" + this.bid.toString()).subscribe(result =>{
      this.board = result;
      this.board.comments = result.comments;
    });
  }

  addNewComment()
  {
    var header = new HttpHeaders();
    header.append('content-type', 'application/json;charset=utf-8');

    var jsonData = {
      idBoard : Number(this.bid),
      content: this.commentForm.get("content")?.value,
      user: this.commentForm.get("user")?.value
    }

    this.http.post(this.baseUrl + 'api/Comments', jsonData, {headers : header}).subscribe(result => {
      //this.router.navigate(['/boardList/'+this.bid]);
      this.router.navigateByUrl('/', {skipLocationChange: true})
  .then(()=>this.router.navigate(['/boardList/'+this.bid]));
    
    },error => console.error(error));


  }

}

interface BoardInfoDetail{
  id: number;
  subject: string;
  content: string;
  owner:string;
  comments: CommentInfo[]
}

interface CommentInfo{
  id: number;
  content: string;
  idBoard: number;
  boardName: string;
  owner: string
}