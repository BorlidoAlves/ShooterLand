import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { HomepageComponent } from './homepage/homepage.component';
import { JWTInterceptorService } from './helpers/jwt-interceptor.service';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LeaderboardSingleScoreComponent } from './leaderboard-single-score/leaderboard-single-score.component';
import { LeaderboardSingleKillsComponent } from './leaderboard-single-kills/leaderboard-single-kills.component';
import { LeaderboardSingleRoundComponent } from './leaderboard-single-round/leaderboard-single-round.component';
import { AchievementAddComponent } from './achievement-add/achievement-add.component';
import { AchievementListComponent } from './achievement-list/achievement-list.component';
import { UserAchievementsComponent } from './user-achievements/user-achievements.component';
import { UserStatsComponent } from './user-stats/user-stats.component';
import { EditPasswordComponent } from './edit-password/edit-password.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { LeaderboardMultiComponent } from './leaderboard-multi/leaderboard-multi.component';
import{ReactiveFormsModule} from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationDeleteComponent } from './confirmation-delete/confirmation-delete.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    HomepageComponent,
    UserProfileComponent,
    ForgotPasswordComponent,
    LeaderboardSingleScoreComponent,
    LeaderboardSingleKillsComponent,
    LeaderboardSingleRoundComponent,
    AchievementAddComponent,
    AchievementListComponent,
    UserAchievementsComponent,
    UserStatsComponent,
    EditPasswordComponent,
    AllUsersComponent,
    LeaderboardMultiComponent,
    ConfirmationDeleteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NoopAnimationsModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [ 
    {
    provide: HTTP_INTERCEPTORS,
    useClass: JWTInterceptorService,
    multi: true,
  },
],
  bootstrap: [AppComponent]
})
export class AppModule { }
