using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Helpers;
using shooterlandWebBack.Models;
using shooterlandWebBack.Services;

namespace shooterlandWebBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AchievementController : ControllerBase
    {
        private IAchievementService _achievementService;
        private IWebHostEnvironment _webHostEnvironment;
        private IMapper _mapper ;

        public AchievementController(IAchievementService achievementService, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _achievementService = achievementService;
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Creates a new Achievement
        /// </summary>
        /// <param name="model">Model Create Achievement</param>
        [Authorize(Roles = Entity.Type.Admin)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm]AchievCreateModel model)
        {
            model.Medal = await SaveImage(model.MedalFile);
            var achievement = _mapper.Map<Achievement>(model);

            try
            {
                _achievementService.Create(achievement);
                return Ok(new {Message = "Achievement Created!!" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all Achievements
        /// </summary>
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var achievements = _achievementService.GetAll();
            return Ok(achievements);

        }
        /// <summary>
        /// Get a Achievement by id
        /// </summary>
        /// <param name="id"></param>
       
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {

            var achievement = _achievementService.GetById(id);
            return Ok(achievement);

        }

        /// <summary>
        /// Delete a Achievement
        /// </summary>
        /// <param name="id">Achievement Id</param>
        [Authorize(Roles = Entity.Type.Admin)]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _achievementService.Delete(id);
                return Ok(new { Message = "Achievement Deleted!!" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ','-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Image", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

    }
}
