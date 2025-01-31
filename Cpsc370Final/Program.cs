namespace Cpsc370Final;

class Program
{
    static void Main(string[] args)
    {
        Player gambler = new Player(100.0);
        CasinoMainMenu mainMenu = new CasinoMainMenu();
        Console.WriteLine("Welcome to Casino Game!");
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
                    Roulette rouletteGame = new Roulette(gambler);
                    rouletteGame.StartGame();
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