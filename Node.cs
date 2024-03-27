using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Node(Node? parent, char[] board, int action, char lastPlayed)
    {
        public Node? parent = parent;
        public char[] board = (char[])board.Clone();
        public int action = action;
        public char lastPlayed = lastPlayed;
    }
}
