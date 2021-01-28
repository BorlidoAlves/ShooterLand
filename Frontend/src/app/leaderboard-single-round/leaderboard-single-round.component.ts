import { Component, OnInit } from '@angular/core';
import { RestService } from '../rest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-leaderboard-single-round',
  templateUrl: './leaderboard-single-round.component.html',
  styleUrls: ['./leaderboard-single-round.component.css']
})
export class LeaderboardSingleRoundComponent implements OnInit {
  highestRounds: any = [];

  constructor(  public rest: RestService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    this.getHighestRound();
  }

  getHighestRound() {
    this.highestRounds = [];
    this.rest.getHighestRoundSingle().subscribe((data:{})=>{
      this.highestRounds=data;
      console.log("ROUNDS:",this.highestRounds);
    });
  }

  goKills(){
    this.router.navigate(["/leaderboards/kills"]);
   
  }

  goMulti(){
    this.router.navigate(["/leaderboards/multiplayer"]);
  }

}
