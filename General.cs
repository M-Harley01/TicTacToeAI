using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Settings;

namespace TicTacToe
{
    public static class General
    {
        public static Func<char[], char, bool, int>[] algorithms = [Algorithms.BreadthFirst, Algorithms.DepthFirstSearch, Algorithms.IterativeDeepeningDepthFirstSearch, Algorithms.AStarSearch, Algorithms.MiniMax]; 
        public static Func<char[], char, bool, int> searchAlgorithm = algorithms[0];

        public static char currentPlayer = manualPlayer;
        public static char[] mainBoard = new char[boardSizeSq];

        public static char winner = '\0';
        public static List<int> winningPlaces = new List<int>();

        public static bool gameOver = false;
        public static bool quit = false;

        public static Stopwatch stopwatch = new Stopwatch();
        public static long timeTakenForAction = 0;

        public static Random rng = new Random();

        public static IScreen currentScreen = new MenuScreen();

        public static char otherPlayer(char player) => player == 'X' ? 'O' : 'X';

        public static int flatten(int x, int y) => x + y * boardSizeLength;

        public static void nextTurn() => currentPlayer = otherPlayer(currentPlayer);

        public static void resetBoard(char[] board)
        {
            for (int i = 0; i < boardSizeSq; i++)
            {
                board[i] = '\0';
            }
        }

        public static bool checkGameOver(char[] board)
        {
            if(winner != '\0' || gameOver)
                return true;        

            if (getWinner(board, false) != '\0')
                return true;

            for (int i = 0; i < boardSizeSq; i++)
            {
                if (board[i] == '\0')
                    return false;
            }

            return true;
        }

        public static List<int> findActions(char[] board)
        {
            List<int> actions = new List<int>();
            for (int i = 0; i < boardSizeSq; i++)
            {
                if (board[i] == '\0')
                    actions.Add(i);
            }
            return actions;
        }

        public static void updateBoardSize(int newBoardSizeLength)
        {
            if (newBoardSizeLength < 3 || newBoardSizeLength > 9)
                return;
            boardSizeLength = newBoardSizeLength;
            boardSizeSq = boardSizeLength*boardSizeLength;
            mainBoard = new char[boardSizeSq];
        }

        public static bool mouseInRect(int x, int y, int width, int height, Vector2 mouse) => !(mouse.X < x || mouse.X > width + x || mouse.Y < y || mouse.Y > y + height);

        //TODO: MAKE BETTER
        public static char getWinner(char[] board, bool highlightWinner)
        {
            char checking = 'X';
            for (int i = 0; i < 2; i++)
            {

                int foundDiagonal1 = 0;
                int foundDiagonal2 = 0;

                for (int x = 0; x < boardSizeLength; x++)
                {
                    int foundDown = 0;
                    int foundAcross = 0;
                    for (int y = 0; y < boardSizeLength; y++)
                    {
                        if (board[flatten(x, y)] == checking)
                            foundDown++;
                        if (board[flatten(y, x)] == checking)
                            foundAcross++;
                    }

                    if (foundDown == boardSizeLength)
                    {
                        if (!highlightWinner)
                            return checking;

                        for (int k = 0; k < boardSizeLength; k++)
                        {
                            winningPlaces.Add(flatten(x, k));
                        }
                        return checking;
                    }
                    if (foundAcross == boardSizeLength)
                    {
                        if (!highlightWinner)
                            return checking;

                        for (int k = 0; k < boardSizeLength; k++)
                        {
                            winningPlaces.Add(flatten(k, x));
                        }
                        return checking;
                    }

                    if (board[flatten(x, x)] == checking)
                        foundDiagonal1++;
                    if (board[flatten((boardSizeLength - 1) - x, x)] == checking)
                        foundDiagonal2++;
                }

                if (foundDiagonal1 == boardSizeLength)
                {
                    if (!highlightWinner)
                        return checking;

                    for (int k = 0; k < boardSizeLength; k++)
                    {
                        winningPlaces.Add(flatten(k, k));
                    }
                    return checking;
                }

                if (foundDiagonal2 == boardSizeLength)
                {
                    if (!highlightWinner)
                        return checking;

                    for (int k = 0; k < boardSizeLength; k++)
                    {
                        winningPlaces.Add(flatten((boardSizeLength - 1) - k, k));
                    }
                    return checking;
                }

                checking = 'O';
            }
            return '\0';
        }

    }
}
