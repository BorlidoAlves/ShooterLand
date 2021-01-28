import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AchievementAddComponent } from './achievement-add.component';

describe('AchievementAddComponent', () => {
  let component: AchievementAddComponent;
  let fixture: ComponentFixture<AchievementAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AchievementAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AchievementAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


});
