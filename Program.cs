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
            int action = searchAlgorithm.Invoke(mainBoard, aiPlayer, true);
            if(action == -1) {
                action = searchAlgorithm.Invoke(mainBoard, aiPlayer, false);
            }
            if (action == -1) {
                action = Algorithms.randomAction(mainBoard);
            }
            mainBoard[action] = currentPlayer;
            timeTakenForAction = stopwatch.ElapsedMilliseconds;
            gameOver = checkGameOver(mainBoard);
            if(gameOver)
            {
                winner = getWinner(mainBoard, true);
            }
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
