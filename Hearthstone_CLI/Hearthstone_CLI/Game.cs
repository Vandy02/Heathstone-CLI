using System;
namespace Hearthstone_CLI
{
    public class Game
    {
        private Player player1;
        private Player player2;
        private Player activePlayer;
        private Player waitingPlayer;
        private bool gameOver;

        public Game(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            activePlayer = player1;
            waitingPlayer = player2;
            gameOver = false;
           
        }


        public void Play()
        {
            while (!gameOver)
            {
                // Start of turn
                activePlayer.DrawCard();
                activePlayer.DrawCard();
                activePlayer.DrawCard();
                activePlayer.DrawCard();
                activePlayer.ResetMana();

                // Main phase
                bool hasPlayedCard = false;
                while (!hasPlayedCard)
                {
                    Console.WriteLine($"{activePlayer.Name}'s turn. Mana: {activePlayer.Mana}");
                    Console.WriteLine("Your hand:");
                    for (int i = 0; i < activePlayer.Hand.Count; i++)
                    {
                        Console.WriteLine($"({i + 1}) {activePlayer.Hand[i].Name}");
                        //{activePlayer.Hand[i]}
                    }
                    Console.WriteLine("Select a card to play (0 to end turn):");
                    int cardIndex;
                    while (!int.TryParse(Console.ReadLine(), out cardIndex) || cardIndex < 0 || cardIndex > activePlayer.Hand.Count)
                    {
                        Console.WriteLine("Invalid input. Select a card to play (0 to end turn):");
                    }
                    if (cardIndex == 0)
                    {
                        break;
                    }
                    Card selectedCard = activePlayer.Hand[cardIndex - 1];
                    if (selectedCard.Cost <= activePlayer.Mana)
                    {
                        activePlayer.PlayCard(selectedCard, waitingPlayer);
                        Console.WriteLine($"{activePlayer.Name} played {selectedCard.Name}.");
                        hasPlayedCard = true;
                    }
                    else
                    {
                        Console.WriteLine($"Not enough mana to play {selectedCard.Name}.");
                    }
                }

                // End of turn
                activePlayer = (activePlayer == player1) ? player2 : player1;
                if (activePlayer.HitPoints <= 0)
                {
                    Console.WriteLine($"{activePlayer.Name} has been defeated!");
                    gameOver = true;
                    break;
                }
                if (activePlayer.Deck.Count == 0)
                {
                    activePlayer.TakeFatigueDamage();
                    Console.WriteLine($"{activePlayer.Name} takes fatigue damage!");
                    if (activePlayer.HitPoints <= 0)
                    {
                        Console.WriteLine($"{activePlayer.Name} has been defeated!");
                        gameOver = true;
                        break;
                    }
                }
               
            }

            Console.WriteLine("Game over.");

            // End of game report
            Game.DisplayEndGameReport(player1, player2);

        }

        public static void DisplayEndGameReport(Player player1, Player player2)
        {
            Console.WriteLine("End of game report:");
            Console.WriteLine("-------------------");

            Console.WriteLine($"Player 1 ({player1.Name}):");
            Console.WriteLine($"- Deck: {string.Join(", ", player1.Deck)}");
            Console.WriteLine($"- Hit Points: {player1.HitPoints}");
            Console.WriteLine($"- Mana: {player1.Mana}");
            Console.WriteLine($"- Cards in Hand: {string.Join(", ", player1.Hand)}");

            Console.WriteLine();

            Console.WriteLine($"Player 2 ({player2.Name}):");
            Console.WriteLine($"- Deck: {string.Join(", ", player2.Deck)}");
            Console.WriteLine($"- Hit Points: {player2.HitPoints}");
            Console.WriteLine($"- Mana: {player2.Mana}");
            Console.WriteLine($"- Cards in Hand: {string.Join(", ", player2.Hand)}");

            Console.WriteLine();

            if (player1.HitPoints <= 0)
            {
                Console.WriteLine($"{player2.Name} wins!");
            }
            else if (player2.HitPoints <= 0)
            {
                Console.WriteLine($"{player1.Name} wins!");
            }
            else
            {
                Console.WriteLine("The game ended in a draw.");
            }
        }


    }


}

