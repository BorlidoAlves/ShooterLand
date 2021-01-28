using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Engine.Basic2DObjects.Buttons;
using ShooterLand.Source.Engine.Output;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Menus
{
    public class EndRoundMenu
    {
        protected Basic2D background;
        protected PassObject nextRoundClick;
        protected List<Button2D> buttons;
        private EndRoundManager endRoundManager;
        private IconManager iconManager; 
        
        public EndRoundMenu(PassObject _nextRoundClick, 
                            PassObject _upgradeHealth, 
                            PassObject _upgradeDamage, 
                            PassObject _upgradeSpeed, 
                            PassObject _takeUpgradeHealth, 
                            PassObject _takeUpgradeDamage,
                            PassObject _takeUpgradeSpeed,
                            EndRoundManager _endRoundManager)
        {
            iconManager = new IconManager();
            this.endRoundManager = _endRoundManager;
            nextRoundClick = _nextRoundClick;
            buttons = new List<Button2D>();
            background = new Basic2D("2D\\UI\\Backgrounds\\mainMenu", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(25, 25), "Fonts\\Arial16", "+", null, _upgradeHealth));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(25, 25), "Fonts\\Arial16", "-", null, _takeUpgradeHealth));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(25, 25), "Fonts\\Arial16", "+", null, _upgradeDamage));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(25, 25), "Fonts\\Arial16", "-", null, _takeUpgradeDamage));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(25, 25), "Fonts\\Arial16", "+", null, _upgradeSpeed));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(25, 25), "Fonts\\Arial16", "-", null, _takeUpgradeSpeed));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(150, 50), "Fonts\\Arial16", "Next Round", 1, _nextRoundClick));

        }

        public virtual void Update()
        {
            buttons[0].Update(new Vector2(Globals.screenWidth / 2 + 80, 310));
            buttons[1].Update(new Vector2(Globals.screenWidth / 2 + 120, 310));
            buttons[2].Update(new Vector2(Globals.screenWidth / 2 + 80, 360));
            buttons[3].Update(new Vector2(Globals.screenWidth / 2 + 120, 360));
            buttons[4].Update(new Vector2(Globals.screenWidth / 2 + 80, 410));
            buttons[5].Update(new Vector2(Globals.screenWidth / 2 + 120, 410));
            buttons[6].Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
        }

        public virtual void Draw(Character _character)
        {
            SpriteFont font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
            SpriteFont fontUpgrade = Globals.content.Load<SpriteFont>("Fonts\\Arial24");

            string heathStr, damageStr, speedStr, upgradeStr;

            background.Draw(Vector2.Zero);

            upgradeStr = "Upgrades Avalilable: " + this.endRoundManager.GetUpgradesAvailable();

            heathStr = "Max health: " + _character.GetHealthMax() + "  (+" + this.endRoundManager.GetUpgradeHealth() + ")";
            damageStr = "Damage: " + _character.GetDamage() + "  (+" + this.endRoundManager.GetUpgradeDamage() + ")";
            speedStr = "Speed: " + _character.GetSpeed() + "  (+" + this.endRoundManager.GetUpgradeSpeed() + ")";

            Vector2 dimStr = fontUpgrade.MeasureString(upgradeStr);
            Globals.spriteBatch.DrawString(fontUpgrade, upgradeStr, new Vector2(Globals.screenWidth / 2 - dimStr.X / 2, 100), Color.Black);

            Globals.spriteBatch.DrawString(font, heathStr, new Vector2(Globals.screenWidth / 2 - 150, 300), Color.Black);
            
            buttons[0].Draw(new Vector2(Globals.screenWidth / 2 + 80, 310));
            buttons[1].Draw(new Vector2(Globals.screenWidth / 2 + 120, 310));
            iconManager.GetListIcon()[1].Draw(new Vector2(Globals.screenWidth / 2 - 200, 310));

            Globals.spriteBatch.DrawString(font, damageStr, new Vector2(Globals.screenWidth / 2 - 150, 350), Color.Black);
            buttons[2].Draw(new Vector2(Globals.screenWidth / 2 + 80, 360));
            buttons[3].Draw(new Vector2(Globals.screenWidth / 2 + 120, 360));
            iconManager.GetListIcon()[0].Draw(new Vector2(Globals.screenWidth / 2 - 200, 360));

            Globals.spriteBatch.DrawString(font, speedStr, new Vector2(Globals.screenWidth / 2 - 150, 400), Color.Black);
            buttons[4].Draw(new Vector2(Globals.screenWidth / 2 + 80, 410));
            buttons[5].Draw(new Vector2(Globals.screenWidth / 2 + 120, 410));
            iconManager.GetListIcon()[2].Draw(new Vector2(Globals.screenWidth / 2 - 200, 410));

            buttons[6].Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));

        }
    }
}
