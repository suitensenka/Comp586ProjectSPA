import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-board-list',
  templateUrl: './board-list.component.html',
  styleUrls: ['./board-list.component.css']
})
export class BoardListComponent {

  public boardInfo:  BoardInfo[] = [];

  constructor(public auth: AuthService, public http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string, private router: Router, public route: ActivatedRoute) 
  {
    http.get<BoardInfo[]>(this.baseUrl + 'api/Boards').subscribe(result =>{
      this.boardInfo = result;
    });
  }

  ngOnInit(): void {
  }

  routeToBoardId(id: number){
    this.router.navigate(['/boardList/' + id]);
  }

  refreshPage()
  {
    this.router.navigateByUrl('/', {skipLocationChange: true})
  .then(()=>this.router.navigate(['/boardList']));
  }

}

interface BoardInfo{
  id: number;
  subject: string;
  content: string;
  owner:string
}

interface CommentInfo{
  id: number;
  content: string;
  idBoard: number;
  boardName: string;
  owner: string
}