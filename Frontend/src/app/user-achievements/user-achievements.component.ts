import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Achievement } from '../models/Achievement';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-user-achievements',
  templateUrl: './user-achievements.component.html',
  styleUrls: ['./user-achievements.component.css']
})
export class UserAchievementsComponent implements OnInit {
  achievs:any = [];
  empty:any

  constructor(public rest: RestService,
    private route: ActivatedRoute,
    private router: Router,) { }

  ngOnInit(): void {
    var idTemp = this.route.snapshot.params['id'];
    this.getAchievements(idTemp);
  }

  
  getAchievements(idTemp) {
    this.achievs = [];
    this.rest.getUserAchievements(idTemp).subscribe((data:{})=>{
      this.achievs=data;
      console.log("CONQUISTAS:",this.achievs);
      if(this.achievs.length==0){
        this.empty=true;
        
      }
      console.log("EMPTY? ",this.empty);
     
    });
  }

}
