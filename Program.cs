using Raylib_cs;
using System.Numerics;
using static TicTacToe.Settings;
using static TicTacToe.General;

namespace TicTacToe
{
    internal class Program
    {
        static void aiTurn()
        {
            stopwatch.Restart();
            mainBoard[searchAlgorithm.Invoke(mainBoard, aiPlayer)] = currentPlayer;
            timeTaken = stopwatch.ElapsedMilliseconds;
            gameOver = checkGameOver(mainBoard);
            nextTurn();
        }

        static void Main(string[] args)
        {
            Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Tic Tac Toe");
            Raylib.SetTargetFPS(60);

            int count = 0;
            for(int i = 0; i < 1000; i++) { 
                if (Tester.compareAlgorithm(Algorithms.MiniMax, Algorithms.AStarSearch, Tester.Difficulty.MEDIUM))
                    count++;
            }
            double percent = (double)count / 10;
            Console.WriteLine($"Same: {percent}% of the time!");

            while (!Raylib.WindowShouldClose() && !quit)
            {
                currentScreen.draw();
                currentScreen.handleMouse();

                if (!gameOver && currentPlayer == aiPlayer)
                    aiTurn();
            } 

            Raylib.CloseWindow();
        }
    }
}
