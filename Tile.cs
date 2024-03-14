using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static TicTacToe.General;

namespace TicTacToe
{
    public class Tile
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public int index;

        public Tile(int x, int y, int width, int height, int index)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.index = index;
        }

        public void onClick()
        {
            if (mainBoard[index] != '\0' || gameOver)
                return;
            mainBoard[index] = currentPlayer;
            nextTurn();
            gameOver = checkGameOver(mainBoard);
        }

        public void Draw()
        {
            Raylib.DrawRectangle(x, y, width, height, Color.White);
            Raylib.DrawText(mainBoard[index].ToString(), x,y,80,Color.Black);
        }
    }
}
