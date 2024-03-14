using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Tester
    {
        private static Random rng = new Random();

        public enum Difficulty
        {
            EMPTY,
            HARD,
            MEDIUM,
            EASY,
            END,
        }

        static int getHowManyToFill(Difficulty difficulty)
        {
            switch (difficulty) {
                case Difficulty.EMPTY:
                    return 0;
                case Difficulty.HARD: 
                    return 2;
                case Difficulty.MEDIUM:
                    return 4;
                case Difficulty.EASY:
                    return 6;
                case Difficulty.END:
                    return 8;
            }

            return 8;
        }

        static char[,] randomBoard(int fill)
        {
            char[,] board = new char[3, 3];

            for(int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    board[x, y] = ' ';
                }
            }

            char currentTurn = 'X';
            for (int i = 0; i < fill; i++) {
                int x = rng.Next(3);
                int y = rng.Next(3);
                while (board[x,y] != ' ')
                {
                    x = rng.Next(3);
                    y = rng.Next(3);
                }
                board[x, y] = currentTurn;
                currentTurn = currentTurn == 'X' ? 'O' : 'X';
            }
            return board;
        }

        public static long testAlgorithm(Func<char[,], int> algorithm, Difficulty difficulty)
        {
            char[,] board = randomBoard(getHowManyToFill(difficulty));
            Stopwatch stopwatch = Stopwatch.StartNew();
            algorithm.Invoke(board);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static int randomAlgorithm(char[,] board)
        {
            int x = rng.Next(3);
            int y = rng.Next(3);
            while (board[x, y] != ' ')
            {
                x = rng.Next(3);
                y = rng.Next(3);
            }
            return x + y * 3;
        }

    }
}
