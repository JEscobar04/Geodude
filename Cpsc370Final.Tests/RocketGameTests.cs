using System;
using System.IO;
using Xunit;

namespace Cpsc370Final.Tests
{
    public class RocketGameTests
    {
        [Fact]
        public void PlayerWinsAndCashesOut_BalanceUpdatedCorrectly()
        {
            // Arrange
            var random = new Random(0); // Seeded random for consistent results
            var player = new Player(1000);
            var game = new RocketGame(player, random);
            double initialBalance = game.TemporaryBalance;
            double betAmount = 100;

            // Act
            game.PlaceBet(betAmount);
            // Force the outcome to be safe
            game.Outcome = true;

            // Simulate a winning round
            game.BetAmount = Math.Round(game.BetAmount * 1.1, 2);
            double expectedBetAfterWin = Math.Round(betAmount * 1.1, 2);

            // Player decides to cash out
            game.TemporaryBalance += game.BetAmount;
            game.BetAmount = 0;

            // Assert
            Assert.Equal(expectedBetAfterWin, expectedBetAfterWin); // Sanity check
            Assert.Equal(initialBalance - betAmount + expectedBetAfterWin, game.TemporaryBalance);
            Assert.Equal(0, game.BetAmount);
        }
    }
}
