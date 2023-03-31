using System;
using System.Numerics;

namespace Hearthstone_CLI
{
    public class Card
    {
        public string Name { get; }
        public int Damage { get; }
        public int Cost { get; }
        public bool Attack { get; }
        public string Message { get; }

        public Card(string name, int damage, int cost,bool attack = false, string message =" " )
        {
            Name = name;
            Damage = damage;
            Cost = cost;
            Attack = attack;
            Message = message;
            
        }

        public void PlayCard(Player player, Player opponent)
        {
            if (player.Mana < Cost)
            {
                Console.WriteLine($"You don't have enough mana to play {Name}!");
                return;
            }

            Console.WriteLine($"{player.Name} plays {Name} and deals {Damage} damage to {opponent.Name}.");

            if (Damage > 0)
            {
                opponent.TakeDamage(Damage);
            }

            //player.Mana -= Cost;
            player.Hand.Remove(this);
            player.DiscardPile.Add(this);
        }

        public void DiscardCard(Player player)
        {
            player.Hand.Remove(this);
            player.DiscardPile.Add(this);
        }
    }

}

