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
        public static List<int> winningPlaces = [];

        public static bool gameOver = false;
        public static bool quit = false;

        public static Stopwatch stopwatch = new();
        public static long timeTakenForAction = 0;

        public static Random rng = new();

        public static IScreen currentScreen = new MenuScreen();

        public static char OtherPlayer(char player) => player == 'X' ? 'O' : 'X';

        //Turns 2d coordinates into an index in the array
        public static int Flatten(int x, int y) => x + y * boardSize;

        public static void NextTurn() => currentPlayer = OtherPlayer(currentPlayer);

        public static void ResetBoard(char[] board)
        {
            for (int i = 0; i < boardSizeSq; i++)
            {
                board[i] = '\0';
            }
        }

        public static bool CheckGameOver(char[] board)
        {
            if(winner != '\0' || gameOver)
                return true;        

            if (GetWinner(board, false) != '\0')
                return true;

            for (int i = 0; i < boardSizeSq; i++)
            {
                if (board[i] == '\0')
                    return false;
            }

            return true;
        }

        //Find every location on the board that is empty
        public static List<int> FindActions(char[] board)
        {
            List<int> actions = [];
            for (int i = 0; i < boardSizeSq; i++)
            {
                if (board[i] == '\0')
                    actions.Add(i);
            }
            return actions;
        }

        public static void UpdateBoardSize(int newBoardSize)
        {
            if (newBoardSize < 3 || newBoardSize > 9)
                return;
            boardSize = newBoardSize;
            boardSizeSq = boardSize*boardSize;
            mainBoard = new char[boardSizeSq];
        }

        //Check if the mouse is inside the bounds of a rectangle.
        public static bool MouseInRect(int x, int y, int width, int height, Vector2 mouse) => !(mouse.X < x || mouse.X > width + x || mouse.Y < y || mouse.Y > y + height);

        public static char GetWinner(char[] board, bool highlightWinner)
        {
            char checking = 'X';
            for (int i = 0; i < 2; i++)
            {
                int inADiagonal1 = 0;
                int inADiagonal2 = 0;

                for (int x = 0; x < boardSize; x++)
                {
                    int inAColumn = 0;
                    int inARow = 0;
                    for (int y = 0; y < boardSize; y++)
                    {
                        if (board[Flatten(x, y)] == checking)
                            inAColumn++;
                        if (board[Flatten(y, x)] == checking)
                            inARow++;
                    }

                    //Check if has a full column filled
                    if (inAColumn == boardSize)
                    {
                        if (!highlightWinner)
                            return checking;

                        for (int k = 0; k < boardSize; k++)
                        {
                            winningPlaces.Add(Flatten(x, k));
                        }
                        return checking;
                    }
                    //Check if has a full row filled
                    if (inARow == boardSize)
                    {
                        if (!highlightWinner)
                            return checking;

                        for (int k = 0; k < boardSize; k++)
                        {
                            winningPlaces.Add(Flatten(k, x));
                        }
                        return checking;
                    }

                    if (board[Flatten(x, x)] == checking)
                        inADiagonal1++;
                    if (board[Flatten((boardSize - 1) - x, x)] == checking)
                        inADiagonal2++;
                }

                //Check if has a full diagonal filled
                if (inADiagonal1 == boardSize)
                {
                    if (!highlightWinner)
                        return checking;

                    for (int k = 0; k < boardSize; k++)
                    {
                        winningPlaces.Add(Flatten(k, k));
                    }
                    return checking;
                }

                //Check if has a full diagonal filled
                if (inADiagonal2 == boardSize)
                {
                    if (!highlightWinner)
                        return checking;

                    for (int k = 0; k < boardSize; k++)
                    {
                        winningPlaces.Add(Flatten((boardSize - 1) - k, k));
                    }
                    return checking;
                }

                checking = 'O';
            }
            return '\0';
        }

    }
}
