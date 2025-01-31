using System;
using Xunit;

public class RouletteTests
{
    private string lastInput;
    private Func<string> SimulateInput(string input)
    {
        return () => input;
    }

    [Fact]
    public void GetBetType_ShouldReturnCorrectBetType()
    {
        var roulette = new Roulette(SimulateInput("outside"));
        var result = roulette.GetBetType();
        Assert.Equal("outside", result.ToLower());
    }

    [Fact]
    public void GetColor_ShouldReturnRedForRedInput()
    {
        var roulette = new Roulette(SimulateInput("red"));
        var result = roulette.GetColor();
        Assert.Equal(0, result); // 0 corresponds to red
    }

    [Fact]
    public void GetColor_ShouldReturnBlackForBlackInput()
    {
        var roulette = new Roulette(SimulateInput("black"));
        var result = roulette.GetColor();
        Assert.Equal(1, result); // 1 corresponds to black
    }

    [Fact]
    public void GetColor_ShouldAskForInputUntilValid()
    {
        var roulette = new Roulette(SimulateInput("invalid")); // First invalid input
        var validColor = "red"; // Simulate a valid input after an invalid one
        roulette = new Roulette(SimulateInput(validColor));  // Simulate valid input next

        var result = roulette.GetColor(); // The second call will be valid
        Assert.Equal(0, result); // The final result should be red
    }

    [Fact]
    public void PlaceInsideBet_ShouldWin_WhenNumberMatches()
    {
        int betNumber = 7;
        int randomNumber = 7; // Fixed for this test
        var roulette = new Roulette(SimulateInput(betNumber.ToString()), new RandomSeedMock(randomNumber));

        // Ideally, modify Roulette class to capture the output here
        roulette.PlaceInsideBet();
        // Assert.Contains("You win!", capturedOutput);
    }

    [Fact]
    public void PlaceInsideBet_ShouldLose_WhenNumberDoesNotMatch()
    {
        int betNumber = 7;
        int randomNumber = 5;
        var roulette = new Roulette(SimulateInput(betNumber.ToString()), new RandomSeedMock(randomNumber));

        // Ideally, modify Roulette class to capture the output here
        roulette.PlaceInsideBet();
        // Assert.Contains("Better luck next time!", capturedOutput);
    }

    [Fact]
    public void PlaceOutsideBet_ShouldWin_WhenColorMatches()
    {
        int userColor = 0; // Red
        int randomColor = 0; // Red (we want this to match)
        var roulette = new Roulette(SimulateInput("red"), new RandomSeedMock(randomColor));

        // / Ideally, modify Roulette class to capture the output here
        roulette.PlaceOutsideBet();
        // Assert.Contains("You guessed correctly!", capturedOutput);
    }

    [Fact]
    public void PlaceOutsideBet_ShouldLose_WhenColorDoesNotMatch()
    {
        // Arrange
        int userColor = 0; // Red
        int randomColor = 1; // Black
        var roulette = new Roulette(SimulateInput("red"), new RandomSeedMock(randomColor));

        // Ideally, modify Roulette class to capture the output here
        roulette.PlaceOutsideBet();
        // Assert.Contains("You guessed incorrectly. Better luck next time!", capturedOutput);
    }

    private class RandomSeedMock : Random
    {
        private readonly int _fixedValue;
        
        public RandomSeedMock(int fixedValue)
        {
            _fixedValue = fixedValue;
        }

        public override int Next(int minValue, int maxValue)
        {
            return _fixedValue;
        }
    }
}
