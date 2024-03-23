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
