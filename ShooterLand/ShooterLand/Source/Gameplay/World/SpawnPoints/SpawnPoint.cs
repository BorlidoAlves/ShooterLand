using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShooterLand.Source.Gameplay.World.SpawnPoints
{
    //Since the character will be something that will appear on the screen it must inherit Basic2D Class
    public class SpawnPoint 
    {
        
        protected _Timer spawnTimer;
        protected int ownerId;
        protected Random random;
        protected static int roundSpawns;
       
        public SpawnPoint(int _ownerId)
        {
            spawnTimer = new _Timer(3500);
            ownerId = _ownerId;
            random = new Random();
        }

        public static void SetRoundSpawns(int _value)
        {
            roundSpawns = _value;
        }
        public _Timer GetSpawnTimer()
        {
            return spawnTimer;
        }

        public int GetOwnerId()
        {
            return ownerId;
        }

   

      

   
    }
}
