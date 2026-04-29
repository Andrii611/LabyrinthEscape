using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace LabyrinthEscape
{
    class Rogue : Prisoner
    {
        public Rogue(string name) : base(name, 80, 100) { }

        public override void UseSpecialAbility(GameField field)
        {
            if (SpecialUsed)
            {
                Console.WriteLine($"{Name}: способность уже использована!");
                return;
            }
            Console.WriteLine($"{Name} использует НЕВИДИМОСТЬ — стражники игнорируют его следующий ход!");
            SpecialUsed = true;
            AddScore(30);
        }

        public override string ToString() => $"Вор [{Name}]";
    }

    class Warrior : Prisoner
    {
        public Warrior(string name) : base(name, 150, 70) { }

        public override void UseSpecialAbility(GameField field)
        {
            if (SpecialUsed)
            {
                Console.WriteLine($"{Name}: способность уже использована!");
                return;
            }
            Console.WriteLine($"{Name} использует БОЕВОЙ КЛИЧ — все стражники на карте оглушены на 2 хода!");
            SpecialUsed = true;
            AddScore(20);
        }

        public override string ToString() => $"Воин [{Name}]";
    }

    class Mage : Prisoner
    {
        public Mage(string name) : base(name, 70, 90) { }

        public override void UseSpecialAbility(GameField field)
        {
            if (SpecialUsed)
            {
                Console.WriteLine($"{Name}: способность уже использована!");
                return;
            }
            Console.WriteLine($"{Name} использует ТЕЛЕПОРТАЦИЮ — мгновенно перемещается к ближайшему сундуку!");
            SpecialUsed = true;
            AddScore(50);
        }

        public override string ToString() => $"Маг [{Name}]";
    }

    class Scout : Prisoner
    {
        public Scout(string name) : base(name, 90, 100) { }

        public override void UseSpecialAbility(GameField field)
        {
            if (SpecialUsed)
            {
                Console.WriteLine($"{Name}: способность уже использована!");
                return;
            }
            Console.WriteLine($"{Name} использует РАЗВЕДКУ — видит всю карту лабиринта!");
            field.RevealAll();
            SpecialUsed = true;
            AddScore(15);
        }

        public override string ToString() => $"Разведчик [{Name}]";
    }

    class Alchemist : Prisoner
    {
        public Alchemist(string name) : base(name, 100, 80) { }

        public override void UseSpecialAbility(GameField field)
        {
            if (SpecialUsed)
            {
                Console.WriteLine($"{Name}: способность уже использована!");
                return;
            }
            Console.WriteLine($"{Name} варит ЗЕЛЬЕ ИСЦЕЛЕНИЯ — восстанавливает 50 здоровья!");
            Heal(50);
            SpecialUsed = true;
            AddScore(25);
        }

        public override string ToString() => $"Алхимик [{Name}]";
    }
}
