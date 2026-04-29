using System;
using System.Collections.Generic;
using System.Text;

    namespace LabyrinthEscape
    {

        class StaticField : GameField
        {
            public StaticField(int width, int height) : base(width, height) { }

            public override void GenerateMap()
            {               
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        Grid[y, x] = new Wall();

                for (int y = 1; y < Height - 1; y++)
                    for (int x = 1; x < Width - 1; x++)
                        if (y % 2 == 1 || x % 2 == 1)
                            Grid[y, x] = new EmptyPath();

                Grid[1, 1] = new EmptyPath(); 
                Grid[1, 3] = new Chest(150);
                Grid[3, 3] = new Guard("Стражник А", 25);
                Grid[3, 5] = new Chest(100);
                Grid[5, 5] = new Guard("Стражник Б", 35);
                Grid[Height - 2, Width - 2] = new Exit();

                Console.WriteLine("Статический лабиринт создан.");
            }
        }

        class DynamicField : GameField
        {
            private int _moveCount = 0;
            private const int ShiftInterval = 5;

            public DynamicField(int width, int height) : base(width, height) { }

            public override void GenerateMap()
            {
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        Grid[y, x] = new Wall();

                for (int y = 1; y < Height - 1; y++)
                    for (int x = 1; x < Width - 1; x++)
                        Grid[y, x] = new EmptyPath();

                Grid[1, 3] = new Chest(120);
                Grid[3, 1] = new Guard("Маг-стражник", 40);
                Grid[Height - 2, Width - 2] = new Exit();

                Console.WriteLine("Динамический лабиринт создан — стены будут меняться!");
            }

            public void OnMove()
            {
                _moveCount++;
                if (_moveCount % ShiftInterval == 0)
                {
                    Console.WriteLine("\n*** Магия меняет стены лабиринта! ***");
                    ShiftWalls();
                }
            }

            private void ShiftWalls()
            {
                var rnd = new Random();
                for (int i = 0; i < 3; i++)
                {
                    int x = rnd.Next(1, Width - 1);
                    int y = rnd.Next(1, Height - 1);
                    if (Grid[y, x] is EmptyPath)
                        Grid[y, x] = new Wall();
                    else if (Grid[y, x] is Wall)
                        Grid[y, x] = new EmptyPath();
                }
            }
        }

        class TrapField : GameField
        {
            public TrapField(int width, int height) : base(width, height) { }

            public override void GenerateMap()
            {
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        Grid[y, x] = new Wall();

                for (int y = 1; y < Height - 1; y++)
                    for (int x = 1; x < Width - 1; x++)
                        Grid[y, x] = new EmptyPath();

                // Расставляем ловушки (стражники в неожиданных местах)
                Grid[2, 2] = new Guard("Ловушка-призрак", 20);
                Grid[2, 4] = new Guard("Ловушка-шипы", 15);
                Grid[4, 2] = new Chest(200);
                Grid[3, 3] = new Guard("Ловушка-паук", 30);
                Grid[Height - 2, Width - 2] = new Exit();

                Console.WriteLine("Лабиринт с ловушками создан — осторожно!");
            }
        }
    }
