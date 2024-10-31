using System;

namespace Case4
{
    public class Piece
    {
        private static readonly int[][,] Shapes = new int[][,]
        {
            new int[,] {{1, 1, 1, 1}},             // I
            new int[,] {{1, 1}, {1, 1}},           // O
            new int[,] {{0, 1, 0}, {1, 1, 1}},     // T
            new int[,] {{1, 0, 0}, {1, 1, 1}},     // L
            new int[,] {{0, 0, 1}, {1, 1, 1}}      // J
        };

        private int[,] shape;
        public int X { get; private set; }
        public int Y { get; private set; }

        public Piece(int[,] shape)
        {
            this.shape = shape;
            X = 0;
            Y = 3;
        }
        public void SpawnAtTop()
        {
            Y = 0; // Верхняя строка
            X = 3; // Центр по горизонтали
        }

        public static Piece GenerateRandomPiece()
        {
            Random random = new Random();
            int index = random.Next(Shapes.Length);
            return new Piece(Shapes[index]);
        }

        public void MoveDown(Field field)
        {
            if (CanMove(Y + 1, X, field)) Y++;
        }

        public void MoveLeft(Field field)
        {
            if (CanMove(Y, X - 1, field)) X--;
        }

        public void MoveRight(Field field)
        {
            if (CanMove(Y, X + 1, field)) X++;
        }

        public void Rotate(Field field)
        {
            int[,] rotated = RotateMatrix(shape);
            if (CanMove(Y, X, field, rotated))
                shape = rotated;
        }

        public bool HasLanded(Field field)
        {
            return !CanMove(Y + 1, X, field);
        }

        public int[,] GetShape() => shape;

        private bool CanMove(int newY, int newX, Field field, int[,] newShape = null)
        {
            newShape ??= shape;
            for (int i = 0; i < newShape.GetLength(0); i++)
            {
                for (int j = 0; j < newShape.GetLength(1); j++)
                {
                    if (newShape[i, j] == 1)
                    {
                        int newRow = newY + i;
                        int newCol = newX + j;
                        if (newRow >= field.Height || newCol < 0 || newCol >= field.Width || field.Matrix[newRow, newCol] == 1)
                            return false;
                    }
                }
            }
            return true;
        }

        private int[,] RotateMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            int[,] rotated = new int[m, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    rotated[j, n - 1 - i] = matrix[i, j];
            return rotated;
        }
    }
}
