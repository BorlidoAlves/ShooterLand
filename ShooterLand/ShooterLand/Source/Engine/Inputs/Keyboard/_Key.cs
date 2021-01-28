using Microsoft.Xna.Framework.Input;

namespace ShooterLand
{
    public class _Key
    {
        
        public string key, print;

        public _Key(string _key)
        {
            key = _key;
            MakePrint(key);
        }

        public void MakePrint(string KEY)
        {
            

            string tempStr = "";

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
            {
                if (KEY == "A" || KEY == "B" || KEY == "C" || KEY == "D" || KEY == "E" || KEY == "F" || KEY == "G" || KEY == "H" || KEY == "I" || KEY == "J" || KEY == "K" || KEY == "L" || KEY == "M" || KEY == "N" || KEY == "O" || KEY == "P" || KEY == "Q" || KEY == "R" || KEY == "S" || KEY == "T" || KEY == "U" || KEY == "V" || KEY == "W" || KEY == "X" || KEY == "Y" || KEY == "Z")
                {
                    tempStr = KEY;
                }

                if (KEY == "D1")
                {
                    tempStr = "!";
                }

                if (KEY == "OemMinus")
                {
                    tempStr = "_";
                    
                }
            }
            else
            {
                if (KEY == "A" || KEY == "B" || KEY == "C" || KEY == "D" || KEY == "E" || KEY == "F" || KEY == "G" || KEY == "H" || KEY == "I" || KEY == "J" || KEY == "K" || KEY == "L" || KEY == "M" || KEY == "N" || KEY == "O" || KEY == "P" || KEY == "Q" || KEY == "R" || KEY == "S" || KEY == "T" || KEY == "U" || KEY == "V" || KEY == "W" || KEY == "X" || KEY == "Y" || KEY == "Z")
                {
                    tempStr = KEY.ToLower();
                }

                if(KEY == "Space")
                {   
                    tempStr = " ";
                }
                if (KEY == "OemCloseBrackets")
                {
                    tempStr = "]";
                   
                }
                if (KEY == "OemOpenBrackets")
                {
                    tempStr = "[";
                    
                }
                if (KEY == "OemMinus")
                {
                    tempStr = "-";
                    
                }
                if (KEY == "OemPeriod" || KEY == "Decimal")
                {
                    tempStr = ".";
                }
                if (KEY == "D1" || KEY == "D2" || KEY == "D3" || KEY == "D4" || KEY == "D5" || KEY == "D6" || KEY == "D7" || KEY == "D8" || KEY == "D9" || KEY == "D0")
                {
                    tempStr = KEY.Substring(1);
                }
                if (KEY == "Back")
                {
                    tempStr = "Back";
                }

                else if (KEY == "NumPad1" || KEY == "NumPad2" || KEY == "NumPad3" || KEY == "NumPad4" || KEY == "NumPad5" || KEY == "NumPad6" || KEY == "NumPad7" || KEY == "NumPad8" || KEY == "NumPad9" || KEY == "NumPad0")
                {
                    tempStr = KEY.Substring(6);
                }
            }

            


            print = tempStr;
        }
    }
}

