import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from '@auth0/auth0-angular';
import { BoardListComponent } from './components/board-list/board-list.component';
import { BoardDetailComponent } from './components/board-detail/board-detail.component';
import { NewBoardComponent } from './components/new-board/new-board.component';

const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'profile', component: UserProfileComponent, canActivate : [AuthGuard]},
  {path: 'boardList', component: BoardListComponent, canActivate : [AuthGuard], 
    children: [{path: ':id', component: BoardDetailComponent, canActivate: [AuthGuard]}]
  },
  {path: 'newBoard', component : NewBoardComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

