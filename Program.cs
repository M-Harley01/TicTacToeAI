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
            long time = 0;
            for(int i = 0; i < 500; i++)
            {
                time += Tester.testAlgorithm(Algorithms.BreadthFirst, Tester.Difficulty.EMPTY);
            }
            time /= 500;
            Console.WriteLine($"Breadth First Search mean: {time}ms");

            const int screenWidth = 1600;
            const int screenHeight = 900;

            Raylib.InitWindow(screenWidth, screenHeight, "Tic Tac Toe");

            Raylib.SetTargetFPS(60);
            Color background = new Color(0, 128, 128, 255);

            Tile[] tiles = new Tile[boardSizeSq];
            for(int x = 0; x < boardSizeLength; x++) 
            {
                for(int y = 0; y < boardSizeLength; y++)
                {
                    tiles[flatten(x, y)] = new Tile(screenWidth / 2 + (y * 110), screenHeight / 2 + (x * 110), 100, 100, flatten(x,y));
                }
            }

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(background);

                for(int i = 0; i < boardSizeSq; i++)
                {
                    tiles[i].Draw();
                }

                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Vector2 mousePosition = Raylib.GetMousePosition();
                    for (int i = 0; i < boardSizeSq; i++)
                    {
                        if(checkBounds(tiles[i].x, tiles[i].y, tiles[i].width, tiles[i].height, mousePosition))
                        {
                            tiles[i].onClick();
                        }
                    }
                }

                if (!gameOver && currentPlayer == 'O')
                {
                    mainBoard[Algorithms.BreadthFirst(mainBoard, 'O')] = currentPlayer;
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
