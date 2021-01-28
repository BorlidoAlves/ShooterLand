import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-leaderboard-multi',
  templateUrl: './leaderboard-multi.component.html',
  styleUrls: ['./leaderboard-multi.component.css']
})
export class LeaderboardMultiComponent implements OnInit {
  victories: any = [];
  
  constructor(public rest: RestService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    this.getVictories();
  }

  getVictories() {
    this.victories = [];
    this.rest.getMultiVictories().subscribe((data:{})=>{
      this.victories=data;
      
     
    });
  }

  goScore(){
    this.router.navigate(["/leaderboards/score"]);
  }

  
  goRounds(){
    this.router.navigate(["/leaderboards/round"]);
   
  }
}
