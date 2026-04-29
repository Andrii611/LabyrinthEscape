namespace LabyrinthEscape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== LABYRINTH ESCAPE ===\n");

            Console.WriteLine("Выберите класс персонажа:");
            Console.WriteLine("1 - Вор       (HP: 80,  Выносливость: 100) | Способность: Невидимость");
            Console.WriteLine("2 - Воин      (HP: 150, Выносливость: 70 ) | Способность: Боевой клич");
            Console.WriteLine("3 - Маг       (HP: 70,  Выносливость: 90 ) | Способность: Телепортация");
            Console.WriteLine("4 - Разведчик (HP: 90,  Выносливость: 100) | Способность: Открыть карту");
            Console.WriteLine("5 - Алхимик   (HP: 100, Выносливость: 80 ) | Способность: Зелье лечения");
            Console.Write("\nВаш выбор (1-5): ");

            string classChoice = Console.ReadLine()?.Trim() ?? "1";
            Console.Write("Введите имя персонажа: ");
            string name = Console.ReadLine()?.Trim() ?? "Герой";
            if (string.IsNullOrWhiteSpace(name)) name = "Герой";

            Prisoner player = classChoice switch
            {
                "2" => new Warrior(name),
                "3" => new Mage(name),
                "4" => new Scout(name),
                "5" => new Alchemist(name),
                _ => new Rogue(name),
            };

            // Выбор типа лабиринта
            Console.WriteLine("\nВыберите тип лабиринта:");
            Console.WriteLine("1 - Статический  (классический, стены не меняются)");
            Console.WriteLine("2 - Динамический (стены меняются каждые 5 ходов)");
            Console.WriteLine("3 - С ловушками  (скрытые опасности)");
            Console.Write("\nВаш выбор (1-3): ");

            string mapChoice = Console.ReadLine()?.Trim() ?? "1";

            GameField field = mapChoice switch
            {
                "2" => new DynamicField(9, 7),
                "3" => new TrapField(9, 7),
                _ => new StaticField(9, 7),
            };

            var game = new Game(player, field);
            game.Start();
        }
    }
}
