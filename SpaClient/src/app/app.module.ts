import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';

import { LoginButtonComponent } from './components/login-button/login-button.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { MainNavComponent } from './components/main-nav/main-nav.component';
import { AuthModule } from "@auth0/auth0-angular";
import { environment as env} from "../environments/environment";
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { HomeComponent } from './components/home/home.component';
import { BoardListComponent } from './components/board-list/board-list.component';
import { BoardDetailComponent } from './components/board-detail/board-detail.component';
import { NewBoardComponent } from './components/new-board/new-board.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginButtonComponent,
    NavBarComponent,
    MainNavComponent,
    UserProfileComponent,
    HomeComponent,
    BoardListComponent,
    BoardDetailComponent,
    NewBoardComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot({ 
      // The domain and clientId were configured in the previous chapter
      domain: 'dev--wzskmtv.us.auth0.com',
      clientId: 'VVtHsDYNCUUmOY67lQaWDt1HX5CUQq0b',
      audience: 'SpaServer586',
      httpInterceptor: {
        allowedList: [{uri:'https://comp586spaserver.com/*'}],
       //allowedList: env.httpInterceptor.allowedList,
        
      },
    }),

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true },
    { provide: "BASE_API_URL", useValue: env.dev.apiUrl},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
