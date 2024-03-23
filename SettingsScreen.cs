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

        private static Button swapButton = new Button(50, (SCREEN_HEIGHT / 8) * 7 , 300, SCREEN_HEIGHT / 8, "Back", () =>
        {
            currentScreen = new MenuScreen();
        });

        private static Button increaseBoardSizeButton = new Button(SCREEN_WIDTH - 150, 150, 100, 100, "+", () =>
        {
            updateBoardSize(boardSizeLength + 1);
        });

        private static Button decreaseBoardSizeButton = new Button(SCREEN_WIDTH - 450, 150, 100, 100, "-", () =>
        {  
            updateBoardSize(boardSizeLength - 1);
        });

        public void draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(background);
            swapButton.draw();
            increaseBoardSizeButton.draw();
            Raylib.DrawText($"Board Size: ", 50, 160, 80, uiTextPrimary);
            Raylib.DrawText($"{boardSizeLength}x{boardSizeLength}", SCREEN_WIDTH - 450 + 130, 160, 80, uiTextPrimary);
            decreaseBoardSizeButton.draw();
            Raylib.EndDrawing();
        }

        public void handleMouse()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePosition = Raylib.GetMousePosition();
                if (checkBounds(swapButton.x, swapButton.y, swapButton.width, swapButton.height, mousePosition))
                {
                    swapButton.onClick();
                }
                if (checkBounds(increaseBoardSizeButton.x, increaseBoardSizeButton.y, increaseBoardSizeButton.width, increaseBoardSizeButton.height, mousePosition))
                {
                    increaseBoardSizeButton.onClick();
                }
                if (checkBounds(decreaseBoardSizeButton.x, decreaseBoardSizeButton.y, decreaseBoardSizeButton.width, decreaseBoardSizeButton.height, mousePosition))
                {
                    decreaseBoardSizeButton.onClick();
                }
            }
        }
    }
}
