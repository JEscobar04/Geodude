namespace Cpsc370Final;

using System;
using System.Collections.Generic;

public class Blackjack
{
    private static double playerChips = 1000; // Starting chip count
    private static double currentBet = 0;
    private Player player;

    public Blackjack(Player player)
    {
        this.player = player;
    }

    public void PlayGame()
{
    while (true) // Loop for replay functionality
    {
        // Display current chip balance
        player.ShowStatus();
        
        // Ask the player for a bet amount
        Console.WriteLine("Enter your bet amount:");
        string betInput = Console.ReadLine();
        if (double.TryParse(betInput, out double bet) && bet > 0.0 && bet <= playerChips)
        {
            currentBet = bet;
            player.RemoveMoney(currentBet); // Deduct the bet from player's chips
            Console.WriteLine("You have bet $" + currentBet + ".");
        }
        else
        {
            Console.WriteLine("Invalid bet. Please enter a valid amount.");
            continue;
        }

        // Create a new deck
        Deck deck = new Deck();

        // Deal two cards to the player and two cards to the dealer
        Hand playerHand = new Hand();
        Hand dealerHand = new Hand();

        playerHand.AddCard(deck.Deal());
        playerHand.AddCard(deck.Deal());

        dealerHand.AddCard(deck.Deal());
        dealerHand.AddCard(deck.Deal());

        if (playerHand.HasBlackJack())
        { 
            Console.WriteLine("Player got Blackjack!");
            player.AddMoney(currentBet, 2);
        }
        else if(dealerHand.HasBlackJack())
            Console.WriteLine("Dealer got Blackjack!");
        else
        {
            // Print the initial hands
        Console.WriteLine("Player's hand:");
        Console.WriteLine(playerHand);

        Console.WriteLine("\nDealer's hand (one card hidden):");
        Console.WriteLine(dealerHand.GetCard(0));  // Show only one dealer card

        // Check for Double Down option
        bool playerDoubledDown = false;
        if (playerHand.CanDoubleDown())
        {
            Console.WriteLine("\nYou can Double Down! Would you like to double your bet? (yes/no)");
            string doubleDownResponse = Console.ReadLine().ToLower();
            if (doubleDownResponse == "yes")
            {
                currentBet *= 2; // Double the bet
                player.RemoveMoney(currentBet);
                Console.WriteLine($"You have doubled your bet. Your new bet is now ${currentBet}.");
                playerHand.AddCard(deck.Deal()); // Deal one more card to the player
                Console.WriteLine(playerHand);
                playerDoubledDown = true;
                break; // End player's turn immediately after doubling down
            }
        }

        // Player's turn: choose to hit, stand, or split (if they haven't doubled down)
        if (!playerDoubledDown)
        {
            string action = "";
            while (action != "stand" && playerHand.GetTotalValue() < 21)
            {
                Console.WriteLine("\nYour hand value: " + playerHand.GetTotalValue() + "\tDealer's hand value: " + dealerHand.GetCard(0).GetValue()); 
                Console.WriteLine("Would you like to 'hit' or 'stand'?");

                // Only show split option if the player has a splittable hand
                action = Console.ReadLine().ToLower();

                if (action == "hit")
                {
                    Console.WriteLine("\nPlayer takes a hit...");
                    playerHand.AddCard(deck.Deal());
                    Console.WriteLine(playerHand);
                }
                else if (action != "stand")
                {
                    Console.WriteLine("Invalid choice. Please type 'hit' or 'stand'.");
                }
            }
        }

        // Dealer's turn (dealer hits until reaching 17 or more)
        Console.WriteLine("\nDealer's turn...");
        while (dealerHand.GetTotalValue() < 17)
        {
            dealerHand.AddCard(deck.Deal());
            Console.WriteLine(dealerHand);
        }

        // Determine the winner and calculate payout
        DetermineWinner(playerHand, dealerHand);

        // Ask the player if they want to play again
        Console.WriteLine("\nWould you like to play again? (yes/no)");
        string playAgain = Console.ReadLine().ToLower();

        if (playAgain != "yes")
        {
            Console.WriteLine("Thanks for playing!");
            break; // Exit the loop to end the game
        }

        Console.WriteLine(); // Add a blank line for spacing between rounds
        }
    }
}


    private static void PlayHand(Hand hand, Deck deck)
    {
        string action = "";
        while (action != "stand" && hand.GetTotalValue() < 21)
        {
            Console.WriteLine("\nYour hand value: " + hand.GetTotalValue());
            Console.WriteLine("Would you like to 'hit' or 'stand'?");
            action = Console.ReadLine().ToLower();

            if (action == "hit")
            {
                Console.WriteLine("\nPlayer takes a hit...");
                hand.AddCard(deck.Deal());
                Console.WriteLine(hand);
            }
            else if (action != "stand")
            {
                Console.WriteLine("Invalid choice. Please type 'hit' or 'stand'.");
            }
        }
    }

    private void DetermineWinner(Hand playerHand, Hand dealerHand)
    {
        Console.WriteLine("\nFinal hands:");
        Console.WriteLine($"Player's hand: {playerHand} (Total: {playerHand.GetTotalValue()})");
        Console.WriteLine($"Dealer's hand: {dealerHand} (Total: {dealerHand.GetTotalValue()})");

        int playerTotal = playerHand.GetTotalValue();
        int dealerTotal = dealerHand.GetTotalValue();

        if (playerTotal > 21)
        {
            Console.WriteLine("Player busts! Dealer wins.");
        }
        else if (dealerTotal > 21)
        {
            Console.WriteLine("Dealer busts! Player wins.");
            player.AddMoney(currentBet, 2); // Player wins and gets double the bet
        }
        else if (playerTotal > dealerTotal)
        {
            Console.WriteLine("Player wins!");
            player.AddMoney(currentBet, 2); // Player wins and gets double the bet
        }
        else if (dealerTotal > playerTotal)
        {
            Console.WriteLine("Dealer wins!");
        }
        else
        {
            Console.WriteLine("It's a tie! You keep the money you bet!");
            player.AddMoney(currentBet, 1); // Player wins and gets double the bet
        }

        Console.WriteLine($"You now have ${player.money}.");
    }
}