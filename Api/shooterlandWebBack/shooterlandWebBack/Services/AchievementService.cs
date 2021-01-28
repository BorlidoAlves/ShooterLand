using shooterlandWebBack.Entity;
using shooterlandWebBack.Helpers;
using shooterlandWebBack.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Services
{
    /// <summary>
    /// Interface of the Service 
    /// </summary>
    public interface IAchievementService
    {
        Achievement Create(Achievement achievement);

        void Update(Achievement achievement, int idAchievement);

        void Delete(int idAchievement);

        Achievement GetById(int idAchievement);

        List<Achievement> GetAll();
    }
    public class AchievementService : IAchievementService
    {
        private DataContext _context;

        public AchievementService(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// This method create a new Achievement
        /// </summary>
        /// <param name="achievement"></param>
        /// <returns></returns>
        public Achievement Create(Achievement achievement)
        {
            if (_context.Achievement.Any(x => x.Description == achievement.Description))
                throw new AppException("Description already used in another Achievement!!");
            _context.Achievement.Add(achievement);
            _context.SaveChanges();

            return achievement;
        }
        /// <summary>
        /// This method delete the Achievement with the given Id
        /// </summary>
        /// <param name="idAchievement"></param>
        public void Delete(int idAchievement)
        {
            var achievement = _context.Achievement.Find(idAchievement);
            
            if(achievement != null)
            {
                _context.Achievement.Remove(achievement);
                _context.SaveChanges();
            }
            else
            {
                throw new AppException("Id does not exist!!");
            }
        }

        /// <summary>
        /// This method gets all Achievements
        /// </summary>
        /// <returns>Returns all Achievements</returns>
        public List<Achievement> GetAll()
        {
            return _context.Achievement.ToList();
        }

        /// <summary>
        /// Thie method get an Achievement by Id 
        /// </summary>
        /// <param name="idAchievement"></param>
        /// <returns>Returns the Achievement with the given Id</returns>
        public Achievement GetById(int idAchievement)
        {
            return _context.Achievement.Find(idAchievement);
        }
        
        /// <summary>
        /// This method does the Update Achievement
        /// </summary>
        /// <param name="achievement"></param>
        /// <param name="idAchievement"></param>
        public void Update(Achievement achievement, int idAchievement)
        {
            var newAchievement = _context.Achievement.Find(idAchievement);

            if (newAchievement == null)
                throw new AppException("Achievement not found!!");

            if (!string.IsNullOrEmpty(achievement.Description))
                newAchievement.Description = achievement.Description;

            _context.Achievement.Update(newAchievement);
            _context.SaveChanges();
        }
    }
}
