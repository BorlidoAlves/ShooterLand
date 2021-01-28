using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Inputs;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ShooterLand.Source.Engine.Basic2DObjects

{
    public class Basic2D
    {
        protected float rotation;
        protected Vector2 position, dimensions;
        protected Texture2D model;                      //what is meant to be drawed. It can be a character,enemy,button,etc
        protected _MouseControl mouseControl;           //mouse controller is in Basic2D because both Character and Button2D need it
        public Basic2D(string _path, Vector2 _position,Vector2 _dimensions)
        {
            position = _position;
            dimensions = _dimensions;
            model = Globals.content.Load<Texture2D>(_path);
            mouseControl = new _MouseControl();
        }

        public Basic2D()
        {

        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetRotation()
        {
            return rotation;
        }

        public void SetPosition(Vector2 _position)
        {
            position = _position;
        }
        public void SetRotation(float _rotation)
        {
            rotation = _rotation;
        }

        public Texture2D GetModel()
        {
            return model;
        }
       

        public Vector2 GetDimentions()
        {
            return dimensions;
        }

        public void SetDimentions(Vector2 _dimensions)
        {
            this.dimensions = _dimensions;
        }

        //delete this
        /*public virtual bool Hover(Vector2 _offset)
        {
            return HoverImg(_offset);
        }*/

        public virtual bool HoverImg(Vector2 _offset)
        {
            Vector2 mousePosition = new Vector2(mouseControl.newMousePos.X, mouseControl.newMousePos.Y);
            if (mousePosition.X>=(position.X+_offset.X)-dimensions.X/2 
                && mousePosition.X <= (position.X + _offset.X) + dimensions.X / 2
                && mousePosition.Y>=(position.Y+_offset.Y)-dimensions.Y/2 
                && mousePosition.Y <= (position.Y + _offset.Y) + dimensions.Y / 2)
            {
                return true;
            }

            return false;
        }

        
        public float RotateTowards(Vector2 Pos, Vector2 focus)
        {

            float h, sineTheta, angle;
            if (Pos.Y - focus.Y != 0)
            {
                h = (float)Math.Sqrt(Math.Pow(Pos.X - focus.X, 2) + Math.Pow(Pos.Y - focus.Y, 2));
                sineTheta = (float)(Math.Abs(Pos.Y - focus.Y) / h); //* ((item.Pos.Y-focus.Y)/(Math.Abs(item.Pos.Y-focus.Y))));
            }
            else
            {
                h = Pos.X - focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            // Drawing diagonial lines here.
            //Quadrant 2
            if (Pos.X - focus.X > 0 && Pos.Y - focus.Y > 0)
            {
                angle = (float)(Math.PI * 3 / 2 + angle);
            }
            //Quadrant 3
            else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y < 0)
            {
                angle = (float)(Math.PI * 3 / 2 - angle);
            }
            //Quadrant 1
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y > 0)
            {
                angle = (float)(Math.PI / 2 - angle);
            }
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y < 0)
            {
                angle = (float)(Math.PI / 2 + angle);
            }
            else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y == 0)
            {
                angle = (float)Math.PI * 3 / 2;
            }
            else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y == 0)
            {
                angle = (float)Math.PI / 2;
            }
            else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y > 0)
            {
                angle = (float)0;
            }
            else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y < 0)
            {
                angle = (float)Math.PI;
            }

            return angle;
        }

        //will overload to
        public virtual void Draw(Vector2 _offset)
        {
            if (model != null)
            {
                Globals.spriteBatch.Draw(model, new Rectangle((int)(position.X+_offset.X), (int)(position.Y+_offset.Y), (int)dimensions.X, (int)dimensions.Y),null, Color.White ,rotation,new Vector2(model.Bounds.Width / 2, model.Bounds.Height / 2),new SpriteEffects(),0);
            }
        }
        public virtual void Draw(Vector2 _offset, Vector2 _origin ,Color _color)
        {
            if (model != null)
            {
                Globals.spriteBatch.Draw(model, new Rectangle((int)(position.X + _offset.X), (int)(position.Y + _offset.Y), (int)dimensions.X, (int)dimensions.Y), null, _color, rotation, new Vector2(_origin.X, _origin.Y), new SpriteEffects(), 0);
            }
        }
        public virtual void Draw(Vector2 _offset, Rectangle _position)// Draw para desenhar o Mapa
        {
            if(model != null)
            {
                Globals.spriteBatch.Draw(model, new Rectangle((int)(_position.X + _offset.X), (int)(_position.Y + _offset.Y), (int)dimensions.X, (int)dimensions.Y), null, Color.White, rotation, new Vector2(model.Bounds.Width / 2, model.Bounds.Height / 2), new SpriteEffects(), 0);
            }
        }
    }
}
