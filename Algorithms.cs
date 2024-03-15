using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TicTacToe.General;

namespace TicTacToe
{
    public static class Algorithms
    {
        public static Random rng = new Random();
        private static Stopwatch sw = new Stopwatch();

        //Pros: Can win.
        //Cons: Only tries to win, doesn't try to not lose. Would rather attempt a risky win than draw.
        public static int BreadthFirst(char[] board, char player)
        {
            char startPlayer = player;
            Queue<Node> frontier = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();
        
            frontier.Enqueue(new Node(null, board, -1, (startPlayer == 'X')? 'O' : 'X' ));
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
                        if (getWinner(childBoard) == startPlayer)
                        {
                            //for (int x = 0; x < Settings.boardSizeLength; x++)
                            //{
                            //    for (int y = 0; y < Settings.boardSizeLength; y++)
                            //    {
                            //        Console.Write(childBoard[flatten(x, y)]);
                            //    }
                            //    Console.WriteLine();
                            //}
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
            return randomMove(board, player);
        }

        public static int BreadthFirst_HashQueue(char[] board, char player)
        {
            char startPlayer = player;
            HashQueue<Node> frontier = new HashQueue<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Enqueue(new Node(null, board, -1, (startPlayer == 'X') ? 'O' : 'X'));
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
                        if (getWinner(childBoard) == startPlayer)
                        {
                            //for (int x = 0; x < Settings.boardSizeLength; x++)
                            //{
                            //    for (int y = 0; y < Settings.boardSizeLength; y++)
                            //    {
                            //        Console.Write(childBoard[flatten(x, y)]);
                            //    }
                            //    Console.WriteLine();
                            //}
                            return getFirstMove(childNode);
                        }
                        else if (checkGameOver(childBoard))
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
            return randomMove(board, player);
        }

        //Same problem as with breadthFirst search, they look for just one winning move, can't find the best one as they don't rank all moves
        public static int DepthFirstSearch(char[] board, char player)
        {
            char startPlayer = player;
            Stack<Node> frontier = new Stack<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Push(new Node(null, board, -1, (startPlayer == 'X') ? 'O' : 'X'));
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Pop();
                visited.Add(currentNode);
                player = (currentNode.lastPlayed == 'X') ? 'O' : 'X';
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    Node childNode = new Node(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        if (getWinner(childBoard) == startPlayer)
                        {
                            Console.WriteLine("--------");
                            for (int x = 0; x < Settings.boardSizeLength; x++)
                            {
                                for (int y = 0; y < Settings.boardSizeLength; y++)
                                {
                                    Console.Write(childBoard[flatten(x, y)]);
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine("--------");
                            return getFirstMove(childNode);
                        }
                        else if (checkGameOver(childBoard))
                        {
                            continue;
                        }
                        else
                        {
                            frontier.Push(childNode);
                        }
                    }
                }

            }
            //If unable to win, it will play a random move.
            return randomMove(board, player);
        }
        public static int IterativeDeepeningDepthFirstSearch(char[] board, char player)
        {
            throw new NotImplementedException();
        }
        public static int GraphSearch(char[] board, char player)
        {
            throw new NotImplementedException();
        }
        public static int AStarSearch(char[] board, char player)
        {
            throw new NotImplementedException();
        }
        public static int BidirectionalSearch(char[] board, char player)
        {
            throw new NotImplementedException();
        }

        private static int getFirstMove(Node childNode)
        {
            if (childNode.parent == null)
                return -1;
            while(childNode.parent.parent != null) {
                childNode = childNode.parent;
                if (childNode.parent == null)
                    return -1;
            }
            return childNode.action;
        }

        private static int scoreMiniMax(char[] board, char currentPlayer, char maximizingPlayer)
        {
            int score = 0;
            if (checkGameOver(board))
            {
                if (getWinner(board) == maximizingPlayer)
                    score = 10;
                else if (getWinner(board) == otherPlayer(maximizingPlayer))
                    score = -10;
            }
            else
            {
                if(currentPlayer == maximizingPlayer)
                {
                    score = int.MinValue;
                    foreach(int action in findActions(board)) {
                        char[] boardCopy = (char[])board.Clone();
                        boardCopy[action] = currentPlayer;
                        int childScore = scoreMiniMax(boardCopy, otherPlayer(currentPlayer), maximizingPlayer);
                        if (childScore > score)
                            score = childScore;
                    }
                } else
                {
                    score = int.MaxValue;
                    foreach (int action in findActions(board))
                    {
                        char[] boardCopy = (char[])board.Clone();
                        boardCopy[action] = currentPlayer;
                        int childScore = scoreMiniMax(boardCopy, otherPlayer(currentPlayer), maximizingPlayer);
                        if (childScore < score)
                            score = childScore;
                    }
                }
            }
            return score;
        }

        //Explores all the nodes in a depth first search, takes a long time but does give optimal move.
        public static int MiniMax(char[] board, char player)
        {
            sw.Start();
            int maxScore = int.MinValue;
            int index = -1;
            List<int> actions = findActions(board);
            for(int i = 0; i < actions.Count; i++)
            {
                char[] boardCopy = (char[])board.Clone();
                boardCopy[actions[i]] = player;
                int score = scoreMiniMax(boardCopy, otherPlayer(player), player);
                if (score > maxScore)
                {
                    maxScore = score;
                    index = i;
                }
            }
            return actions[index];
        }

        public static int randomMove(char[] board, char player)
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
