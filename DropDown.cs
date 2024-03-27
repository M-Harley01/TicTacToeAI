using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Settings;
using static TicTacToe.General;

namespace TicTacToe
{
    public class DropDown(int x, int y, int width, int height, string[] options)
    {
        public int x = x;
        public int y = y;
        public int width = width;
        public int height = height;
        readonly int totalHeight = height * (options.Length + 1);
        readonly string[] options = options;
        int selected = 0;
        bool open = false;

        public void Draw()
        {
            //Draw the selected option
            Raylib.DrawRectangle(x,y,width, open? totalHeight : height, uiSecondary);
            Raylib.DrawRectangle(x + 5, y + 5, width - 10, height - 10, uiPrimary);
            Raylib.DrawText(options[selected], x + 10, y + 5, 20, uiTextSecondary);
            Raylib.DrawRectangleLinesEx(new Rectangle(x + width - 40, y, 40, height), 2, uiSecondary);
            Raylib.DrawRectangle(x + width - 30, y + (height - 20) / 2, 20, 20, uiSecondary);

            //if the dropdown box is open, draw the rest of the options
            for (int i = 0; i < options.Length && open; i++)
            {
                Raylib.DrawRectangle(x+5, y + ((height)*(i+1)), width - 10, height - 10, uiPrimary);
                Raylib.DrawText(options[i], x +10, y + ((height) * (i + 1)), 20, uiTextSecondary);
            }
        }

        public int GetHeight() => open ? totalHeight : height;

        public void OnClick(Vector2 mousePos)
        {
            int y = (int)mousePos.Y;

            for(int i = 0; i <= options.Length;i++)
            {
                if (y < (this.y+height*(i+1)) && y >= (this.y + height * i))
                {
                    //If the selected option is clicked, close the dropdown menu.
                    if(i==0)
                        open = !open;
                    //else select the clicked on option from the menu.
                    else if(open)
                    {
                        open = false;
                        selected = i - 1;
                        searchAlgorithm = algorithms[selected];
                    }
                }
            }
        }

    }
}
