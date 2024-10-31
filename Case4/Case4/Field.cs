using System;

namespace Case4
{
    public class Field
    {
        public int[,] Matrix { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Field(int height, int width)
        {
            Height = height;
            Width = width;
            Matrix = new int[height, width];
        }

        public void PlacePiece(Piece piece)
        {
            var shape = piece.GetShape();
            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                    if (shape[i, j] == 1)
                        Matrix[piece.Y + i, piece.X + j] = 1;
        }

        public void ClearFullLines()
        {
            for (int i = 0; i < Height; i++)
            {
                bool isFull = true;
                for (int j = 0; j < Width; j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    for (int k = i; k > 0; k--)
                        for (int j = 0; j < Width; j++)
                            Matrix[k, j] = Matrix[k - 1, j];
                }
            }
        }

        public void Render(Piece currentPiece)
        {
            var tempMatrix = (int[,])Matrix.Clone();
            var shape = currentPiece.GetShape();

            for (int i = 0; i < shape.GetLength(0); i++)
                for (int j = 0; j < shape.GetLength(1); j++)
                    if (shape[i, j] == 1)
                        tempMatrix[currentPiece.Y + i, currentPiece.X + j] = 1;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write(tempMatrix[i, j] == 1 ? "█" : " ");
                Console.WriteLine();
            }
        }

        public bool CheckGameOver()
        {
            for (int i = 0; i < Width; i++)
                if (Matrix[0, i] == 1)
                    return true;
            return false;
        }
    }
}
