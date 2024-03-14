using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.General;

namespace TicTacToe
{
    public static class Algorithms
    {
        public static Random rng = new Random();

        public static int BreadthFirst(char[] board)
        {
            char player = 'O';
            Queue<Node> frontier = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();
        
            frontier.Enqueue(new Node(null, board, -1));
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Dequeue();
                visited.Add(currentNode);
        
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    player = player == 'X' ? 'O' : 'X';
                    Node childNode = new Node(currentNode, childBoard, action);
        
                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        if (getWinner(childBoard) == 'O')
                        {
                            return getFirstMove(childNode);
                        }
                        else
                        {
                            frontier.Enqueue(childNode);
                        }
                    }
                }
            }
            Console.WriteLine("Random Move!");
            return randomMove(board);
        }

        private static int getFirstMove(Node childNode)
        {
            if (childNode.parent == null)
                return -1;
            while(childNode.parent.parent != null) {
                childNode = childNode.parent;
            }
            return childNode.action;
        }

        public static int randomMove(char[] board)
        {
            int pos = rng.Next(Settings.boardSizeSq);
            while (board[pos] != '\0')
            {
                pos = rng.Next(Settings.boardSizeSq);
            }
            return pos;
        }
    }
}
