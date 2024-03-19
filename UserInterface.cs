using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Settings;
using static TicTacToe.General;

namespace TicTacToe
{
    public static class UserInterface
    {
        private static Color background = new Color(0, 128, 128, 255);
        public static Tile[] tiles = new Tile[boardSizeSq];
        private const int TILES_PANEL_START = SCREEN_WIDTH / 8;
        private const int TILES_PANEL_END = (SCREEN_WIDTH / 8) * 7;
        private const int TILES_START_Y = SCREEN_HEIGHT / 8;
        private const int TILES_END_Y = (SCREEN_HEIGHT / 8) * 7;
        private const int TILE_GAP = 10;

        private static int tileSize()
        {
            return ((SCREEN_HEIGHT / 4)*3 - TILE_GAP * boardSizeLength) / boardSizeLength;
        }

        private static int centreX()
        {
            int totalX = (TILE_GAP + tileSize()) * boardSizeLength - TILE_GAP;
            int area = TILES_PANEL_END - TILES_PANEL_START;
            return TILES_PANEL_START + (area - totalX)/2;
        }

        public static void init()
        {
            Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Tic Tac Toe");
            Raylib.SetTargetFPS(60);


            for (int x = 0; x < boardSizeLength; x++)
            {
                for (int y = 0; y < boardSizeLength; y++)
                {
                    tiles[flatten(x, y)] = new Tile(centreX() + (TILE_GAP + tileSize())*y, TILES_START_Y + TILE_GAP/2 + (TILE_GAP + tileSize()) * x, tileSize(), tileSize(), flatten(x, y));
                }
            }

        }

        public static void draw()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(background);

            //Raylib.DrawRectangle(0, 0, TILES_PANEL_START, SCREEN_HEIGHT, Color.Red);
            //Raylib.DrawRectangle(TILES_PANEL_END, 0, SCREEN_WIDTH - TILES_PANEL_END, SCREEN_HEIGHT, Color.Green);
            //Raylib.DrawRectangle(0, 0, SCREEN_WIDTH, TILES_START_Y, Color.Blue);
            //Raylib.DrawRectangle(0, TILES_END_Y, SCREEN_WIDTH, SCREEN_HEIGHT - TILES_END_Y, Color.Yellow);

            Raylib.DrawText("Using: " + searchAlgorithm.Method.Name, 0, 0, 60, Color.White);

            for (int i = 0; i < boardSizeSq; i++)
            {
                tiles[i].Draw(tileSize());
            }

            Raylib.DrawText("Last Move Took: " + timeTaken + "ms", 0, 60, 30, Color.White);

            if (gameOver)
            {
                Raylib.DrawText("Game over", 0, 90, 30, Color.White);
            }

            Raylib.EndDrawing();
        }
    }
}
