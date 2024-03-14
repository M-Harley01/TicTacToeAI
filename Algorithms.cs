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

        //Pros: Can win.
        //Cons: Only tries to win, doesn't try to not lose. Would rather attempt a risky win than draw.
        public static int BreadthFirst(char[] board)
        {
            char player = 'O';
            Queue<Node> frontier = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();
        
            frontier.Enqueue(new Node(null, board, -1, 'X'));
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Dequeue();
                visited.Add(currentNode);
                player = (currentNode.lastPlayed == 'X') ? 'O' : 'X';
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    Node childNode = new Node(currentNode, childBoard, action, player);
        
                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        if (getWinner(childBoard) == 'O')
                        {
                            for(int x = 0; x < Settings.boardSizeLength; x++)
                            {
                                for(int y = 0; y < Settings.boardSizeLength; y++)
                                {
                                    Console.Write(childBoard[flatten(x, y)]);
                                }
                                Console.WriteLine();
                            }
                            return getFirstMove(childNode);
                        }
                        else if(checkGameOver(childBoard))
                        {
                            continue;
                        }
                        else
                        {
                            frontier.Enqueue(childNode);
                        }
                    }
                }

            }
            //If unable to win, it will play a random move.
            Console.WriteLine("Random Move!");
            return randomMove(board);
        }

        private static int getFirstMove(Node childNode)
        {
            int count = 0;
            if (childNode.parent == null)
                return -1;
            while(childNode.parent.parent != null) {
                childNode = childNode.parent;
                count++;
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
