using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class Settings
    {
        public static int boardSize = 3;
        public static int boardSizeSq = boardSize * boardSize;
        public const int SCREEN_WIDTH = 1600;
        public const int SCREEN_HEIGHT = 900;
        public static Color background = new(0, 128, 128, 255);
        public static Color uiPrimary = Color.White;
        public static Color uiSecondary = Color.DarkGray;
        public static Color uiTextSecondary = Color.Black;
        public static Color uiTextPrimary = Color.White;
        internal static Color uiHighlighted = Color.Green;
        public static char aiPlayer = 'X';
        public static char manualPlayer = 'O';
    }
}
