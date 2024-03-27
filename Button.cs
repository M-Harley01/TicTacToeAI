using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Button(int x, int y, int width, int height, string text, Action action)
    {
        public int x = x;
        public int y = y;
        public int width = width;
        public int height = height;
        readonly string text = text;
        readonly Action action = action;

        public void OnClick() => action.Invoke();

        public void Draw()
        {
            //Draw the button
            Raylib.DrawRectangle(x, y, width, height, Settings.uiPrimary);
            Raylib.DrawRectangleLinesEx(new Rectangle(x+5, y+5, width-10, height-10), 10, Settings.uiSecondary);

            //Calculate the position of the text on the button
            int fontSize = (width > height)? height / 2 : width / 2;
            int textSize = Raylib.MeasureText(text, fontSize);

            //Draw the text on the button
            Raylib.DrawText(text, x + (width - 10 - textSize) / 2, y + (height - 10 - fontSize) / 2, fontSize, Settings.uiTextSecondary);

        }
    }
}
