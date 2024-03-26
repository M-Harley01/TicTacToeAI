using Raylib_cs;
using static TicTacToe.General;

namespace TicTacToe
{
    public class Tile(int x, int y, int width, int height, int index)
    {
        public int x = x;
        public int y = y;
        public int width = width;
        public int height = height;
        public int index = index;

        public void onClick()
        {
            if (mainBoard[index] != '\0' || gameOver)
                return;

            mainBoard[index] = currentPlayer;
            gameOver = checkGameOver(mainBoard);

            if(gameOver)
                winner = getWinner(mainBoard, true);

            nextTurn();
        }

        public void Draw(int size)
        {
            Raylib.DrawRectangle(x, y, width, height, winningPlaces.Contains(index) ? Settings.uiHighlighted : Settings.uiPrimary);
            string text = mainBoard[index].ToString();
            Raylib.DrawText(text, x + Raylib.MeasureText(text, (size * 3) / 4)/2, y + size/8,(size*3)/4, Settings.uiTextSecondary);
        }
    }
}
