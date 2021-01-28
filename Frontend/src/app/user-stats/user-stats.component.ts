import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Stats } from '../models/Stats';
import { RestService } from '../rest.service';


@Component({
  selector: 'app-user-stats',
  templateUrl: './user-stats.component.html',
  styleUrls: ['./user-stats.component.css']
})
export class UserStatsComponent implements OnInit {
  stats:Stats;


  constructor(  public rest: RestService,
    private route: ActivatedRoute,
    private router: Router,) { }

  ngOnInit(): void {
    var idTemp = this.route.snapshot.params['id'];
    this.getStats(idTemp);
  }

  getStats(idTemp) {
    this.rest.getUserStats(idTemp).subscribe((data: Stats) => {
      this.stats = data;
      console.log("ESTATISTICAS:",this.stats);
    });
  }
}
