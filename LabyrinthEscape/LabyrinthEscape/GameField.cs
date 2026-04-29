using System;
using System.Collections.Generic;
using System.Text;

namespace LabyrinthEscape
{
    abstract class GameField
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        protected IInteractable[,] Grid;
        protected bool revealed = false;

        protected GameField(int width, int height)
        {
            if (width < 5 || width > 50)
                throw new ArgumentException("Ширина поля должна быть от 5 до 50!");
            if (height < 5 || height > 50)
                throw new ArgumentException("Высота поля должна быть от 5 до 50!");

            Width = width;
            Height = height;
            Grid = new IInteractable[height, width];
        }

        public abstract void GenerateMap();

        public void DrawToConsole()
        {
            Console.WriteLine();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    char sym = Grid[y, x].GetSymbol();
                    Console.Write(sym + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool IsPassable(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return false;
            return Grid[y, x].IsPassable();
        }

        public IInteractable GetCell(int x, int y) => Grid[y, x];

        public void SetCell(int x, int y, IInteractable obj) => Grid[y, x] = obj;

        public void RevealAll()
        {
            revealed = true;
            Console.WriteLine("Вся карта открыта!");
            DrawToConsole();
        }
    }
}
