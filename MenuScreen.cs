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
        
        private static Button startButton = new Button(SCREEN_WIDTH / 2 - 150, SCREEN_HEIGHT / 2 - 50, 300, 100, "Start", () =>
        {
            currentScreen = new GameScreen();
        });

        private static Button settingsButton = new Button(SCREEN_WIDTH / 2 - 150, SCREEN_HEIGHT / 2 + 100, 300, 100, "Settings", () =>
        {
            currentScreen = new SettingsScreen();
        });

        public void draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(background);
            startButton.draw();
            settingsButton.draw();
            Raylib.EndDrawing();
        }

        public void handleMouse()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePosition = Raylib.GetMousePosition();
                if (checkBounds(startButton.x, startButton.y, startButton.width, startButton.height, mousePosition))
                {
                    startButton.onClick();
                }
                else if(checkBounds(settingsButton.x, settingsButton.y, settingsButton.width, settingsButton.height, mousePosition))
                {
                    settingsButton.onClick();
                }
            }
        }
    }
}
