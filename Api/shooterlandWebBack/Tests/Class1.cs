using Newtonsoft.Json;
using shooterlandWebBack.Models;
using shooterlandWebBack.Models.User;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Tests
{
    public class Class1
    {
        private static readonly HttpClient client = new HttpClient();
        private string AdminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIwMTIiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2MTAyODA3MjksImV4cCI6MTYxMDM2NzEyOSwiaWF0IjoxNjEwMjgwNzI5fQ.QMDZDQZLiwCopexUNXFMXYkAyQH9WVKdL5kBnRuxm1g";
        private string UserToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjMwMjAiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTYxMDI4MDIzNiwiZXhwIjoxNjEwMzY2NjM2LCJpYXQiOjE2MTAyODAyMzZ9.dNIRvK4_MlQYr9frWnomlnP4IP7ARg8Vpjg2mtAMtkY";


        //This test checks if we can acess the endpoint of all users.
        //It is supposed to pass since we are using a administrator token
        [Fact]
        public void TestRouteAllUsers()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest
                                           .Create("http://localhost:5000/User/AllUsers");
            request.Headers.Add("Authorization", "Bearer " + AdminToken);


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        //This test checks if we can acess the endpoint of all achievements
        [Fact]
        public void TestRouteAllAchievements()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest
                                           .Create("http://localhost:5000/Achievement/GetAll");
            request.Headers.Add("Authorization", "Bearer " + UserToken);


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //This test checks if we can acess the endpoint of the leaderboard of scores

        [Fact]
        public void TestRouteLeaderboardScore()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest
                                           .Create("http://localhost:5000/GameStats/LeaderboardScoresSingleplayer");
            request.Headers.Add("Authorization", "Bearer " + UserToken);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //This test checks if we can acess the endpoint of the leaderboard of monsters killed
        [Fact]
        public void TestRouteLeaderboardKills()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest
                                           .Create("http://localhost:5000/GameStats/LeaderboardKillsSingleplayer");
            request.Headers.Add("Authorization", "Bearer " + UserToken);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //This test checks if we can acess the endpoint of the leaderboard of highest round
        [Fact]
        public void TestRouteLeaderboardRound()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest
                                           .Create("http://localhost:5000/GameStats/LeaderboardRoundsSingleplayer");
            request.Headers.Add("Authorization", "Bearer " + UserToken);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //This test checks if we can acess the endpoint of the multiplayer leaderboard
        [Fact]
        public void TestRouteLeaderboardMultiplayer()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest
                                           .Create("http://localhost:5000/GameStats/LeaderboardMultiplayer");
            request.Headers.Add("Authorization", "Bearer " + UserToken);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //This test will check if we can get a status 200 with an existing ID;
        [Theory]
        [InlineData("1034")]
        [InlineData("1035")]
        [InlineData("1037")]
        [InlineData("1038")]
        [InlineData("1039")]
        public void TestGetAchievement(string id)
         {

             HttpWebRequest request = (HttpWebRequest)WebRequest
                                         .Create("http://localhost:5000/Achievement/GetById/"+id);
             request.Headers.Add("Authorization", "Bearer " + UserToken);


             HttpWebResponse response = (HttpWebResponse)request.GetResponse();

             Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         }


        //This test checks if it is possible to update the singleplayer stats
        [Fact]
        public async System.Threading.Tasks.Task TestUpdateSinglePlayerStats()
        {
            string id = "2012";
            StatUpdateModel info = new StatUpdateModel();
            info.Score = 4000;
            info.Round = 4;
            info.MonstersKilled = 72;
            info.GameMode = "SinglePlayer";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("http://localhost:5000/GameStats/Update/" + id, content);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }

        //This test checks if is possible to update the multiplayer stats
        [Fact]
        public async System.Threading.Tasks.Task TestUpdateMultiPlayerStats()
        {
            string id = "2012";
            StatUpdateMultiplayerModel info = new StatUpdateMultiplayerModel();
            info.Result = "Win";
            info.GameMode = "MultiPlayer";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("http://localhost:5000/GameStats/UpdateMultiplayer/" + id, content);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }

        //This test checks if we can send a non valid gameMode
        [Fact]
        public async System.Threading.Tasks.Task FailTestUpdateSinglePlayerStats()
        {
            string id = "2012";
            StatUpdateModel info = new StatUpdateModel();
            info.Score = 0;
            info.Round = 0;
            info.MonstersKilled = 0;
            info.GameMode = "something";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("http://localhost:5000/GameStats/Update/" + id, content);

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        
        //This test checks if it is possible to update the info with wrong information
        [Fact]
        public async System.Threading.Tasks.Task FailTestUpdateMultiPlayerStats()
        {
            string id = "2012";
            StatUpdateMultiplayerModel info = new StatUpdateMultiplayerModel();
            info.Result = "Invalid Result";
            info.GameMode = "Multiplayer";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("http://localhost:5000/GameStats/UpdateMultiplayer/" + id, content);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

        }

        //This test checks if it is possible to reset the password given a invalid email
        [Fact]
        public async System.Threading.Tasks.Task FailTestForgetPassword()
        {
            string email = "nonexistant@nonexistant.com";

            var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("http://localhost:5000/User/ForgetPassword", content);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

        }


        //This test checks if it is possible to create an user with an already used Username
        [Fact]
        public async System.Threading.Tasks.Task TestRepeateUsername()
        {
            RegisterModel info = new RegisterModel();
            info.Email = "testemail@gmail.com";
            info.Username = "Admin";
            info.FirstName = "Test";
            info.LastName = "Test";
            info.Password = "test12345";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("http://localhost:5000/User/Register", content);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

        }

        //This test checks if it is possible to create an user with an already used email
        [Fact]
        public async System.Threading.Tasks.Task TestRepeateEmail()
        {
            RegisterModel info = new RegisterModel();
            info.Email = "admin@shooterland.com";
            info.Username = "12dsaddsadasdsadasdasdasdsadasd";
            info.FirstName = "Test";
            info.LastName = "Test";
            info.Password = "test12345";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("http://localhost:5000/User/Register", content);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

        }

        //This test checks if it is possible to create an user with a password shorter than 8 characters
        [Fact]
        public async System.Threading.Tasks.Task TestShortPassword()
        {
            RegisterModel info = new RegisterModel();
            info.Email = "testemail@gmail.com";
            info.Username = "121213223323";
            info.FirstName = "Test";
            info.LastName = "Test";
            info.Password = "123";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("http://localhost:5000/User/Register", content);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

        }

        //This test checks if we can send a negative number of monsters killed
        [Fact]
        public async System.Threading.Tasks.Task TestNegativeMonsters()
        {
            string id = "2012";
            StatUpdateModel info = new StatUpdateModel();
            info.Score = 12000;
            info.Round = 7;
            info.MonstersKilled = -30;
            info.GameMode = "SinglePlayer";

            var content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync("http://localhost:5000/GameStats/Update/" + id, content);

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }



    }
}
