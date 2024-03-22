using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Settings;

namespace TicTacToe
{
    public class DropDown
    {
        public int x;
        public int y;
        public int width;
        public int height;
        int totalHeight;
        string[] options;
        int selected;
        bool open;

        public DropDown(int x, int y, int width, int height, string[] options)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.options = options;
            this.selected = 0;
            this.totalHeight = height * (options.Length + 1);
            this.open = false;
        }

        public void draw()
        {
            Raylib.DrawRectangle(x,y,width, open? totalHeight : height, uiSecondary);
            Raylib.DrawRectangle(x + 5, y + 5, width - 10, height - 10, uiPrimary);
            Raylib.DrawText(options[selected], x + 10, y + 5, 20, uiTextSecondary);
            Raylib.DrawRectangleLinesEx(new Rectangle(x + width - 40, y, 40, height), 2, uiSecondary);
            Raylib.DrawRectangle(x + width - 30, y + (height - 20) / 2, 20, 20, uiSecondary);

            for (int i = 0; i < options.Length && open; i++)
            {
                Raylib.DrawRectangle(x+5, y + ((height)*(i+1)), width - 10, height - 10, uiPrimary);
                Raylib.DrawText(options[i], x +10, y + ((height) * (i + 1)), 20, uiTextSecondary);
            }
        }

        public int getHeight()
        {
            return open? totalHeight : height;
        }

        public void onClick(Vector2 mousePos)
        {
            int x = (int)mousePos.X;
            int y = (int)mousePos.Y;

            for(int i = 0; i <= options.Length;i++)
            {
                if (y < (this.y+height*(i+1)) && y >= (this.y + height * i))
                {
                    if(i==0)
                        open = !open;
                    else if(open)
                    {
                        open = false;
                        selected = i - 1;
                        General.searchAlgorithm = General.algorithms[selected];
                    }
                }
            }
        }

    }
}
