import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Achievement } from '../models/Achievement';
import { RestService } from '../rest.service';

@Component({
  selector: 'app-achievement-list',
  templateUrl: './achievement-list.component.html',
  styleUrls: ['./achievement-list.component.css']
})
export class AchievementListComponent implements OnInit {
  achievs:any = [];
  role:string;

  constructor(public rest: RestService,private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    this.getAchievements();
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.role=currentUser.type
    console.log("Role:"+this.role);
  }

  getAchievements() {
    this.achievs = [];
    this.rest.getAchievements().subscribe((data:{})=>{
      this.achievs=data;
  
     
       
     
    });
  }

  delete(id){
    this.rest.deleteAchievement(id).subscribe(
      (res) => {
      
        console.log('Achievement deleted');
        window.location.reload();
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
