using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Design;

namespace ShooterLand.Source.Engine.Basic2DObjects
{
    public class Tile :Basic2D 
    {
        private bool crossableUnit;
        private bool crossableProjectile;
        

        public Tile(string _path, bool _crossableUnit, bool _crossableProjectile, int _width,int _height) :base (_path, Vector2.Zero, new Vector2(_width,_height))
        {
            this.crossableUnit = _crossableUnit;
            crossableProjectile = _crossableProjectile;

        }

        public bool UnitCross()
        {
            return this.crossableUnit;
        } 

        public bool ProjectileCross()
        {
            return crossableProjectile;
        }

    }
}
