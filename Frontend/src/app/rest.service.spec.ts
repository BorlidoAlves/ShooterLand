import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { RestService } from './rest.service';

describe('RestService', () => {
  let service: RestService;
  let backend: HttpTestingController;
  const urlApi = 'http://localhost:5000/';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RestService]
    });
    service = TestBed.inject(RestService);
    backend = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    backend.verify();
  });

  describe('#getUserStats', () => {
    it('should return an Observable<Stats>', () => {
      const dummyStat =
      {
        TotalMonstersKilled: 12,
        TotalGamesPlayed: 1,
        SingleplayerHighestMonstersKilled: 12,
        SingleplayerGamesPlayed: 1,
        SingleplayerHighestRound: 1,
        SingleplayerHighestScore: 1200,
        MultiplayerWins: 0,
        MultiplayerDefeats: 0,
        MultiplayerWinRate: 0,
        MultiplayerGamesPlayed: 0
      };

      service.getUserStats("2014").subscribe(stat => {
        expect(stat).toEqual(dummyStat);
      });

      const req = backend.expectOne(`${urlApi}User/Stats/2014`);
      expect(req.request.method).toBe("GET");
      req.flush(dummyStat);
    })
  });

  describe('#getAllUsers', () => {
    it('should return an Observable<User[]>', () => {
      const dummyUsers =
        [{
          Id: 0,
          Username: "dumy",
          FirstName: "dumy",
          LastName: "Dummy",
          Password: "ewqewqeqw",
          Email: "sadasdasd"
        },
        {
          Id: 1,
          Username: "dummy",
          FirstName: "dumy",
          LastName: "Dummy",
          Password: "ewsqewqeqw",
          Email: "sadasdsasd"
        }];

      service.getAllUsers().subscribe(users => {
        expect(users).toEqual(dummyUsers);
        expect(users.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}User/AllUsers`);
      expect(req.request.method).toBe("GET");
      req.flush(dummyUsers);
    })
  });

  describe('#userInfo', () => {
    it('should return an Observable<User>', () => {
      const dummyUser =
      {
        Id: 0,
        Username: "dumy",
        FirstName: "dumy",
        LastName: "Dummy",
        Password: "ewqewqeqw",
        Email: "sadasdasd"
      };


      service.userInfo("2014").subscribe(user => {
        expect(user).toEqual(dummyUser);
      });

      const req = backend.expectOne(`${urlApi}Information/2014`);
      expect(req.request.method).toBe("GET");
      req.flush(dummyUser);
    })
  });

  describe('#getKillsSingle', () => {
    it('should return an Observable<Kills[]>', () => {
      const killsDumy =
        [{
          Username: "King",
          Kills: 123
        },
        {
          Username: "Kin",
          Kills: 23
        }];

      service.getKillsSingle().subscribe(info => {
        expect(info).toEqual(killsDumy);
        expect(info.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}GameStats/LeaderboardKillsSingleplayer`);
      expect(req.request.method).toBe("GET");
      req.flush(killsDumy);
    })
  });

  describe('#getScoreSingle', () => {
    it('should return an Observable<Score[]>', () => {
      const ScoreDumy =
        [{
          Username: "King",
          Score: 123
        },
        {
          Username: "Kin",
          Score: 23
        }];

      service.getScoreSingle().subscribe(info => {
        expect(info).toEqual(ScoreDumy);
        expect(info.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}GameStats/LeaderboardScoresSingleplayer`);
      expect(req.request.method).toBe("GET");
      req.flush(ScoreDumy);
    })
  });

  describe('#getHighestRoundSingle', () => {
    it('should return an Observable<Round[]>', () => {
      const RoundDumy =
        [{
          Username: "King",
          rounds: 10
        },
        {
          Username: "Kin",
          rounds: 2
        }];

      service.getHighestRoundSingle().subscribe(info => {
        expect(info).toEqual(RoundDumy);
        expect(info.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}GameStats/LeaderboardRoundsSingleplayer`);
      expect(req.request.method).toBe("GET");
      req.flush(RoundDumy);
    })
  });

  describe('#getMultiVictories', () => {
    it('should return an Observable<Victory[]>', () => {
      const VictoryDumy =
        [{
          Id: 210,
          Username: "Adsd",
          Wins: 1,
          defeats: 1,
          WinRate: 50
        },
        {
          Id: 211,
          Username: "adadd",
          Wins: 2,
          defeats: 2,
          WinRate: 50
        }];

      service.getMultiVictories().subscribe(info => {
        expect(info).toEqual(VictoryDumy);
        expect(info.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}GameStats/LeaderboardMultiplayer`);
      expect(req.request.method).toBe("GET");
      req.flush(VictoryDumy);
    })
  });

  describe('#getAchievements', () => {
    it('should return an Observable<Achievement[]>', () => {
      const AchievDumy =
        [{
          IdAchievement: 12,
          Description: "Kills 200",
          Type: "Kills",
          Value: "200",
          MedalFile: "Medal.png",
          Medal: "Medal.png"
        },
        {
          IdAchievement: 13,
          Description: "Score 200",
          Type: "Score",
          Value: "200",
          MedalFile: "Medal.png",
          Medal: "Medal.png"
        }];

      service.getAchievements().subscribe(info => {
        expect(info).toEqual(AchievDumy);
        expect(info.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}Achievement/GetAll`);
      expect(req.request.method).toBe("GET");
      req.flush(AchievDumy);
    })
  });

  describe('#addUser', () => {
    it('should post the right data', () => {
      const UserDumy =
      {
        Id: 2014,
        Username: "BorlidoAlves",
        FirstName: "André",
        LastName: "Alves",
        Password: "asdasd",
        Email: "afb.alves@hotmail.com"
      };


      service.addUser(UserDumy).subscribe(info => {
        expect(info.Id).toEqual(2014);
        expect(info.Username).toEqual("BorlidoAlves");
      });

      const req = backend.expectOne(`${urlApi}User/register`);
      expect(req.request.method).toBe("POST");
      req.flush(UserDumy);
    })
  });

  describe('#changePassword', () => {
    it('should put the right data', () => {

      service.changePassword("12", "asdasd").subscribe((data: any) => {
        expect(data.password).toEqual("asdasd")
      });

      const req = backend.expectOne(`${urlApi}User/UpdatePassword/12`);
      expect(req.request.method).toBe("PUT");
      req.flush({ password: 'asdasd' });

    })
  });

  describe('#resetPassword', () => {
    it('should post the right data', () => {

      service.resetPassword("afb.alves@hotmail.com").subscribe((data: any) => {
        expect(data.email).toEqual("afb.alves@hotmail.com");

      });

      const req = backend.expectOne(`${urlApi}User/ForgetPassword`);
      expect(req.request.method).toBe("POST");
      req.flush({ email: "afb.alves@hotmail.com" });
    })
  });

  describe('#createAchievement', () => {
    it('should post the right data', () => {
      const DumyAchiev =
      {
        IdAchievement: 1,
        Description: "kill 200 Monsters in one Game",
        Type: "Kills",
        Value: "200",
        MedalFile: "medal.png",
        Medal: "medal.png"
      };
      service.createAchievement(DumyAchiev).subscribe((data: any) => {
        expect(data.Type).toEqual("Kills");

      });

      const req = backend.expectOne(`${urlApi}Achievement/Create`);
      expect(req.request.method).toBe("POST");
      req.flush(DumyAchiev);
    })
  });

  describe('#deleteAchievement', () => {
    it('should Delete the right data', () => {
      const Id = "1";
      service.deleteAchievement(Id).subscribe((data: any) => {
        expect(data.Id).toEqual("1");

      });

      const req = backend.expectOne(`${urlApi}Achievement/Delete/${Id}`);
      expect(req.request.method).toBe("DELETE");
      req.flush({Id:"1"});
    })
  });

  describe('#deletUser', () => {
    it('should Delete the right data', () => {
      const Id = "1";
      service.deleteUser(Id).subscribe((data: any) => {
        expect(data.Id).toEqual("1");

      });

      const req = backend.expectOne(`${urlApi}User/RemoveUser/${Id}`);
      expect(req.request.method).toBe("DELETE");
      req.flush({Id:"1"});
    })
  });

  describe('#getUserAchievements', () => {
    it('should return an Observable<Achievement[]>', () => {
      const AchievDumy =
        [{
          IdAchievement: 12,
          Description: "Kills 200",
          Type: "Kills",
          Value: "200",
          MedalFile: "Medal.png",
          Medal: "Medal.png"
        },
        {
          IdAchievement: 13,
          Description: "Score 200",
          Type: "Score",
          Value: "200",
          MedalFile: "Medal.png",
          Medal: "Medal.png"
        }];

      service.getUserAchievements("12").subscribe((data:any) => {
        expect(data).toEqual(AchievDumy);
        expect(data.length).toBe(2);
      });

      const req = backend.expectOne(`${urlApi}User/UserAchievements/12`);
      expect(req.request.method).toBe("GET");
      req.flush(AchievDumy);
    })
  });
});
