using Raylib_cs;
using System;
using System.Numerics;

namespace TicTacToe
{
    internal class Program
    {
        static char currentPlayer = 'X';
        static char[,] board = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        static bool gameOver = false;

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
            if (currentPlayer == 'X')
            {
                currentPlayer = 'O';
            }
            else
            {
                currentPlayer = 'X';
            }
        }

        static void resetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        static void checkWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                {
                    Console.WriteLine($"Player {board[i, 0]} wins!");
                    gameOver = true;
                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[0, i] == board[2, i])
                {
                    Console.WriteLine($"Player {board[0, i]} wins!");
                    gameOver = true;
                    return;
                }
            }

            if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2])
            {
                Console.WriteLine($"Player {board[0, 0]} wins!");
                gameOver = true;
                return;
            }

            if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0])
            {
                Console.WriteLine($"Player {board[0, 2]} wins!");
                gameOver = true;
                return;
            }

            bool isTie = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        isTie = false;
                        break;
                    }
                }
            }

            if (isTie)
            {
                Console.WriteLine("It's a tie!");
                gameOver = true;
                return;
            }
        }

        static void BreadthFirst()
        {
            Queue<char[,]> frontier = new Queue<char[,]>();
            HashSet<char[,]> visited = new HashSet<char[,]>();

            frontier.Enqueue(board.Clone() as char[,]);

            while (frontier.Count > 0)
            {
                char[,] currentNode = frontier.Dequeue();
                visited.Add(currentNode);

                foreach (Tuple<int, int> action in findEmpty(currentNode))
                {
                    char[,] childBoard = currentNode.Clone() as char[,];
                    childBoard[action.Item1, action.Item2] = currentPlayer;

                    if (!visited.Contains(childBoard) && !frontier.Contains(childBoard))
                    {
                        if (IsGameOver())
                        {
                            return;
                        }
                        else
                        {
                            frontier.Enqueue(childBoard);
                        }
                    }
                }
            }
        }

        static bool IsGameOver()
        {
            return false;
        }

        static List<Tuple<int,int>> findEmpty(char[,] board)
        {
            List<Tuple<int, int>> tupleArray = new List<Tuple<int, int>>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[j, i] == ' ')
                    {
                        tupleArray.Add(new Tuple<int, int>(j,i));
                    }
                }
            }

            return tupleArray;
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
                            if (checkBounds(screenWidth / 2 + (i * 110), screenHeight / 2 + (j * 110), 100, 100, mousePOS) && board[j, i] != 'X' && board[j, i] != 'O' && !gameOver)
                            {
                                Console.WriteLine($"Clicked at i:{i}, j:{j}");
                                board[j, i] = currentPlayer;
                                nextTurn();
                                checkWinner();
                            }
                        }
                        Raylib.DrawRectangle(screenWidth / 2 + (i * 110), screenHeight / 2 + (j * 110), 100, 100, Color.White);
                        Raylib.DrawText(board[j, i].ToString(), screenWidth / 2 + (i * 110) + 25, screenHeight / 2 + (j * 110) + 15, 80, Color.Black);
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
