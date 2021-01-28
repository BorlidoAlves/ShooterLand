using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Engine
{
   public class PathUnit
    {
        private int tileX;
        private int tileY;
        private int cost;
        private PathUnit parent;
        private int distance;

        public PathUnit(int _tileX,int _tileY,PathUnit _parent,int _cost)
        {
            tileX = _tileX;
            tileY = _tileY;
            cost = _cost;
            parent = _parent;

        }
        public int GetTileX()
        {
            return tileX;
        }

        public int GetTileY()
        {
            return tileY;
        }

        public PathUnit GetParent()
        {
            return parent;
        }

        public void SetParent(PathUnit _parent)
        {
            parent = _parent;
        }

        public int GetCost()
        {
            return cost;
        }

        public void SetCost(int _cost)
        {
            cost = _cost;
        }

        public void SetDistance(int targetX, int targetY)
        {
           distance = Math.Abs(targetX - tileX) + Math.Abs(targetY - tileY);
        }

        public int GetCostDistance()
        {
            return cost + distance;
        }
    }
}
