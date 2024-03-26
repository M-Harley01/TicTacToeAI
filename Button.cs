using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Button
    {
        public int x;
        public int y;
        public int width;
        public int height;
        string text;
        Action action;

        public Button(int x, int y, int width, int height, string text, Action action) { 
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.text = text;
            this.action = action;
        }

        public void onClick() => action.Invoke();

        public void draw()
        {
            Raylib.DrawRectangle(x, y, width, height, Settings.uiPrimary);
            Raylib.DrawRectangleLinesEx(new Rectangle(x+5, y+5, width-10, height-10), 10, Settings.uiSecondary);

            int fontSize = (width > height)? height / 2 : width / 2;
            int textSize = Raylib.MeasureText(text, fontSize);

            Raylib.DrawText(text, x + (width - 10 - textSize) / 2, y + (height - 10 - fontSize) / 2, fontSize, Settings.uiTextSecondary);

        }
    }
}
