using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ShooterLand.Source.Engine.Basic2DObjects.Buttons
{
    public class Button2D : Basic2D
    {
        protected bool pressed, hovered;
        protected string text;
       // protected Color hoverColor;
        protected SpriteFont font;
        protected object info;
        

        public Button2D(string _path,Vector2 _position, Vector2 _dimensions,string _font,string _text) 
            : base(_path,_position,_dimensions)
        {
            text = _text;
           
            

            if (_font != null)
            {
                font = Globals.content.Load<SpriteFont>(_font);
            }

            pressed = false;
            //hoverColor = new Color(200, 230, 255);

           // info = _info;
        }

        public virtual void Update(Vector2 _offset)
        {
            mouseControl.Update();
            if (HoverImg(_offset))
            {
                hovered = true;

                if (mouseControl.LeftClick())
                {
                   
                    hovered = false;
                    pressed = true;
                }

                else if (mouseControl.LeftClickRelease())
                {
                    RunButtonClicked();
                   
                }
            }
            else
            {
                hovered = false;
            }

            if(!mouseControl.LeftClick() && !mouseControl.LeftClickHold())
            {
                pressed = false;
            }

            mouseControl.UpdateOld();

            
        }

        public void Reset()
        {
            pressed = false;
            hovered = false;
        }

        public virtual void RunButtonClicked()
        {
            
            Reset();
        }

        public override void Draw(Vector2 _offset)
        {
            /*Color tempColor = Color.White;

            if (pressed)
            {
                tempColor = Color.Gray;
            }*/
           /* else if (hovered)
            {
                tempColor = hoverColor;
            }*/
           /* Globals.normalEffect.Parameters["xSize"].SetValue((float)GetModel().Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)GetModel().Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dimensions.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dimensions.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();*/

            base.Draw(_offset);

            Vector2 strDims = font.MeasureString(text);

            Globals.spriteBatch.End();
            //Debug.WriteLine
            Globals.spriteBatch.Begin();
            Globals.spriteBatch.DrawString(font, text, position + _offset + new Vector2(-strDims.X / 2, -strDims.Y / 2), Color.Black);
            
        }
    }
}
