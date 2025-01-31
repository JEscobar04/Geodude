namespace Cpsc370Final;

class Program
{
    static void Main(string[] args)
    {
        // Player gambler = new Player(100.0);
        CasinoMainMenu mainMenu = new CasinoMainMenu();
        
        double startingChips = 100.0;
        if (args.Length > 0 && double.TryParse(args[0], out double parsedChips) && parsedChips > 0)
        {
            startingChips = parsedChips;
        }
        else if (args.Length > 0)
        {
            Console.WriteLine($"Invalid input. Starting with default chips: {startingChips}.");
        }
        
        Player gambler = new Player(startingChips);
        Console.WriteLine("Welcome to the Casino Game!");
        gambler.ShowStatus();
        bool exit = false;
        while (!exit)
        {
            mainMenu.ShowMenu();
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("You chose Rocket Game!");
                    RocketGame rocketGame = new RocketGame(gambler);
                    rocketGame.PlayRocketGame();
                    break;

                case "2":
                    Console.WriteLine("You chose Roulette!");
                    Roulette roulette = new Roulette(gambler);
                    roulette.StartGame();
                    break;

                case "3":
                    Console.WriteLine("You chose Blackjack!");
                    Blackjack blackjackGame = new Blackjack(gambler);
                    blackjackGame.PlayGame();
                    break;

                case "4":
                    Console.WriteLine("Showing Player Information...");
                    gambler.ShowStatus();
                    break;

                case "5":
                    Console.WriteLine("Exiting the Casino...");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }  
    }

    // this is just an example of how to get the command
    // line arguments so you can use them
}