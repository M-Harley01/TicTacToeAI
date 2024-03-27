using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Settings;
using static TicTacToe.General;
using System.Numerics;

namespace TicTacToe
{
    public class SettingsScreen : IScreen
    {
        private static readonly Button[] buttons = [
            new Button(50, (SCREEN_HEIGHT / 8) * 7 , 300, SCREEN_HEIGHT / 8, "Back", () =>
            {
                currentScreen = new MenuScreen();
            }),
            new Button(SCREEN_WIDTH - 150, 150, 100, 100, "+", () =>
            {
                UpdateBoardSize(boardSize + 1);
            }),
            new Button(SCREEN_WIDTH - 450, 150, 100, 100, "-", () =>
            {
                UpdateBoardSize(boardSize - 1);
            })
        ];

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(background);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw();
            }
            Raylib.DrawText($"Board Size: ", 50, 160, 80, uiTextPrimary);
            Raylib.DrawText($"{boardSize}x{boardSize}", SCREEN_WIDTH - 450 + 130, 160, 80, uiTextPrimary);
            Raylib.EndDrawing();
        }

        public void HandleMouse()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePosition = Raylib.GetMousePosition();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (MouseInRect(buttons[i].x, buttons[i].y, buttons[i].width, buttons[i].height, mousePosition))
                    {
                        buttons[i].OnClick();
                    }
                }
            }
        }
    }
}
