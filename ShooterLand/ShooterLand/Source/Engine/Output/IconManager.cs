using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Basic2DObjects;
using System;
using System.Collections.Generic;

using System.Text;

namespace ShooterLand.Source.Engine.Output
{
    public class IconManager
    {
        List<Icon> iconList;

        public IconManager()
        {
            this.iconList = new List<Icon>();

            iconList.Add(new Icon("2D\\Icons\\damageIcon",new Vector2(30,30)));
            iconList.Add(new Icon("2D\\Icons\\heartIcon", new Vector2(25, 25)));
            iconList.Add(new Icon("2D\\Icons\\speedIcon", new Vector2(25, 25)));
        }
        
        public List<Icon> GetListIcon()
        {
            return this.iconList;
        }

        public class Icon
        {
            Basic2D icon;
            
            public Icon(string _path, Vector2 _dimensions)
            {
                this.icon = new Basic2D(_path, Vector2.Zero,  _dimensions);
            } 

            public void Draw(Vector2 _position)
            {
                icon.Draw(_position);
            }
        }
    }
}
