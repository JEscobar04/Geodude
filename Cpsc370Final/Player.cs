namespace Cpsc370Final;

public class Player
{
    public double money { get; private set; }

    public Player(double startingMoney)
    {
        money = startingMoney;
    }

    public void ShowStatus()
    {
        Console.WriteLine($"You currently have ${money} to gamble.");
    }

    public void AddMoney(double moneyBet, double multiplier)
    {
        money += moneyBet * multiplier;
    }

    public void RemoveMoney(double moneyBet)
    {
        money -= moneyBet;
    }

    public bool HasMoney()
    {
        return money > 0.0;
    }

    public bool HasEnoughMoney(double bet)
    {
        return money >= bet;
        //throw new NotImplementedException();
    }
}