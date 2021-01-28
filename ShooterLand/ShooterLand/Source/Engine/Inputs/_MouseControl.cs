using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShooterLand.Source.Engine.Inputs
{
    public class _MouseControl
    {
        public bool dragging;
        public Vector2 newMousePos, oldMousePos, systemCursorPos, screenLoc,firstMousePos ;
        public MouseState newMouse, oldMouse,firstMouse;

        public _MouseControl()
        {
            dragging = false;
            newMouse = Mouse.GetState();
            oldMouse = newMouse;
            firstMouse = newMouse;
            newMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            oldMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            firstMousePos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
            GetMouseAndAdjust();
        }

        public void Update()
        {
            GetMouseAndAdjust();

            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton ==ButtonState.Released)
            {
                firstMouse = newMouse;
                firstMousePos = newMousePos = GetScreenPos(firstMouse);
            }

        }

        public void UpdateOld()
        {
            oldMouse = newMouse;
            oldMousePos = GetScreenPos(oldMouse);
        }

      
        public virtual void GetMouseAndAdjust()
        {
            newMouse = Mouse.GetState();
            newMousePos = GetScreenPos(newMouse);

        }


        public Vector2 GetScreenPos(MouseState MOUSE)
        {
            Vector2 tempVec = new Vector2(MOUSE.Position.X, MOUSE.Position.Y);

            return tempVec;
        }

        public virtual bool LeftClick()
        {
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool LeftClickRelease()
        {
            if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
            {
                dragging = false;
                return true;
            }

            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;

            if (newMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && oldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
            {
                holding = true;

                if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
                {
                    dragging = true;
                }
            }



            return holding;
        }



    }
}
