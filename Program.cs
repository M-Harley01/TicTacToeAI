﻿using Raylib_cs;
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

            //long time = 0;
            //for(int i = 0; i < 1; i++)
            //{
            //    time += Tester.testAlgorithm(Algorithms.MiniMax, Tester.Difficulty.MEDIUM);
            //}
            //time /= 1;
            //Console.WriteLine($"Depth First Search mean: {time}ms");
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
                }

                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Vector2 mousePosition = Raylib.GetMousePosition();
                    for (int i = 0; i < 5; i++)
                    {
                        if (checkBounds(UserInterface.TILES_PANEL_END, UserInterface.TILES_START_Y + 45 * i + 45, 150, 40, mousePosition))
                        {
                            searchAlgorithm = algorithms[i];
                        }
                    }
                }
                if (!gameOver && currentPlayer == 'O')
                {
                    stopwatch.Restart();
                    mainBoard[searchAlgorithm.Invoke(mainBoard, 'O')] = currentPlayer;
                    timeTaken = stopwatch.ElapsedMilliseconds;
                    nextTurn();
                    gameOver = checkGameOver(mainBoard);
                }


                

                
            }

            Raylib.CloseWindow();
        }
    }
}
