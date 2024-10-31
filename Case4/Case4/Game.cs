using System;
using System.Threading;

namespace Case4
{
    public class Game
    {
        private Field field;
        private Piece currentPiece;
        private bool isGameOver;

        public Game()
        {
            field = new Field(20, 10);
            isGameOver = false;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в Тетрис! Нажмите любую клавишу, чтобы начать...");
            Console.ReadKey();

            while (true)
            {
                RunGameLoop();

                Console.Clear();
                Console.WriteLine("Игра окончена! Нажмите R, чтобы начать заново, или любую другую клавишу, чтобы выйти.");
                var key = Console.ReadKey();
                if (key.Key != ConsoleKey.R) break;

                RestartGame();
            }
        }

        private void RunGameLoop()
        {
            isGameOver = false;

            while (!isGameOver)
            {
                currentPiece = Piece.GenerateRandomPiece();
                currentPiece.SpawnAtTop(); // Новое появление в верхней части поля

                while (!currentPiece.HasLanded(field))
                {
                    Render();
                    HandleInput();
                    currentPiece.MoveDown(field);
                    Thread.Sleep(500);
                }

                field.PlacePiece(currentPiece);
                field.ClearFullLines();
                isGameOver = field.CheckGameOver();
            }
        }

        private void HandleInput()
        {
            if (!Console.KeyAvailable) return;
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    currentPiece.MoveLeft(field);
                    break;
                case ConsoleKey.RightArrow:
                    currentPiece.MoveRight(field);
                    break;
                case ConsoleKey.UpArrow:
                    currentPiece.Rotate(field);
                    break;
                case ConsoleKey.DownArrow:
                    currentPiece.MoveDown(field);
                    break;
            }
        }

        private void Render()
        {
            Console.Clear();
            field.Render(currentPiece);
            Console.WriteLine("Используйте стрелки для управления фигурой.");
        }

        private void RestartGame()
        {
            field = new Field(20, 10);
        }
    }
}
