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
    public class GameScreen : IScreen
    {
        private const int TILES_PANEL_START = SCREEN_WIDTH / 4;
        private const int TILES_PANEL_END = (SCREEN_WIDTH / 4) * 3;
        private const int TILES_START_Y = SCREEN_HEIGHT / 8;
        private const int TILES_END_Y = (SCREEN_HEIGHT / 8) * 7;
        private const int TILE_GAP = 10;
        private static Tile[] tiles = new Tile[boardSizeSq];
        private static DropDown dropDownMenu = new DropDown(TILES_PANEL_END + SCREEN_WIDTH / 8 - 150, TILES_START_Y + 45, 300, 40, ["Breadth First", "Depth First", "I.D.D.F.S", "A*", "MiniMax search"]);
        private static Button resetButton = new Button(TILES_PANEL_END + SCREEN_WIDTH / 8 - 150, TILES_END_Y, 300, SCREEN_HEIGHT / 8, "Reset", () =>
        {
            mainBoard = new char[boardSizeSq];
            gameOver = false;
            currentPlayer = manualPlayer;
        });

        private static Button swapButton = new Button(TILES_PANEL_END + SCREEN_WIDTH / 8 - 150, TILES_END_Y-150, 300, SCREEN_HEIGHT / 8, "Swap", () =>
        {
            aiPlayer = otherPlayer(aiPlayer);
            manualPlayer = otherPlayer(manualPlayer);
        });

        private static Button backButton = new Button(50, TILES_END_Y, 300, SCREEN_HEIGHT / 8, "Back", () =>
        {
            currentScreen = new MenuScreen();
        });

        public GameScreen()
        {
            generateTiles();
            mainBoard = new char[boardSizeSq];
            gameOver = false;
            currentPlayer = manualPlayer;
        }

        private static int tileSize()
        {
            return ((SCREEN_HEIGHT / 4) * 3 - TILE_GAP * boardSizeLength) / boardSizeLength;
        }

        private static int centreX()
        {
            int totalX = (TILE_GAP + tileSize()) * boardSizeLength - TILE_GAP;
            int area = TILES_PANEL_END - TILES_PANEL_START;
            return TILES_PANEL_START + (area - totalX) / 2;
        }

        public static void generateTiles()
        {
            tiles = new Tile[boardSizeSq];
            for (int x = 0; x < boardSizeLength; x++)
            {
                for (int y = 0; y < boardSizeLength; y++)
                {
                    tiles[flatten(x, y)] = new Tile(centreX() + (TILE_GAP + tileSize()) * y, TILES_START_Y + TILE_GAP / 2 + (TILE_GAP + tileSize()) * x, tileSize(), tileSize(), flatten(x, y));
                }
            }
        }
        public void draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(background);

            dropDownMenu.draw();
            Raylib.DrawText("Select an algorithm", TILES_PANEL_END + SCREEN_WIDTH / 8 - 150, TILES_START_Y + 15, 30, Color.White);

            Raylib.DrawText("Using: " + searchAlgorithm.Method.Name, 0, 0, 60, Color.White);

            //Raylib.DrawRectangle(0, 0, TILES_PANEL_START, SCREEN_HEIGHT, Color.Red);
            //Raylib.DrawRectangle(TILES_PANEL_END, 0, SCREEN_WIDTH - TILES_PANEL_END, SCREEN_HEIGHT, Color.Green);
            //Raylib.DrawRectangle(0, 0, SCREEN_WIDTH, TILES_START_Y, Color.Blue);
            //Raylib.DrawRectangle(0, TILES_END_Y, SCREEN_WIDTH, SCREEN_HEIGHT - TILES_END_Y, Color.Yellow);

            for (int i = 0; i < boardSizeSq; i++)
            {
                tiles[i].Draw(tileSize());
            }

            Raylib.DrawText("Last Move Took: " + timeTaken + "ms", 0, 60, 30, Color.White);
            Raylib.DrawText($"Manual Player: {manualPlayer}", 0, 90, 30, Color.White);
            Raylib.DrawText($"Ai Player: {aiPlayer}", 0, 120, 30, Color.White);

            if (gameOver)
            {
                Raylib.DrawText("Game over", 0, 150, 30, Color.White);
            }
            resetButton.draw();
            swapButton.draw();
            backButton.draw();
            Raylib.EndDrawing();
        }

        public void handleMouse()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePosition = Raylib.GetMousePosition();
                foreach (Tile tile in tiles)
                {
                    if (checkBounds(tile.x, tile.y, tile.width, tile.height, mousePosition))
                    {
                        tile.onClick();
                    }
                }
                if (checkBounds(dropDownMenu.x, dropDownMenu.y, dropDownMenu.width, dropDownMenu.getHeight(), mousePosition))
                {
                    dropDownMenu.onClick(mousePosition);
                }
                else if (checkBounds(resetButton.x, resetButton.y, resetButton.width, resetButton.height, mousePosition))
                {
                    resetButton.onClick();
                }
                else if (checkBounds(swapButton.x, swapButton.y, swapButton.width, swapButton.height, mousePosition))
                {
                    swapButton.onClick();
                }
                else if (checkBounds(backButton.x, backButton.y, backButton.width, backButton.height, mousePosition))
                {
                    backButton.onClick();
                }
            }
        }
    }
}
