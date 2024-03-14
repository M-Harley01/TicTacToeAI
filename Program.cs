using Raylib_cs;
using System.Numerics;
using static TicTacToe.Settings;
using static TicTacToe.General;

namespace TicTacToe
{
    internal class Program
    {
        static bool checkBounds(int x, int y, int width, int height, Vector2 mouse)
        {
            if (mouse.X < x || mouse.X > width + x || mouse.Y < y || mouse.Y > y + height)
            {
                return false;
            }
            return true;
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
                            if (checkBounds(screenWidth / 2 + (i * 110), screenHeight / 2 + (j * 110), 100, 100, mousePOS) && mainBoard[flatten(j, i)] != 'X' && mainBoard[flatten(j, i)] != 'O' && !gameOver)
                            {
                                Console.WriteLine($"Clicked at i:{i}, j:{j}");
                                mainBoard[flatten(j, i)] = currentPlayer;
                                nextTurn();
                                gameOver = checkGameOver(mainBoard);
                            }
                        }
                        Raylib.DrawRectangle(screenWidth / 2 + (i * 110), screenHeight / 2 + (j * 110), 100, 100, Color.White);
                        Raylib.DrawText(mainBoard[flatten(j, i)].ToString(), screenWidth / 2 + (i * 110) + 25, screenHeight / 2 + (j * 110) + 15, 80, Color.Black);
                    }
                }

                if(!gameOver && currentPlayer == 'O')
                {
                    mainBoard[Algorithms.BreadthFirst(mainBoard)] = currentPlayer;
                    nextTurn();
                    gameOver = checkGameOver(mainBoard);
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
