using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Basic2DObjects;

namespace ShooterLand.Source.Engine.Output
{
    public class HealthBar
    {
        public int boarder;
        public Basic2D bar, barBack;
        public Color color;

        public HealthBar(Vector2 _dims,  int _boarder, Color _color)
        {
            boarder = _boarder;
            color = _color;
            bar = new Basic2D("2D\\solid", new Vector2(0, 0), new Vector2(_dims.X - boarder * 2, _dims.Y - boarder * 2));
            barBack = new Basic2D("2D\\solid", new Vector2(0, 0), new Vector2(_dims.X, _dims.Y));
        }

        public virtual void Update(int _health, int _healthMax)
        {
            if (_health >0)
            {
                float remaingLife = (float)_health / _healthMax;
                bar.SetDimentions(new Vector2(remaingLife * (barBack.GetDimentions().X - boarder * 2), bar.GetDimentions().Y));
            }
            else
            {
                
                bar.SetDimentions(new Vector2(0, bar.GetDimentions().Y));
            }

        }

        public virtual void Draw(Vector2 _offset)
        {
            barBack.Draw(_offset, new Vector2(0,0), Color.Black);
            bar.Draw(_offset + new Vector2(boarder, boarder), new Vector2(0,0), color);
        }
    }
}
