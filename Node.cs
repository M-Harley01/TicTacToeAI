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

        public Node(Node parent, char[] board, int action) 
        {
            this.parent = parent;
            this.board = (char[])board.Clone();
            this.action = action;
        }

        
    }
}
