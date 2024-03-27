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
        private const int TILES_START_X = SCREEN_WIDTH / 4;
        private const int TILES_START_Y = SCREEN_HEIGHT / 8;

        private const int TILES_END_X = (SCREEN_WIDTH / 4) * 3;
        private const int TILES_END_Y = (SCREEN_HEIGHT / 8) * 7;

        private const int TILE_GAP = 10;

        private static Tile[] tiles = new Tile[boardSizeSq];
        private static readonly DropDown dropDownMenu = new(TILES_END_X + SCREEN_WIDTH / 8 - 150, TILES_START_Y + 45, 300, 40, ["Breadth First", "Depth First", "I.D.D.F.S", "A*", "MiniMax search"]);
        
        private static readonly Button[] buttons = [
            new Button(TILES_END_X + SCREEN_WIDTH / 8 - 150, TILES_END_Y, 300, SCREEN_HEIGHT / 8, "Reset", () =>
            {
                mainBoard = new char[boardSizeSq];
                gameOver = false;
                currentPlayer = manualPlayer;
                winningPlaces.Clear();
                winner = '\0';
            }),
            new Button(TILES_END_X + SCREEN_WIDTH / 8 - 150, TILES_END_Y-150, 300, SCREEN_HEIGHT / 8, "Swap", () =>
            {
                aiPlayer = OtherPlayer(aiPlayer);
                manualPlayer = OtherPlayer(manualPlayer);
            }),
            new Button(50, TILES_END_Y, 300, SCREEN_HEIGHT / 8, "Back", () =>
            {
                currentScreen = new MenuScreen();
            })
        ];


        public GameScreen()
        {
            GenerateTiles();
            mainBoard = new char[boardSizeSq];
            gameOver = false;
            currentPlayer = manualPlayer;
        }

        private static int TileSize() => ((SCREEN_HEIGHT / 4) * 3 - TILE_GAP * boardSize) / boardSize;

        //Get the x position needed to centre the tiles.
        private static int CentreX()
        {
            int totalWidth = (TILE_GAP + TileSize()) * boardSize - TILE_GAP;
            int areaAvailable = TILES_END_X - TILES_START_X;
            return TILES_START_X + (areaAvailable - totalWidth) / 2;
        }

        public static void GenerateTiles()
        {
            //Generate the tiles for the user interface, based on the size of the grid (3x3,4x4,etc).
            tiles = new Tile[boardSizeSq];
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    tiles[Flatten(x, y)] = new Tile(CentreX() + (TILE_GAP + TileSize()) * y, TILES_START_Y + TILE_GAP / 2 + (TILE_GAP + TileSize()) * x, TileSize(), TileSize(), Flatten(x, y));
                }
            }
        }
        public void Draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(background);

            Raylib.DrawText("Select an algorithm", TILES_END_X + SCREEN_WIDTH / 8 - 150, TILES_START_Y + 15, 30, Color.White);
            Raylib.DrawText("Using: " + searchAlgorithm.Method.Name, 0, 0, 60, Color.White);
            Raylib.DrawText("Last Action Took: " + timeTakenForAction + "ms", 0, 60, 30, Color.White);
            Raylib.DrawText($"Manual Player: {manualPlayer}", 0, 90, 30, Color.White);
            Raylib.DrawText($"Ai Player: {aiPlayer}", 0, 120, 30, Color.White);

            for (int i = 0; i < boardSizeSq; i++)
            {
                tiles[i].Draw(TileSize());
            }

            dropDownMenu.Draw();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw();
            }

            if (gameOver)
            {
                Raylib.DrawText("Game over", 0, 150, 30, Color.White);
                string winnerText = "Draw";
                if (winner != '\0')
                {
                    winnerText = $"Winner: {winner}";
                }
                Raylib.DrawText(winnerText, (SCREEN_WIDTH - Raylib.MeasureText(winnerText, 100)) / 2, SCREEN_HEIGHT - 100, 100, uiPrimary);
            }
            Raylib.EndDrawing();
        }

        public void HandleMouse()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePosition = Raylib.GetMousePosition();

                foreach (Tile tile in tiles)
                {
                    if (MouseInRect(tile.x, tile.y, tile.width, tile.height, mousePosition))
                    {
                        tile.OnClick();
                    }
                }

                for (int i = 0; i < buttons.Length; i++)
                {
                    if (MouseInRect(buttons[i].x, buttons[i].y, buttons[i].width, buttons[i].height, mousePosition))
                    {
                        buttons[i].OnClick();
                    }
                }

                if (MouseInRect(dropDownMenu.x, dropDownMenu.y, dropDownMenu.width, dropDownMenu.GetHeight(), mousePosition))
                {
                    dropDownMenu.OnClick(mousePosition);
                }
            }
        }
    }
}
