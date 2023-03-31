using System;
using System.Collections.Generic;

namespace Hearthstone_CLI
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Deck { get; } 
        public List<Card> Hand { get; }
        public List<Card> DiscardPile { get; }
        public List<Card> deck { get; }
        public int Mana { get; set; }
        public int HitPoints { get; set; }


        private readonly Random _random;

        public Player(string name)
        {
            Name = name;
            Deck = new List<Card>();
            Hand = new List<Card>();
            DiscardPile = new List<Card>();
            Mana = 0;
            deck = new List<Card>();
            HitPoints = 30;
            _random = new Random();
            
        }

        

        // Generates a deck of cards for the player according to the game rules
        public List<Card> GenerateDeck()
        {
           // List<Card> deck = new List<Card>();

            // Add 10 cards that deal 1 damage and cost 1 mana
            for (int i = 0; i < 10; i++)
            {
                Deck.Add(new Card("1 Damage", 1, 1));
            }

            // Add 4 cards that deal 2 damage and cost 2 mana
            for (int i = 0; i < 4; i++)
            {
                Deck.Add(new Card("2 Damage", 2, 2));
            }

            // Add 2 cards that deal 3 damage and cost 3 mana
            for (int i = 0; i < 2; i++)
            {
                Deck.Add(new Card("3 Damage", 3, 3));
            }

            // Add 2 cards that deal 4 damage and cost 4 mana
            for (int i = 0; i < 2; i++)
            {
                Deck.Add(new Card("4 Damage", 4, 4));
            }

            // Add 1 card that deals 4 damage, costs 5 mana, and gives an extra mana crystal
            Deck.Add(new Card("Legendary", 4, 5, true, "You will never defeat me!"));

            // Add 5 cards that heal for 1 hit point and cost 1 mana
            for (int i = 0; i < 5; i++)
            {
                Deck.Add(new Card("1 Heal", -1, 1));
            }

            // Add 2 cards that heal for 2 hit points and cost 2 mana
            for (int i = 0; i < 2; i++)
            {
                Deck.Add(new Card("2 Heal", -2, 2));
            }

            // Add 2 cards that deal 1 damage and draw another card, and cost 1 mana
            for (int i = 0; i < 2; i++)
            {
                Deck.Add(new Card("1 Damage + Draw", 1, 1, true));
            }

            // Shuffle the deck
            ShuffleDeck(Deck);

            return Deck;
        }

        public void DrawCard()
        {
            if (Deck.Count == 0)
            {
                Console.WriteLine($"{Name} has no cards left in their deck and loses 1 hit point!");
                TakeDamage(1);
                return;
            }
            GenerateDeck();
            Card card = Deck[_random.Next(Deck.Count)];
            Deck.Remove(card);
            Hand.Add(card);
            Console.WriteLine($"{Name} draws {card.Name}.");
        }

        public void PlayCard(Card card, Player opponent)
        {
            card.PlayCard(this, opponent);
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
            Console.WriteLine($"{Name} takes {damage} damage and now has {HitPoints} hit points remaining.");
            if (HitPoints <= 0)
            {
                Console.WriteLine($"{Name} has been defeated!");
            }
        }

        /*public void ShuffleDeck()
        {
            List<Card> shuffledDeck = new List<Card>();
            while (Deck.Count > 0)
            {
                int index = _random.Next(Deck.Count);
                Card card = Deck[index];
                Deck.RemoveAt(index);
                shuffledDeck.Add(card);
            }
            Deck.AddRange(shuffledDeck);
        }*/

        public void ResetMana()
        {
            Mana = Math.Min(10, Mana + 1);
        }
        
        public void TakeFatigueDamage()
        {
            if (Deck.Count == 0)
            {
                HitPoints--;
                Console.WriteLine($"{Name} took 1 fatigue damage.");
            }
        }


        

        // Shuffle the given deck using Fisher-Yates shuffle algorithm
        private void ShuffleDeck(List<Card> deck)
        {
            Random rng = new Random();

            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                Card temp = deck[j];
                deck[j] = deck[i];
                deck[i] = temp;
            }
        }


       /* public void StartTurn(Player opponent)
        {
            Console.WriteLine($"{Name}'s turn:");
            ResetMana();
            DrawCard();
            foreach (Card card in Hand.ToList())
            {
                PlayCard(card, opponent);
            }
        } */
    }


}




