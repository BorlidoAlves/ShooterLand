import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './models/User';
import { Observable } from 'rxjs';
import { Score } from './models/Score';
import { Kills } from './models/Kills';
import { Round } from './models/Round';
import { Stats } from './models/Stats';
import { Achievement } from './models/Achievement';
import { Victory } from './models/Victories';

const endpoint = 'http://localhost:5000/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    
  }),
};


@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(private http: HttpClient) { }

  private extractData(res: Response) {
    let body = res;
    return body || {};
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(
      endpoint + 'User/register',
      JSON.stringify(user),
      httpOptions
    );
  }

  getAllUsers():Observable<User[]>{
    return this.http.get<User[]>(
      endpoint+'User/AllUsers',
    );
  }

  userInfo(id:String):Observable<User>{
    return this.http.get<User>(endpoint+'Information/'+id);
  }

  deleteUser(id: string): Observable<User> {
    return this.http.delete<User>(endpoint + 'User/RemoveUser/' + id, httpOptions);
  }

  getScoreSingle(): Observable<Score[]> {
    return this.http.get<Score[]>(endpoint + 'GameStats/LeaderboardScoresSingleplayer');
  }

  getKillsSingle(): Observable<Kills[]> {
    return this.http.get<Kills[]>(endpoint + 'GameStats/LeaderboardKillsSingleplayer');
  }
  
  getHighestRoundSingle(): Observable<Round[]> {
    return this.http.get<Round[]>(endpoint + 'GameStats/LeaderboardRoundsSingleplayer');
  }

  getMultiVictories():Observable<Victory[]>{
    return this.http.get<Victory[]>(endpoint+"GameStats/LeaderboardMultiplayer");
  }

 getUserStats(id:String):Observable<Stats>{
   return this.http.get<Stats>(endpoint+"User/Stats/"+id);
 }

  resetPassword(email: String): Observable<any> {
    console.log("Type:" ,typeof(email));
    console.log("THE RECEIVED EMAIL IS: ", email);
    return this.http.post<any>(
      endpoint + 'User/ForgetPassword',
      JSON.stringify({"Email":email}),
      httpOptions
    );
  }

  changePassword(id: String, password: String): Observable<any> {
    return this.http.put<any>( 
      endpoint + 'User/UpdatePassword/' + id ,
      JSON.stringify({"Password":password}),
      httpOptions
    );
  }

  createAchievement(ach:any):Observable<Achievement>{
    return this.http.post<Achievement>(
      endpoint + 'Achievement/Create',
      ach
    );
  }

  getAchievements():Observable<Achievement[]>{
    return this.http.get<Achievement[]>(endpoint+"Achievement/GetAll");
  }

 
  deleteAchievement(id: string): Observable<Achievement> {
    return this.http.delete<Achievement>(endpoint + 'Achievement/Delete/' + id, httpOptions);
  }

  getUserAchievements(id:String):Observable<Achievement[]>{
    return this.http.get<Achievement[]>(endpoint+'User/UserAchievements/'+id);
  }
}
