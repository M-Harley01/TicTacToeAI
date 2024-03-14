using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Node
    {
        public Node parent;
        public char[] board;
        public int action;
        public char lastPlayed;

        public Node(Node parent, char[] board, int action, char lastPlayed) 
        {
            this.parent = parent;
            this.board = (char[])board.Clone();
            this.action = action;
            this.lastPlayed = lastPlayed;
        }

        
    }
}
