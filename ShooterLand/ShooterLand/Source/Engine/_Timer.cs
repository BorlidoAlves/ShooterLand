using System;

namespace ShooterLand.Source.Engine
{
    public class _Timer
    {
        private bool goodToGo;
        private int mSec;
        private TimeSpan timer;
        
        public _Timer(int m)
        {
            goodToGo = false;
            mSec = m;
            timer = new TimeSpan();

        }


        public int GetTimer()
        {
            return (int)timer.TotalMilliseconds;

        }

        public int GetMSec()
        {
            return mSec;
        }



        public void UpdateTimer()
        {
            timer += Globals.gameTime.ElapsedGameTime;
        }

        public bool Test()
        {
            if(timer.TotalMilliseconds >= mSec || goodToGo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            goodToGo = false;
        }

        public virtual void AddToTimer(int MSEC)
        {
            timer += TimeSpan.FromMilliseconds((long)(MSEC));
        }


    }
}
