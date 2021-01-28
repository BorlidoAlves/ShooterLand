import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { AuthGuardService } from './helpers/auth-guard.service';
import { HomepageComponent } from './homepage/homepage.component';
import { LeaderboardSingleScoreComponent } from './leaderboard-single-score/leaderboard-single-score.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { LeaderboardSingleKillsComponent } from './leaderboard-single-kills/leaderboard-single-kills.component';
import { LeaderboardSingleRoundComponent } from './leaderboard-single-round/leaderboard-single-round.component';
import { AchievementListComponent } from './achievement-list/achievement-list.component';
import { AchievementAddComponent } from './achievement-add/achievement-add.component';
import { AdminGuardService } from './admin-guard.service';
import { EditPasswordComponent } from './edit-password/edit-password.component';
import { AllUsersComponent } from './all-users/all-users.component';
import { LeaderboardMultiComponent } from './leaderboard-multi/leaderboard-multi.component';

// if a route canActivate AuthGuardService that means that the user needs to be logged in to access that path
//if a route canActive AdminGuardService that means that it is exclusive to administrators

const routes: Routes = [
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'register',
    component:RegisterComponent
  },
  {
    path:'homepage',
    component:HomepageComponent,
    canActivate: [AuthGuardService],
  },
  {
    path:'reset',
    component:ForgotPasswordComponent,
  },
  {
    path: 'profile/:id',
    component: UserProfileComponent,
    canActivate: [AuthGuardService],
  },
  {
    path:'users',
    component:AllUsersComponent,
    canActivate:[AuthGuardService,AdminGuardService]
  },
  {
    path: 'leaderboards/score',
    component: LeaderboardSingleScoreComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'leaderboards/kills',
    component: LeaderboardSingleKillsComponent,
    canActivate:[AuthGuardService],
  },
  {
    path: 'leaderboards/round',
    component: LeaderboardSingleRoundComponent,
    canActivate:[AuthGuardService],
  },
  {
    path: 'leaderboards/multiplayer',
    component: LeaderboardMultiComponent,
    canActivate:[AuthGuardService],
  },
  {
    path: 'achievements',
    component: AchievementListComponent,
    canActivate:[AuthGuardService],
  },
  {
    path: 'achievements/add',
    component:AchievementAddComponent,
    canActivate:[AuthGuardService,AdminGuardService]
  },
  {
    path:'change-password/:id',
    component:EditPasswordComponent,
    canActivate:[AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
