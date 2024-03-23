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

        public void draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(background);
            swapButton.draw();
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
            }
        }
    }
}
