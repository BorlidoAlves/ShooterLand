import { Component, OnInit } from '@angular/core';
import { RestService } from '../rest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-leaderboard-single-kills',
  templateUrl: './leaderboard-single-kills.component.html',
  styleUrls: ['./leaderboard-single-kills.component.css']
})
export class LeaderboardSingleKillsComponent implements OnInit {
  kills: any = [];

  constructor(  public rest: RestService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    this.getKills();
  }

  getKills() {
    this.kills = [];
    this.rest.getKillsSingle().subscribe((data:{})=>{
      this.kills=data;
     
    });
  }

  goRounds(){
    this.router.navigate(["/leaderboards/round"]);
   
  }

  goScore(){
    this.router.navigate(["/leaderboards/score"]);
  }

}
