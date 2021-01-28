import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderboardSingleRoundComponent } from './leaderboard-single-round.component';

describe('LeaderboardSingleRoundComponent', () => {
  let component: LeaderboardSingleRoundComponent;
  let fixture: ComponentFixture<LeaderboardSingleRoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardSingleRoundComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaderboardSingleRoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
