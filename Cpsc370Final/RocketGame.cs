namespace Cpsc370Final
{
    public class RocketGame
    {
        private Random _random;
        private Player _player;

        public RocketGame(Player player)
        {
            _random = new Random();
            _player = player;
        }

        public RocketGame(Player player, Random rand)
        {
            _random = rand;
            _player = player;
        }

        // Tracking player's bet
        public double TemporaryBalance = 1000.0;
        public double BetAmount { get; set; }

        public bool Outcome { get; set; }        // true = rocket safe, false = rocket fails

        // Place a bet
        public void PlaceBet(double bet)
        {
            if (!_player.HasMoney())
            {
                Console.WriteLine("You don't have enough balance to place that bet.");
                throw new InvalidOperationException("Insufficient balance.");
            }
            BetAmount = bet;
            _player.RemoveMoney(BetAmount);
        }

        // Randomly determines the outcome for the round with a 90% chance of survival
        public void GenerateOutcome()
        {
            // 90% chance rocket is safe
            Outcome = (_random.NextDouble() < 0.90);
        }

        // Resets game state for a new game
        public void RestartGame()
        {
            BetAmount = 0;
            Outcome = false;
        }

        /// <summary>
        /// PlayRocketGame: This method runs the "loop" logic, allowing the player to keep playing.
        /// </summary>
        public void PlayRocketGame()
        {
            Console.WriteLine("Welcome to the Rocket Game!");

            while (_player.HasMoney())
            {
                // Prompt for initial bet
                _player.ShowStatus();
                Console.Write("Enter bet (or 0 to exit): ");
                string? betText = Console.ReadLine();
                if (!double.TryParse(betText, out double bet) || bet <= 0)
                {
                    if (bet == 0)
                    {
                        Console.WriteLine("Exiting Rocket Game...");
                        break;
                    }
                    Console.WriteLine("Invalid bet.");
                    continue;
                }
                try
                {
                    PlaceBet(bet);
                }
                catch (InvalidOperationException)
                {
                    continue;
                }

                bool keepGoing = true;

                while (keepGoing)
                {
                    // Generate rocket outcome
                    GenerateOutcome();

                    if (Outcome) // Rocket is safe
                    {
                        // Multiply the current bet amount
                        BetAmount = Math.Round(BetAmount * 1.1, 2);
                        Console.WriteLine($"\nThe rocket was safe! Your bet is now: {BetAmount}");

                        // Ask if the player wants to keep going
                        Console.Write("Do you want to keep going? (go/not go): ");
                        string? guessText = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(guessText))
                        {
                            Console.WriteLine("Invalid input.");
                            continue;
                        }
                        string guess = guessText.ToLower();
                        if (guess == "go")
                        {
                            // Player chooses to continue
                            continue;
                        }
                        else if (guess == "not go")
                        {
                            // Player chooses to cash out
                            _player.AddMoney(BetAmount, 1);

                            Console.WriteLine($"You cashed out with {BetAmount}!");
                            break; // Exit the inner loop
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please type 'go' or 'not go'.");
                            continue;
                        }
                    }
                    else // Rocket fails
                    {
                        Console.WriteLine("\nBoom! The rocket exploded. You lost your bet.");
                        BetAmount = 0;
                        break; // Exit the inner loop
                    }
                }

                // Check if the player has funds to keep playing
                if (!_player.HasMoney())
                {
                    Console.WriteLine("No more funds! Game over!");
                    break;
                }

                // Ask to continue or exit the game
                Console.Write("Do you want to play again? (y/n): ");
                string? playAgain = Console.ReadLine();
                if (playAgain?.ToLower() == "n")
                {
                    Console.WriteLine("Exiting Rocket Game...");
                    break;
                }
                else
                {
                    RestartGame();
                }
            }

            Console.WriteLine("Thanks for playing Rocket Game!");
        }
    }
}
