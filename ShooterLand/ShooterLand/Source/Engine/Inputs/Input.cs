using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Basic2DObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Engine.Inputs
{
    public class Input : Basic2D
    {
        private _Keyboard keyboard;
        private string inputText;
        private SpriteFont font;
        private bool hovered, pressed, active, confidential;

        public Input( Vector2 _position, Vector2 _dimensions,bool _confidential): base("2D\\UI\\inputBox", _position, _dimensions)
        {
            keyboard = new _Keyboard();
            inputText = "";
            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
            position = _position;
            hovered = false;
            pressed = false;
            active = false;
            confidential = _confidential;
        }

        public string GetInputText()
        {
            return inputText;
        }

        public  void Update(Vector2 _offset,Input _input)
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
                    active = true;
                    _input.active = false;

                }
            }
            else
            {
                hovered = false;
            }

            if (!mouseControl.LeftClick() && !mouseControl.LeftClickHold())
            {
                pressed = false;
            }

            mouseControl.UpdateOld();
            if (active)
            {
                keyboard.Update();
                List<_Key> pressedKeys = keyboard.ReturnKeysPressed();
                for (int i = 0; i < pressedKeys.Count; i++)
                {
                    if (keyboard.GetSinglePress(pressedKeys[i].key))
                    {
                        if (pressedKeys[i].print.Equals("Back"))
                        {
                            string tempString="";
                            for(int j = 0; j < inputText.Length - 1; j++)
                            {
                                tempString += inputText[j];
                            }

                            inputText = tempString;
                        }
                        else
                        {
                            inputText += pressedKeys[i].print;
                        }
                        
                    }
                }


                keyboard.UpdateOld();
            }
        }

      

        public override void Draw(Vector2 _offset)
        {
            string tempString;
            if (!confidential)
            {
                tempString = inputText;
            }
            else  
            {
                tempString = "";
                for (int i = 0; i < inputText.Length; i++)
                {
                    tempString += "*";
                }
            }
           
            Vector2 strDims = font.MeasureString(tempString);
            Globals.spriteBatch.DrawString(font, tempString, new Vector2(_offset.X - strDims.X / 2, _offset.Y-10), Color.Black);
            base.Draw(_offset);

        
        }
    }
}
