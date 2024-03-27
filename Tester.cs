using System.Diagnostics;
using static TicTacToe.General;

namespace TicTacToe
{
    public static class Tester
    {
        public enum Difficulty
        {
            EMPTY,
            HARD,
            MEDIUM,
            EASY,
            END,
        }

        static int GetHowManyToFill(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.EMPTY:
                    return 0;
                case Difficulty.HARD:
                    return (2 * Settings.boardSizeSq) / 9;
                case Difficulty.MEDIUM:
                    return (4 * Settings.boardSizeSq) / 9;
                case Difficulty.EASY:
                    return (6 * Settings.boardSizeSq) / 9;
                case Difficulty.END:
                    return (8 * Settings.boardSizeSq) / 9;
                default:
                    break;
            }

            return 8;
        }

        //Fills a board with random moves.
        static char[] RandomBoard(int fill)
        {
            char[] board = new char[Settings.boardSizeSq];

            for(int x = 0; x < Settings.boardSizeSq; x++)
            {
                board[x] = '\0';
            }

            char currentTurn = 'X';
            for (int i = 0; i < fill; i++) {
                int x = rng.Next(Settings.boardSizeSq);
                while (board[x] != '\0')
                {
                    x = rng.Next(Settings.boardSizeSq);
                }
                board[x] = currentTurn;
                currentTurn = OtherPlayer(currentTurn);
            }
            return board;
        }

        //Records how long it takens an algorithm to take an action. 
        public static long TestAlgorithm(Func<char[], char, int> algorithm, Difficulty difficulty)
        {
            char[] board = RandomBoard(GetHowManyToFill(difficulty));
            Stopwatch stopwatch = Stopwatch.StartNew();
            algorithm.Invoke(board, 'X');
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        //Compares the action taken by two algorithms
        public static bool CompareAlgorithm(Func<char[], char, int> algorithm1, Func<char[], char, int> algorithm2, Difficulty difficulty)
        {
            char[] board = RandomBoard(GetHowManyToFill(difficulty));
            return algorithm1.Invoke(board, 'X') == algorithm2.Invoke(board, 'X');
        }

    }
}
