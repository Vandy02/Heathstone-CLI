using System;

namespace Hearthstone_CLI
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hearthstone!");

            // Create two players

          
            Player player1 = new Player("Player 1");
            player1.GenerateDeck();
            Player player2 = new Player("Player 2");
            player2.GenerateDeck();

            // Create a game and start it
            Game game = new Game(player1, player2);
            game.Play();

            // Display the end-of-game report
           // Console.WriteLine("Game over! Here's the end-of-game report:");
            //Console.WriteLine(game.DisplayEndGameReport());

            Console.ReadLine();
        }

    }
}
