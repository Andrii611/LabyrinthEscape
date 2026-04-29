using System;
using System.Collections.Generic;
using System.Text;

namespace LabyrinthEscape
{
    abstract class Prisoner
    {
        public string Name { get; private set; }

        private int _health;
        public int Health
        {
            get => _health;
            private set => _health = Math.Max(0, value); // не ниже нуля
        }

        private int _stamina;
        public int Stamina
        {
            get => _stamina;
            private set => _stamina = Math.Max(0, Math.Min(100, value));
        }

        public List<string> Inventory { get; private set; }
        public int Score { get; protected set; }
        public bool IsAlive => Health > 0;
        public bool SpecialUsed { get; protected set; } = false;

        protected Prisoner(string name, int health, int stamina)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя персонажа не может быть пустым!");
            if (health <= 0 || health > 200)
                throw new ArgumentException("Здоровье должно быть от 1 до 200!");
            if (stamina < 0 || stamina > 100)
                throw new ArgumentException("Выносливость должна быть от 0 до 100!");

            Name = name;
            Health = health;
            Stamina = stamina;
            Inventory = new List<string>();
            Score = 0;
        }

        public abstract void UseSpecialAbility(GameField field);

        public void Move(string direction)
        {
            if (Stamina < 5)
            {
                Console.WriteLine($"{Name}: не хватает выносливости для движения!");
                return;
            }
            Stamina -= 5;
            Console.WriteLine($"{Name} движется {direction}. Выносливость: {Stamina}");
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0) throw new ArgumentException("Урон не может быть отрицательным!");
            Health -= amount;
            Console.WriteLine($"{Name} получает {amount} урона. Здоровье: {Health}");
            if (!IsAlive)
                Console.WriteLine($"{Name} погиб в лабиринте...");
        }

        public void Heal(int amount)
        {
            if (amount < 0) throw new ArgumentException("Лечение не может быть отрицательным!");
            Health += Math.Min(amount, 200 - Health);
            Console.WriteLine($"{Name} восстанавливает {amount} здоровья. Здоровье: {Health}");
        }

        public void AddToInventory(string item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{Name} подбирает: {item}");
        }

        public void AddScore(int points)
        {
            Score += points;
        }

        public void PrintStats()
        {
            Console.WriteLine($"\n[ {Name} | HP: {Health} | Выносливость: {Stamina} | Очки: {Score} | Инвентарь: {Inventory.Count} предм. ]");
        }
    }
}
