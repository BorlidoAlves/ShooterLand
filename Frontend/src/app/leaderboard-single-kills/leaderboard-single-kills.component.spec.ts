import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderboardSingleKillsComponent } from './leaderboard-single-kills.component';

describe('LeaderboardSingleKillsComponent', () => {
  let component: LeaderboardSingleKillsComponent;
  let fixture: ComponentFixture<LeaderboardSingleKillsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardSingleKillsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaderboardSingleKillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

 
});
