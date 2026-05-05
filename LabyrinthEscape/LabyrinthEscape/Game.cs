using System;
using System.Collections.Generic;
using System.Text;

namespace LabyrinthEscape
{
    class Game
    {
        private Prisoner _player;
        private GameField _field;
        private int _playerX;
        private int _playerY;
        private int _moveCount;
        private bool _gameOver;

        public Game(Prisoner player, GameField field)
        {
            _player = player;
            _field = field;
            _playerX = 1;
            _playerY = 1;
            _moveCount = 0;
            _gameOver = false;
        }

        public void Start()
        {
            _field.GenerateMap();
            Console.WriteLine($"\n=== LABYRINTH ESCAPE ===");
            Console.WriteLine($"Персонаж: {_player}");
            Console.WriteLine("Управление: W/A/S/D — движение, U — способность, M — карта, Q — выход\n");
            _field.DrawToConsole(_playerX, _playerY);

            while (!_gameOver && _player.IsAlive)
            {
                _player.PrintStats();
                Console.Write("Действие: ");
                string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
                HandleInput(input);
            }

            ShowResult();
        }

        private void HandleInput(string input)
        {
            int newX = _playerX;
            int newY = _playerY;
            string dir = "";

            switch (input)
            {
                case "W": newY--; dir = "вверх"; break;
                case "S": newY++; dir = "вниз"; break;
                case "A": newX--; dir = "влево"; break;
                case "D": newX++; dir = "вправо"; break;
                case "U":
                    _player.UseSpecialAbility(_field);
                    return;
                case "M":
                    _field.DrawToConsole(_playerX, _playerY);
                    return;
                case "Q":
                    _gameOver = true;
                    Console.WriteLine("Вы сдались...");
                    return;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    return;
            }

            if (!_field.IsPassable(newX, newY))
            {
                Console.WriteLine("Путь заблокирован!");
                return;
            }

            _player.Move(dir);
            _playerX = newX;
            _playerY = newY;
            _moveCount++;

            var cell = _field.GetCell(_playerX, _playerY);
            cell.Interact(_player);

            if (cell is Exit exit && exit.Reached)
            {
                _gameOver = true;
                return;
            }

            if (_field is DynamicField dynamicField)
                dynamicField.OnMove();

            _field.DrawToConsole(_playerX, _playerY);
        }

        private void MarkPlayer()
        {
            Console.WriteLine($"Ваша позиция: ({_playerX}, {_playerY})");
        }

        private void ShowResult()
        {
            Console.WriteLine("\n========== ИТОГ ==========");
            Console.WriteLine($"Персонаж:    {_player}");
            Console.WriteLine($"Здоровье:    {_player.Health}");
            Console.WriteLine($"Ходов:       {_moveCount}");
            Console.WriteLine($"Инвентарь:   {_player.Inventory.Count} предметов");

            // Штраф за количество ходов
            int penalty = _moveCount * 2;
            int finalScore = Math.Max(0, _player.Score - penalty);

            Console.WriteLine($"Очки (до штрафа): {_player.Score}");
            Console.WriteLine($"Штраф за ходы:    -{penalty}");
            Console.WriteLine($"ИТОГОВЫЙ СЧЁТ:    {finalScore}");

            if (!_player.IsAlive)
                Console.WriteLine("Результат: ПОГИБ В ЛАБИРИНТЕ");
            else if (_moveCount <= 15)
                Console.WriteLine("Результат: МАСТЕР ПОБЕГА!");
            else if (_moveCount <= 30)
                Console.WriteLine("Результат: УДАЧНЫЙ ПОБЕГ");
            else
                Console.WriteLine("Результат: ПОБЕГ УДАЛСЯ, НО С ТРУДОМ");

            Console.WriteLine("===========================");
        }
    }
}
