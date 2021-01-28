import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderboardMultiComponent } from './leaderboard-multi.component';

describe('LeaderboardMultiComponent', () => {
  let component: LeaderboardMultiComponent;
  let fixture: ComponentFixture<LeaderboardMultiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaderboardMultiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaderboardMultiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
