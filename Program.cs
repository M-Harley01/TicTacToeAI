using Raylib_cs;
using System.Numerics;
using static TicTacToe.Settings;
using static TicTacToe.General;

namespace TicTacToe
{
    internal class Program
    {
        static void AiTurn()
        {
            //Set stopwatch to 0 and start it.
            stopwatch.Restart();

            //Try to find an action that leads to a win.
            int action = searchAlgorithm.Invoke(mainBoard, aiPlayer, true);

            //if it can't find that, try to find an action that leads to a draw. 
            if(action == -1) {
                action = searchAlgorithm.Invoke(mainBoard, aiPlayer, false);
            }

            //if it can't that, take a random action.
            if (action == -1) {
                action = Algorithms.RandomAction(mainBoard);
            }

            mainBoard[action] = aiPlayer;

            //Record time taken for the algorithm to find the action.
            timeTakenForAction = stopwatch.ElapsedMilliseconds;

            //Check if the game is over.
            gameOver = CheckGameOver(mainBoard);
            if(gameOver)
            {
                //Check who won.
                winner = GetWinner(mainBoard, true);
            }
            NextTurn();
        }

        static void Main()
        {
            Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Tic Tac Toe");
            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose() && !quit)
            {
                currentScreen.Draw();
                currentScreen.HandleMouse();

                if (!gameOver && currentPlayer == aiPlayer)
                    AiTurn();
            } 

            Raylib.CloseWindow();
        }
    }
}
