using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ShooterLand.Source.Engine.Basic2DObjects.Buttons
{
    public class MethodButton : Button2D
    {
        
        private PassObject buttonClicked;

        public MethodButton(string _path,Vector2 _position, Vector2 _dimensions,string _font,string _text,object _info,PassObject _buttonClicked) 
            : base(_path,_position,_dimensions,_font,_text)
        {  
            buttonClicked = _buttonClicked;
            info = _info;
        }

        public override void Update(Vector2 _offset)
        {
            base.Update(_offset);
            
        }


        public override  void RunButtonClicked()
        {
            buttonClicked(info);
            base.RunButtonClicked();
        }

        public override void Draw(Vector2 _offset)
        {
            base.Draw(_offset);
            
        }
    }
}
