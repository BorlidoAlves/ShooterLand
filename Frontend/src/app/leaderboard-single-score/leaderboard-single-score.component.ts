import { Component, OnInit } from '@angular/core';
import { RestService } from '../rest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-leaderboard-single-score',
  templateUrl: './leaderboard-single-score.component.html',
  styleUrls: ['./leaderboard-single-score.component.css']
})
export class LeaderboardSingleScoreComponent implements OnInit {
  scores: any = [];

  constructor(  public rest: RestService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    this.getScores();
  }

  getScores() {
    this.scores = [];
    this.rest.getScoreSingle().subscribe((data:{})=>{
      this.scores=data;
      console.log('Scores:', this.scores);
    });
  }

  goMulti(){
    this.router.navigate(["/leaderboards/multiplayer"]);
   
  }

  goKills(){
    this.router.navigate(["/leaderboards/kills"]);
  }
}
