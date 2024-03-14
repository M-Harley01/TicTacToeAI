using Raylib_cs;
using System.Numerics;
using static TicTacToe.Settings;

namespace TicTacToe
{
    internal class Program
    {
        static char currentPlayer = 'X';
        static char[] board = {  '\0', '\0', '\0',  '\0', '\0', '\0' ,  '\0', '\0', '\0' };
        static bool gameOver = false;

        static int flatten(int x, int y)
        {
            return x + y * boardSizeLength;
        }

        static Tuple<int, int> deFlatten(int f)
        {
            return new Tuple<int, int>(f%boardSizeLength, f/boardSizeLength);
        }

        static bool checkBounds(int x, int y, int width, int height, Vector2 mouse)
        {
            if (mouse.X < x || mouse.X > width + x || mouse.Y < y || mouse.Y > y + height)
            {
                return false;
            }
            return true;
        }

        static void nextTurn()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        static void resetBoard()
        {
            for (int i = 0; i < boardSizeLength*boardSizeLength; i++)
            {
                board[i] = '\0';
            }
        }

        static bool checkGameOver()
        {
            if (getWinner() != '\0')
                return true;
            for (int i = 0;i < boardSizeLength * boardSizeLength;i++)
            {
                if (board[i] == '\0')
                    return false;
            }
            return true;
        }

        //TODO: OPTIMISE THIS PLEASE
        static char getWinner()
        {
            char checking = 'X';
            for(int i = 0; i < 2; i++)
            {

                int foundDiagonal1 = 0;
                int foundDiagonal2 = 0;

                for (int x = 0; x < boardSizeLength; x++)
                {
                    int foundDown = 0;
                    int foundAcross = 0;
                    for(int y = 0; y < boardSizeLength; y++)
                    {
                        if (board[flatten(x, y)] == checking)
                            foundDown++;
                        if (board[flatten(y, x)] == checking)
                            foundAcross++;
                    }
                    if (foundDown == boardSizeLength)
                    {
                        return checking;
                    }
                    if (foundAcross == boardSizeLength)
                    {
                        return checking;
                    }

                    if (board[flatten(x, x)] == checking)
                        foundDiagonal1++;
                    if (board[flatten((boardSizeLength-1) - x, x)] == checking)
                        foundDiagonal2++;
                }
                if (foundDiagonal1 == boardSizeLength)
                {
                    return checking;
                }
                if (foundDiagonal2 == boardSizeLength)
                {
                    return checking;
                }

                checking = 'O';
            }
            return '\0';
        }

        //static void BreadthFirst()
        //{
        //    Queue<char[,]> frontier = new Queue<char[,]>();
        //    HashSet<char[,]> visited = new HashSet<char[,]>();
        //
        //    frontier.Enqueue(board.Clone() as char[,]);
        //
        //    while (frontier.Count > 0)
        //    {
        //        char[,] currentNode = frontier.Dequeue();
        //        visited.Add(currentNode);
        //
        //        foreach (Tuple<int, int> action in findEmpty(currentNode))
        //        {
        //            char[,] childBoard = currentNode.Clone() as char[,];
        //            childBoard[action.Item1, action.Item2] = currentPlayer;
        //
        //            if (!visited.Contains(childBoard) && !frontier.Contains(childBoard))
        //            {
        //                if (IsGameOver())
        //                {
        //                    return;
        //                }
        //                else
        //                {
        //                    frontier.Enqueue(childBoard);
        //                }
        //            }
        //        }
        //    }
        //}


        static List<int> findActions(char[] board)
        {
            List<int> actions = new List<int> ();
            for(int i = 0; i < boardSizeLength*boardSizeLength; i++)
            {
                if (board[boardSizeLength] == '\0')
                    actions.Add(i);
            }
            return actions;
        }


        static void Main(string[] args)
        {
            const int screenWidth = 1600;
            const int screenHeight = 900;

            Raylib.InitWindow(screenWidth, screenHeight, "Tic Tac Toe");

            Raylib.SetTargetFPS(60);
            Color background = new Color(0, 128, 128, 255);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(background);

                Vector2 mousePOS = Raylib.GetMousePosition();

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Raylib.IsMouseButtonPressed(MouseButton.Left);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            if (checkBounds(screenWidth / 2 + (i * 110), screenHeight / 2 + (j * 110), 100, 100, mousePOS) && board[flatten(j, i)] != 'X' && board[flatten(j, i)] != 'O' && !gameOver)
                            {
                                Console.WriteLine($"Clicked at i:{i}, j:{j}");
                                board[flatten(j, i)] = currentPlayer;
                                nextTurn();
                                gameOver = checkGameOver();
                            }
                        }
                        Raylib.DrawRectangle(screenWidth / 2 + (i * 110), screenHeight / 2 + (j * 110), 100, 100, Color.White);
                        Raylib.DrawText(board[flatten(j, i)].ToString(), screenWidth / 2 + (i * 110) + 25, screenHeight / 2 + (j * 110) + 15, 80, Color.Black);
                    }
                }

                if (gameOver)
                {
                    Raylib.DrawText("Game over", 10, 10, 30, Color.White);
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
