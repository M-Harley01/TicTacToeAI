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
            //Func<char[], char, int> searchAlgorithm = Algorithms.MiniMax;

            UserInterface.init();
            while (!Raylib.WindowShouldClose())
            {
                UserInterface.draw();
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Vector2 mousePosition = Raylib.GetMousePosition();
                    foreach(Tile tile in UserInterface.tiles) { 
                        if (checkBounds(tile.x, tile.y, tile.width, tile.height, mousePosition))
                        {
                            tile.onClick();
                        }
                    }
                    if (checkBounds(UserInterface.dropDownMenu.x, UserInterface.dropDownMenu.y, UserInterface.dropDownMenu.width, UserInterface.dropDownMenu.getHeight(), mousePosition))
                    {
                        UserInterface.dropDownMenu.onClick(mousePosition);
                    }
                }

                if(Raylib.IsKeyPressed(KeyboardKey.R))
                {
                    mainBoard = new char[Settings.boardSizeSq];
                    gameOver = false;
                }

                if (!gameOver && currentPlayer == 'O')
                {
                    stopwatch.Restart();
                    mainBoard[searchAlgorithm.Invoke(mainBoard, 'O')] = currentPlayer;
                    timeTaken = stopwatch.ElapsedMilliseconds;
                    gameOver = checkGameOver(mainBoard);
                    nextTurn();
                }





            }

            Raylib.CloseWindow();
        }
    }
}
