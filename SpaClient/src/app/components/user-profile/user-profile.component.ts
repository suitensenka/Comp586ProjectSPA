import { Component, OnInit} from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Auth0ClientService } from '@auth0/auth0-angular/lib/auth.client';
import { Inject } from '@angular/core';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  public userInfo = {} as UserInfo;

  constructor(public auth: AuthService, public http: HttpClient, @Inject('BASE_API_URL') private baseUrl: string) 
  {
    //this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
    var userName = "";

    this.auth.user$.subscribe( r =>{userName = r.name;
      console.log(this.baseUrl + 'api/Users/'+ userName);

      http.get<UserInfo>(this.baseUrl + 'api/Users/'+ userName).subscribe(result =>{
        this.userInfo = result;
        this.userInfo.boards = result.boards;
        this.userInfo.comments = result.comments;
      
        }, error => console.error(error));
    });
    
  }

  //This method is not in used right now.
  public updateUserToDataBase()
  {
    var userName = "";
    this.auth.user$.subscribe( r =>{userName = r.name;
      console.log(this.baseUrl + 'api/Users/'+ userName);
      var data = {
        user: userName,
        username: userName,
        role: "user"
      };
      var jsonData = JSON.stringify(data);
      console.log(jsonData);
      
      this.http.post(this.baseUrl + 'api/Users', jsonData);

    });
  }

}


interface UserInfo {
  user:string;
  username: string;
  boards : BoardInfo[];
  comments:  CommentInfo[]
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