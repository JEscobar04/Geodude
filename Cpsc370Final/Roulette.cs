using System;

public class Roulette
{
    private readonly Random random;
    private readonly Func<string> readLineFunc;

    // Constructor with injected dependencies
    public Roulette(Func<string> readLineFunc, Random random = null)
    {
        this.readLineFunc = readLineFunc ?? throw new ArgumentNullException(nameof(readLineFunc));
        this.random = random ?? new Random();
    }

    public void StartGame()
    {
        Console.WriteLine("Welcome to Roulette!");
        string betType = GetBetType();
        if (betType.ToLower() == "outside")
        {
            PlaceOutsideBet();
        }
        else if (betType.ToLower() == "inside")
        {
            PlaceInsideBet();
        }
        else
        {
            Console.WriteLine("Invalid bet type");
            StartGame();
        }
    }

    public string GetBetType()
    {
        Console.WriteLine("Would you like to place an Outside or Inside bet? \n (Type 'outside' or 'inside')");
        return readLineFunc(); // Delegate to injected function
    }

    public int GetColor()
    {
        while (true)
        {
            Console.WriteLine("Please choose a color ('red' or 'black'): ");
            string colorChoice = readLineFunc(); // Delegate to injected function

            if (colorChoice.ToLower() == "red")
            {
                return 0;  // Red
            }
            else if (colorChoice.ToLower() == "black")
            {
                return 1;  // Black
            }
            else
            {
                Console.WriteLine("Invalid color choice. Please type 'red' or 'black'.");
            }
        }
    }

    public void PlaceOutsideBet()
    {
        Console.WriteLine("You've chosen an outside bet.");
        int userColor = GetColor();
        int randomColor = random.Next(0, 2);

        if (userColor == randomColor)
        {
            Console.WriteLine("You guessed correctly!"); // Add payout logic here
        }
        else
        {
            Console.WriteLine("You guessed incorrectly. Better luck next time!");
        }
    }

    public void PlaceInsideBet()
    {
        Console.WriteLine("You've chosen an inside bet.");
        int betNumber = -1;

        while (betNumber < 0 || betNumber > 36)
        {
            Console.WriteLine("Please choose a number to bet on (0-36): ");
            string numberBet = readLineFunc(); // Delegate to injected function

            if (int.TryParse(numberBet, out betNumber) && betNumber >= 0 && betNumber <= 36)
            {
                Console.WriteLine($"You placed an inside bet on the number {betNumber}.");
            }
            else
            {
                Console.WriteLine("Invalid number! Please choose a number between 0 and 36.");
            }
        }

        int randomNumber = random.Next(0, 37);

        if (betNumber == randomNumber)
        {
            Console.WriteLine($"It landed on {betNumber}! You win!"); // Add payout logic here
        }
        else
        {
            Console.WriteLine($"It landed on {randomNumber}. Better luck next time!");
        }
    }
}
