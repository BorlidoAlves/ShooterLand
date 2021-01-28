using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ShooterLand
{
    public class _Keyboard
    {

        public KeyboardState newKeyboard, oldKeyboard;       //newKeyboard tracks the actual frame while oldKeyboard tracks the previous frame
        public List<_Key> pressedKeys = new List<_Key>(), previousPressedKeys = new List<_Key>();

        public _Keyboard()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();
            GetPressedKeys();

        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<_Key>();
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }


        public bool GetPress(string KEY)
        {

            for (int i = 0; i < pressedKeys.Count; i++)
            {

                if (pressedKeys[i].key == KEY)
                {
                    return true;
                }

            }


            return false;
        }

        public virtual void GetPressedKeys()
        {
            pressedKeys.Clear();
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
            {

                pressedKeys.Add(new _Key(newKeyboard.GetPressedKeys()[i].ToString()));

            }
        }

        public List<_Key> ReturnKeysPressed()
        {
            return pressedKeys;
        }

        public virtual bool GetSinglePress(string KEY)
        {
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                bool isIn = false;

                for(int j = 0; j < previousPressedKeys.Count; j++)
                {
                    if(pressedKeys[i].key == previousPressedKeys[j].key)
                    {
                        isIn = true;
                        break;
                    }
                }

                if(!isIn && (pressedKeys[i].key == KEY || pressedKeys[i].print == KEY))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
