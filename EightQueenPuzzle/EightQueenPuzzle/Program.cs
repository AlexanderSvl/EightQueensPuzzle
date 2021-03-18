using System;
using System.Collections.Generic;
using System.IO;

namespace EightQueenPuzzle
{
    class Program
    {
        public static StreamWriter writer = new StreamWriter("output.txt");

        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter chess board size: ");

                using (writer)
                {
                    Console.WriteLine();
                    PlaceQueens(0);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    writer.WriteLine("Possible solutions: " + solutionsCount);
                    Console.WriteLine("Possible solutions: " + solutionsCount);
                }                

                Console.ResetColor();
            }
            catch (Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.ToString());
                Console.ResetColor();
            }
            
        }

        public static int Size = int.Parse(Console.ReadLine());

        static bool[,] chessBoard = new bool[Size, Size];
        static int solutionsCount = 0;

        static HashSet<int> attackedRows = new HashSet<int>();
        static HashSet<int> attackedCols = new HashSet<int>();
        static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        static HashSet<int> attackedRightDiagonals = new HashSet<int>();

        private static void PlaceQueens(int row)
        {
            if (row == Size)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAllAttacked(row, col);
                        PlaceQueens(row + 1);
                        UnmarkAllAttacked(row, col);
                    }
                }
            }
        }

        private static bool CanPlaceQueen(int row, int col)
        {
            int leftDiagonal = col - row;
            int rightDiagonal = col + row;

            if (attackedRows.Contains(row)
                || attackedCols.Contains(col)
                || attackedLeftDiagonals.Contains(leftDiagonal)
                || attackedRightDiagonals.Contains(rightDiagonal))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void MarkAllAttacked(int row, int col)
        {
            int leftDiagonal = col - row;
            int rightDiagonal = col + row;

            attackedRows.Add(row);
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(leftDiagonal);
            attackedRightDiagonals.Add(rightDiagonal);

            chessBoard[row, col] = true;
        }

        private static void UnmarkAllAttacked(int row, int col)
        {
            int leftDiagonal = col - row;
            int rightDiagonal = col + row;

            attackedRows.Remove(row);
            attackedCols.Remove(col);
            attackedLeftDiagonals.Remove(leftDiagonal);
            attackedRightDiagonals.Remove(rightDiagonal);

            chessBoard[row, col] = false;
        }

        private static void PrintSolution()
        {
            solutionsCount++;

            Console.ForegroundColor = ConsoleColor.Yellow;
            writer.WriteLine("Solution #" + solutionsCount + ":");
            Console.WriteLine("Solution #" + solutionsCount + ":");

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (chessBoard[row, col] == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        writer.Write("*");
                        Console.Write("*");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        writer.Write("-");
                        Console.Write("-");
                    }
                }

                writer.WriteLine();
                Console.WriteLine();
            }

            writer.WriteLine();
            Console.WriteLine();
        }
    }
}
