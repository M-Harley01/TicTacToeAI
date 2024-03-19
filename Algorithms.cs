using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TicTacToe.General;
using static TicTacToe.Settings;

namespace TicTacToe
{
    public static class Algorithms
    {

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
            char startPlayer = player;
            Stack<Node> frontier = new Stack<Node>();
            Stack<Node> temporaryHolder = new Stack<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Push(new Node(null, board, -1, (startPlayer == 'X') ? 'O' : 'X'));
            int maxDepth = 2;

            while (frontier.Count > 0 || temporaryHolder.Count > 0)
            {
                Node currentNode = frontier.Pop();
                while(getDepth(currentNode) > maxDepth)
                {
                    temporaryHolder.Push(currentNode);

                    if(frontier.Count == 0) { 
                        Stack<Node> temp = new Stack<Node>();
                        temp = temporaryHolder;
                        temporaryHolder = frontier;
                        frontier = temp;
                        maxDepth += 2;
                    }
                    currentNode = frontier.Pop();
                }
                
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
            Console.Write("RANDOM MOVE!!");
            return randomMove(board, player);
        }
        
        public static int AStarSearch(char[] board, char player)
        {   
            char startPlayer = player;
            PriorityQueue<Node, int> frontier = new PriorityQueue<Node,int>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Enqueue(new Node(null, board, -1, (startPlayer == 'X') ? 'O' : 'X'), 0);
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

                    if (!visited.Contains(childNode) /*&& !frontier*/)
                    {
                        if (getWinner(childBoard) == startPlayer)
                        {
                            Console.WriteLine("TEST!");
                            Console.WriteLine("--------");
                            for (int x = 0; x < Settings.boardSizeLength; x++)
                            {
                                for (int y = 0; y < Settings.boardSizeLength; y++)
                                {
                                    Console.Write(childBoard[flatten(x, y)] == '\0'? ' ' : childBoard[flatten(x,y)]);
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
                            frontier.Enqueue(childNode, getDepth(childNode)*10 + heuristic(childNode, startPlayer));
                        }
                    }
                } 
            }
            //If unable to win, it will play a random move.
            Console.Write("RANDOM MOVE!!");
            return randomMove(board, player);
        }
        
        private static int heuristic(Node node, char player)
        {
            char[] board = node.board;

            int count = checkInARow(board, player);
            int otherPlayerCount = checkInARow(board, otherPlayer(player));

          

            if(otherPlayerCount == 3)
            {
                return 100;
            } else if (otherPlayerCount == 2)
            {
                return 10000;
            }
            

            return otherPlayerCount + count;
        }

        public static int checkInARow(char[] board, char player)
        {
            int max = 0;
            for (int i = 0; i < 2; i++)
            {

                int foundDiagonal1 = 0;
                int foundDiagonal2 = 0;

                for (int x = 0; x < boardSizeLength; x++)
                {
                    int foundDown = 0;
                    int foundAcross = 0;
                    for (int y = 0; y < boardSizeLength; y++)
                    {
                        if (board[flatten(x, y)] == player)
                            foundDown++;
                        if (board[flatten(y, x)] == player)
                            foundAcross++;
                    }
                    if (foundDown > max)
                    {
                        max=foundDown;
                    }
                    if (foundAcross > max)
                    {
                        max = foundAcross;
                    }

                    if (board[flatten(x, x)] == player)
                        foundDiagonal1++;
                    if (board[flatten((boardSizeLength - 1) - x, x)] == player)
                        foundDiagonal2++;
                }
                if (foundDiagonal1 > max)
                {
                    max = foundDiagonal1;
                }
                if (foundDiagonal2 > max)
                {
                    max = foundDiagonal2;
                }

            }
            return max;
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

        private static int getDepth(Node childNode)
        {
            int depth = 0;

            if (childNode.parent == null)
                return 0;
            while (childNode.parent != null)
            {
                childNode = childNode.parent;
                depth += 1;
            }
            return depth;
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
