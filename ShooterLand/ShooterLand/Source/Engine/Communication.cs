using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShooterLand.Source.Engine
{
    public static class Communication
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<User> Login(string username, string password)
        {
            User user = null;
           
            try 
            { 
                string responseBody;
                UserLogin userLogin = new UserLogin
                {
                    Username = username,
                    Password = password

                };

                HttpResponseMessage response = await client.PostAsJsonAsync("User/Login", userLogin);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(responseBody);
            }

            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return user;
        }

        public static async Task<HttpResponseMessage> SaveStats(int _monstersKilled, int _score, int _round, string _gameMode, int _userId)
        {
            HttpResponseMessage response = null;
            try
            {
                Stats stats = new Stats
                {
                    MonstersKilled = _monstersKilled,
                    Score = _score,
                    Round = _round,
                    GameMode = _gameMode

                };
                string endpoint = "GameStats/Update/" + _userId;
                response = await client.PutAsJsonAsync(endpoint, stats);
                response.EnsureSuccessStatusCode();

            }

            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return response;
        }

        public static async Task<HttpResponseMessage> SaveStatsMultiplayer(string _result,string _gameMode,int _userId)
        {
            
            HttpResponseMessage response = null;
            Debug.WriteLine("RESULTADO DO JOGO:"+_result);
            try
            {
                StatsMultiplayer stats = new StatsMultiplayer
                {
                    Result = _result,
                    GameMode = _gameMode

                };
                string endpoint = "GameStats/UpdateMultiplayer/" + _userId;
                response = await client.PutAsJsonAsync(endpoint, stats);
                response.EnsureSuccessStatusCode();

            }

            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return response;

        }

        public static void Initialize()
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
       


    }

    public class UserLogin{
        public string Username { get; set; }
        public string Password { get; set; }
        }


    public class Stats
    {
        public int MonstersKilled { get; set; }
        public int Score { get; set; }
        public int Round { get; set; }
        public string GameMode { get; set; }

    }

    public class StatsMultiplayer
    {
        public string Result;
        public string GameMode;
    }

}

