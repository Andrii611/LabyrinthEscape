using System;
using System.Collections.Generic;
using System.Text;

namespace LabyrinthEscape
{
    class Wall : IInteractable
    {
        public void Interact(Prisoner player)
        {
            Console.WriteLine("Это стена — путь заблокирован.");
        }
        public bool IsPassable() => false;
        public char GetSymbol() => '#';
    }

    class EmptyPath : IInteractable
    {
        public void Interact(Prisoner player) { }
        public bool IsPassable() => true;
        public char GetSymbol() => '.';
    }

    class Chest : IInteractable
    {
        private bool _opened = false;
        private int _points;

        public Chest(int points = 100)
        {
            if (points < 0) throw new ArgumentException("Очки сундука не могут быть отрицательными!");
            _points = points;
        }

        public void Interact(Prisoner player)
        {
            if (_opened)
            {
                Console.WriteLine("Сундук уже открыт.");
                return;
            }
            player.AddScore(_points);
            player.AddToInventory($"Сокровище ({_points} очков)");
            _opened = true;
            Console.WriteLine($"Сундук открыт! +{_points} очков!");
        }

        public bool IsPassable() => true;
        public char GetSymbol() => _opened ? '.' : 'C';
    }

    class Guard : IInteractable
    {
        private int _damage;
        private string _name;

        public Guard(string name = "Стражник", int damage = 30)
        {
            if (damage < 0) throw new ArgumentException("Урон стражника не может быть отрицательным!");
            _name = name;
            _damage = damage;
        }

        public void Interact(Prisoner player)
        {
            Console.WriteLine($"{_name} атакует!");
            player.TakeDamage(_damage);
        }

        public bool IsPassable() => true;
        public char GetSymbol() => 'G';
    }

    class Exit : IInteractable
    {
        public bool Reached { get; private set; } = false;

        public void Interact(Prisoner player)
        {
            Reached = true;
            player.AddScore(200);
            Console.WriteLine($"\n*** {player.Name} нашёл выход! +200 очков за побег! ***");
        }

        public bool IsPassable() => true;
        public char GetSymbol() => 'E';
    }
}
