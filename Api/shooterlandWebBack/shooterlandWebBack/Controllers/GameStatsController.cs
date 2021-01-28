using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Helpers;
using shooterlandWebBack.Models;
using shooterlandWebBack.Models.User;
using shooterlandWebBack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class GameStatsController: ControllerBase
    {
        private IGameStatsService _gameStatsService;
        private IUsersAchievService _usersAchievService;
        private IMapper _mapper;

        public GameStatsController(
            IGameStatsService gameStatsService,
            IUsersAchievService usersAchievService,
            IMapper mapper)
        {
            _gameStatsService = gameStatsService;
            _usersAchievService = usersAchievService;    
            _mapper = mapper;
        }

        /// <summary>
        /// This endpoint is responsible for returning data to do the leaderboard of Kills in Singleplayer
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("LeaderboardKillsSingleplayer")]
        public IActionResult LeaderboardKillsSinglePlayer()
        {
            var leaderboard = _gameStatsService.LeaderboardKillsSinglePlayer();
            return Ok(leaderboard);
        }

        /// <summary>
        /// This endpoint is responsible for returning data to do the leaderboard of Rounds in Singleplayer
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("LeaderboardRoundsSingleplayer")]
        public IActionResult LeaderboardRoundsSinglePlayer()
        {
            var leaderboard = _gameStatsService.LeaderboardRoundsSinglePlayer();
            return Ok(leaderboard);
        }
        
        /// <summary>
        /// This endpoint is responsible for returning data to do the leaderboard of Scores in Singleplayer
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("LeaderboardScoresSingleplayer")]
        public IActionResult LeaderboardScoresSinglePlayer()
        {
            var leaderboard = _gameStatsService.LeaderboardScoreSinglePlayer();
            return Ok(leaderboard);
        }

        /// <summary>
        /// This endpoint is responsible for returning data to do the leaderboard in Multiplayer
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("LeaderboardMultiplayer")]
        public IActionResult LeaderboardMultiPlayer()
        {
            var leaderboard = _gameStatsService.LeaderboardMultiplayer();
            return Ok(leaderboard);
        }
        
        /// <summary>
        /// This endpoint is responsible for updating the user stats in Singleplayer in the Database and refresh it's achievements
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpPut("Update/{id}")]
        public IActionResult UpdateStats([FromBody]StatUpdateModel model, int id)
        {

            try
            {
                _gameStatsService.UpdateStats(model, id);
                _usersAchievService.RefreshUserAchiev(id, model.GameMode);
                return Ok(new { Message = "Stats Updated!!"});
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        /// <summary>
        /// This endpoint is responsible for updating the user stats in Multiplayer gamemode in the Database and refresh it's achievements
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut("UpdateMultiplayer/{id}")]
        public IActionResult UpdateStatsMultiplayer([FromBody] StatUpdateMultiplayerModel model, int id)
        {

            try
            {
                _gameStatsService.UpdateStatsMultiplayer(model, id);
               // _usersAchievService.RefreshUserAchiev(id, model.GameMode);
                return Ok(new { Message = "Stats Updated!!" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
