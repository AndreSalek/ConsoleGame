using System;
using Xunit;
using GameLibrary.Player;
using GameLibrary.Interactions;
using ConsoleUI;

namespace GameTests
{

    public class PlayerTests
    {
        Warrior warrior;
        Scout scout;
        Mage mage;
        Interaction interaction;
        Factory factory;
        public PlayerTests()
        {
            //Arrange
            interaction = new Interaction();
            factory = new Factory();
            warrior = factory.CreateWarrior("Warrior");
            scout = factory.CreateScout("Warrior");
            mage = factory.CreateMage("Warrior");
        }

        [Theory]
        [InlineData(0,100)]
        [InlineData(200, 500)]
        public void GenerateNumberInRange_ShouldReturnIntValueInRange(int min, int max)
        {
            //Act
            var number = new Random().Next(min, max);
            //Assert
            Assert.InRange(number, min, max);
        }
        [Fact]
        public void GetMainAttributeValue_ShouldReturnWarriorMainAttributeValue()
        {
            var atribute = warrior.GetMainAttributeValue();

            Assert.Equal(warrior.Strength, atribute);
        }
        [Fact]
        public void GetMainAttributeValue_ShouldReturnScoutMainAttributeValue()
        {
            var atribute = scout.GetMainAttributeValue();

            Assert.Equal(scout.Dexterity, atribute);
        }
        [Fact]
        public void GetMainAttributeValue_ShouldReturnMageMainAttributeValue()
        {
            var atribute = mage.GetMainAttributeValue();

            Assert.Equal(mage.Intelligence, atribute);
        }
        [Fact]
        public void RestoreHealth_ShouldSetWarriorHealthToMax()
        {
            warrior.Health -= 10;
            warrior.RestoreHealth();

            Assert.Equal(warrior.Vitality * 10 * 2, warrior.Health);
        }
        [Fact]
        public void RestoreHealth_ShouldSetScoutHealthToMax()
        {
            scout.Health -= 10;
            scout.RestoreHealth();

            Assert.Equal(scout.Vitality * 10 * 1.5, scout.Health);
        }
        [Fact]
        public void RestoreHealth_ShouldSetMageHealthToMax()
        {
            mage.Health -= 10;
            mage.RestoreHealth();

            Assert.Equal(mage.Vitality * 10, mage.Health);
        }
        
        
        [Fact]
        public void UpdateMaxHealth_ShouldSetWarriorNewMaxHPValue()
        {
            warrior.Vitality += 10;
            warrior.UpdateMaxHealth();

            //should not equal because Health should be set to old value unlike max health
            Assert.NotEqual(warrior.Vitality * 10 * 2, warrior.Health);
        }
        [Fact]
        public void UpdateMaxHealth_ShouldSetScoutNewMaxHPValue()
        {
            scout.Vitality += 10;
            scout.UpdateMaxHealth();

            Assert.NotEqual(scout.Vitality * 10 * 1.5, scout.Health);
        }
        [Fact]
        public void UpdateMaxHealth_ShouldSetMageNewMaxHPValue()
        {
            mage.Vitality += 10;
            mage.UpdateMaxHealth();

            Assert.NotEqual(mage.Vitality * 10, mage.Health);
        }

        //minimum damage player can deal
        [Fact]
        public void UpdateDamage_ShouldSetNewMinWarriorDamage()
        {
            warrior.Strength += 10;
            warrior.UpdateDamage();
            //asserting entire damage formula against calculated damage by the method
            Assert.Equal(warrior.EquippedWeapon.MinDamage * (1 + warrior.GetMainAttributeValue() / 10), warrior.MinDamage);
        }
        //maximum damage player can deal
        [Fact]
        public void UpdateDamage_ShouldSetNewMaxWarriorDamage()
        {
            warrior.Strength += 10;
            warrior.UpdateDamage();

            Assert.Equal(warrior.EquippedWeapon.MaxDamage * (1 + warrior.GetMainAttributeValue() / 10), warrior.MaxDamage);
        }
        [Fact]
        public void UpdateDamage_ShouldSetNewMinScoutDamage()
        {
            scout.Dexterity += 10;
            scout.UpdateDamage();

            Assert.Equal(Math.Floor(scout.EquippedWeapon.MinDamage * (1 + scout.GetMainAttributeValue() / 10) * 1.5), scout.MinDamage);
        }
        [Fact]
        public void UpdateDamage_ShouldSetNewMaxScoutDamage()
        {
            scout.Dexterity += 10;
            scout.UpdateDamage();

            Assert.Equal(Math.Floor(scout.EquippedWeapon.MaxDamage * (1 + scout.GetMainAttributeValue() / 10) * 1.5), scout.MaxDamage);
        }
        [Fact]
        public void UpdateDamage_ShouldSetNewMinMageDamage()
        {
            mage.Intelligence += 10;
            mage.UpdateDamage();

            Assert.Equal(mage.EquippedWeapon.MinDamage * (1 + mage.GetMainAttributeValue() / 10) * 2, mage.MinDamage);
        }
        [Fact]
        public void UpdateDamage_ShouldSetNewMaxMageDamage()
        {
            mage.Intelligence += 10;
            mage.UpdateDamage();

            Assert.Equal(mage.EquippedWeapon.MaxDamage * (1 + mage.GetMainAttributeValue() / 10) * 2, mage.MaxDamage);
        }
        [Fact]
        public void Duel_ShouldWinPlayerWithMoreThanZeroHealth()
        {
            PlayerModel winner = interaction.Duel(warrior, mage);

            Assert.True(winner.Health > 0);
        }
        [Fact]
        public void SimulateTurn_BothPlayerShouldReceiveDamage()
        {
            interaction.SimulateTurn(warrior, mage);
            /* two asserts because I need to check if both players received damage, which should happen 100
            percent of the time in this case */
            Assert.NotEqual(warrior.Health, warrior.Vitality * 10 * 2);
            Assert.NotEqual(mage.Health, mage.Vitality * 10 * 2);
        }

    }
}
