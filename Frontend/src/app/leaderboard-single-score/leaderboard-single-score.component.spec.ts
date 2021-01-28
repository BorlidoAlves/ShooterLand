import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderboardSingleScoreComponent } from './leaderboard-single-score.component';

describe('LeaderboardSingleScoreComponent', () => {
  let component: LeaderboardSingleScoreComponent;
  let fixture: ComponentFixture<LeaderboardSingleScoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardSingleScoreComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaderboardSingleScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
