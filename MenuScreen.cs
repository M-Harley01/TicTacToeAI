using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.General;
using static TicTacToe.Settings;

namespace TicTacToe
{
    internal class MenuScreen : IScreen
    {
        private static Button[] buttons = [
            new Button(SCREEN_WIDTH / 2 - 150, SCREEN_HEIGHT / 2 - 50, 300, 100, "Start", () =>
            {
                currentScreen = new GameScreen();
            }),
            new Button(SCREEN_WIDTH / 2 - 150, SCREEN_HEIGHT / 2 + 100, 300, 100, "Settings", () =>
            {
                currentScreen = new SettingsScreen();
            }),
            new Button(SCREEN_WIDTH / 2 - 150, SCREEN_HEIGHT / 2 + 250, 300, 100, "Quit", () =>
            {
                quit = true;
            })
        ];


        public void draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(background);
            Raylib.DrawText("Tic Tac Toe AI", (SCREEN_WIDTH - Raylib.MeasureText("Tic Tac Toe AI", 100))/2, SCREEN_HEIGHT / 8, 100, uiTextPrimary);
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].draw();
            }
            Raylib.EndDrawing();
        }

        public void handleMouse()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePosition = Raylib.GetMousePosition();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (checkBounds(buttons[i].x, buttons[i].y, buttons[i].width, buttons[i].height, mousePosition))
                    {
                        buttons[i].onClick();
                    }
                }
            }
        }
    }
}
