using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShooterLand.Source.Gameplay.Managers
{
    public class EndRoundManager
    {
        private int upgrades;
        private Character character;
        private int upgradeDamage, upgradeHealth, modHealth, modDamage, modSpeed;
        private float upgradeSpeed;
        public EndRoundManager(Character _character)
        {
            this.upgrades = 0;
            this.modDamage = 0;
            this.modHealth = 0;
            this.modSpeed = 0;
            this.character = _character;
            this.upgradeHealth = 30; // Value of Health upgrade
            this.upgradeDamage = 10; // Value of Damage upgrade
            this.upgradeSpeed = 0.5f; // Value of Speed upgrade
        }

        public int GetUpgradeHealth()
        {
            return this.upgradeHealth;
        }
        public float GetUpgradeSpeed()
        {
            return this.upgradeSpeed;
        }
        public int GetUpgradeDamage()
        {
            return this.upgradeDamage;
        }

        public int GetUpgradesAvailable()
        {
            return this.upgrades;
        }
        public void GainPowerUp()// This method is called in the end of each round and increments 1 in the variable upgrades 
        {
            this.upgrades++;
        }

        public void ResetModCount()
        {
            this.modHealth = 0;
            this.modSpeed = 0;
            this.modDamage = 0;
        }
        public void TakeUpgradeHealth(object _character)
        {
            if (modHealth > 0)
            {
                character.SetMaxHealth(character.GetHealthMax() - this.upgradeHealth);
                character.SetHealth(character.GetHealthMax());
                this.modHealth--;
                this.upgrades++;
            }
        }
        public void SetPowerUpHealth(object _character)
        {
            if (upgrades > 0)
            {
                character.SetMaxHealth(character.GetHealthMax() + this.upgradeHealth);
                character.SetHealth(character.GetHealthMax());
                this.upgrades--;
                this.modHealth++;
            }
        }

        public void TakeUpgradeSpeed(object _character)
        {
            if (modSpeed > 0)
            {
                character.SetSpeed(character.GetSpeed() - this.upgradeSpeed);
                this.modSpeed--;
                this.upgrades++;
            }
        }

        public void SetPowerUpSpeed(object _character)
        {
            if (upgrades > 0)
            { 
                character.SetSpeed(character.GetSpeed() + this.upgradeSpeed);
                this.upgrades--;
                this.modSpeed++;
            }
        }

        public void TakeUpgradeDamage(object _character)
        {
            if (modDamage > 0)
            {
                character.SetDamage(character.GetDamage() - this.upgradeDamage);
                this.modDamage--;

                this.upgrades++;
            }
        }

        public void SetPowerUpDamage(object _character)
        {
            if (upgrades > 0)
            {
                character.SetDamage(character.GetDamage() + this.upgradeDamage);
                this.upgrades--;
                this.modDamage++;
            }
        }
    }
}
