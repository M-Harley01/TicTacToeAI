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

        public static int BreadthFirst(char[] board, char player, bool canWin)
        {
            char startPlayer = player;
            HashQueue<Node> frontier = new HashQueue<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Enqueue(new Node(null, board, -1, otherPlayer(startPlayer)));
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Dequeue();
                visited.Add(currentNode);
                player = otherPlayer(currentNode.lastPlayed);
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    Node childNode = new Node(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        if (getWinner(childBoard, false) == startPlayer)
                        {
                            return getFirstAction(childNode);
                        }
                        else if (!canWin && checkGameOver(childBoard) && getWinner(childBoard, false) != otherPlayer(startPlayer))
                        {
                            return getFirstAction(childNode);
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
            return -1;
        }


        public static int DepthFirstSearch(char[] board, char player, bool canWin)
        {
            char startPlayer = player;
            Stack<Node> frontier = new Stack<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Push(new Node(null, board, -1, otherPlayer(startPlayer)));
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Pop();
                visited.Add(currentNode);
                player = otherPlayer(currentNode.lastPlayed);
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    Node childNode = new Node(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        if (getWinner(childBoard, false) == startPlayer)
                        {
                            return getFirstAction(childNode);
                        }
                        else if (!canWin && checkGameOver(childBoard) && getWinner(childBoard, false) != otherPlayer(startPlayer))
                        {
                            return getFirstAction(childNode);
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
            return -1;
        }
        public static int IterativeDeepeningDepthFirstSearch(char[] board, char player, bool canWin)
        {
            char startPlayer = player;
            Stack<Node> frontier = new Stack<Node>();
            Stack<Node> tooDeepStack = new Stack<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Push(new Node(null, board, -1, otherPlayer(startPlayer)));
            int maxDepth = 2;

            while (frontier.Count > 0 || tooDeepStack.Count > 0)
            {
                Node currentNode = frontier.Pop();
                while(getDepth(currentNode) > maxDepth)
                {
                    tooDeepStack.Push(currentNode);

                    if(frontier.Count == 0) { 
                        Stack<Node> buffer = tooDeepStack;
                        tooDeepStack = frontier;
                        frontier = buffer;
                        maxDepth += 2;
                    }
                    currentNode = frontier.Pop();
                }
                
                visited.Add(currentNode);
                player = otherPlayer(currentNode.lastPlayed);
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    Node childNode = new Node(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        if (getWinner(childBoard, false) == startPlayer)
                        {
                            return getFirstAction(childNode);
                        }
                        else if (!canWin && checkGameOver(childBoard) && getWinner(childBoard, false) != otherPlayer(startPlayer))
                        {
                            return getFirstAction(childNode);
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
            return -1;
        }
        
        public static int AStarSearch(char[] board, char player, bool canWin)
        {   
            char startPlayer = player;
            PriorityQueue<Node, int> frontier = new PriorityQueue<Node,int>();
            HashSet<Node> visited = new HashSet<Node>();

            frontier.Enqueue(new Node(null, board, -1, otherPlayer(startPlayer)), 0);
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Dequeue();
                visited.Add(currentNode);
                player = otherPlayer(currentNode.lastPlayed);
                foreach (int action in findActions(currentNode.board))
                {
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;
                    Node childNode = new Node(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode))
                    {
                        if (getWinner(childBoard, false) == startPlayer)
                        {
                            return getFirstAction(childNode);
                        }
                        else if (!canWin && checkGameOver(childBoard) && getWinner(childBoard, false) != otherPlayer(startPlayer))
                        {
                            return getFirstAction(childNode);
                        }
                        else if (checkGameOver(childBoard))
                        {
                            continue;
                        }
                        else
                        {
                            frontier.Enqueue(childNode, getDepth(childNode) + heuristic(childNode, player, startPlayer)*10);
                        }
                    }
                } 
            }
            return -1;
        }

        private static int heuristic(Node node, char player, char startPlayer)
        {
            char[] board = node.board;
            if (node.parent == null)
                return 0;

            char[] lastBoard = node.parent.board;
            int lastMove = node.action;
            int score = 0;

            if (getNumberOfWinningMoves(lastBoard, otherPlayer(startPlayer)) > 0 && getNumberOfWinningMoves(board, otherPlayer(startPlayer)) == 0)
            {
                score = -100;
            }
            else if (getNumberOfWinningMoves(lastBoard, otherPlayer(startPlayer)) > 0)
            {
                score = 100;
            }
            else
            {
                if (getNumberOfWinningMoves(board, startPlayer) > 0)
                    score -= 10;
                else if ((boardSizeLength % 2) != 0 && lastMove == boardSizeSq / 2 && player == startPlayer)
                    score -= 10;

                score += (getNumberOfWinningMoves(board, otherPlayer(startPlayer))) * 10;
            }
            return score;

        }

        public static int getNumberOfWinningMoves(char[] board, char player)
        {
            int winningMoves = 0;
            for (int i = 0; i < 2; i++)
            {

                int inADiagonal1 = 0;
                bool diagonal1Empty = true;
                int inADiagonal2 = 0;
                bool diagonal2Empty = true;

                for (int x = 0; x < boardSizeLength; x++)
                {
                    int inAColumn = 0;
                    bool columnEmpty = true;
                    int inARow = 0;
                    bool rowEmpty = true;

                    for (int y = 0; y < boardSizeLength; y++)
                    {
                        if (board[flatten(x, y)] == player)
                            inAColumn++;
                        else if (board[flatten(x, y)] == otherPlayer(player))
                            columnEmpty = false;

                        if (board[flatten(y, x)] == player)
                            inARow++;
                        else if(board[flatten(y, x)] == otherPlayer(player))
                            rowEmpty = false;
                    }

                    if (inAColumn == 2 && columnEmpty)
                        winningMoves++;
                    
                    if (inARow == 2 && rowEmpty)
                        winningMoves++;
                    

                    if (board[flatten(x, x)] == player)
                        inADiagonal1++;
                    else if (board[flatten(x, x)] == otherPlayer(player))
                        diagonal1Empty = false;

                    if (board[flatten((boardSizeLength - 1) - x, x)] == player)
                        inADiagonal2++;
                    else if (board[flatten((boardSizeLength - 1) - x, x)] == otherPlayer(player))
                        diagonal2Empty = false;

                }

                if (inADiagonal1 == 2 && diagonal1Empty)
                    winningMoves++;

                if (inADiagonal2 == 2 && diagonal2Empty)
                    winningMoves++;

            }
            return winningMoves;
        }
        private static int getFirstAction(Node childNode)
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
                if (getWinner(board, false) == maximizingPlayer)
                    score = 10;
                else if (getWinner(board, false) == otherPlayer(maximizingPlayer))
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

        public static int MiniMax(char[] board, char player, bool canWin)
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

        public static int randomAction(char[] board)
        {
            int pos = rng.Next(boardSizeSq);

            while (board[pos] != '\0')
                pos = rng.Next(boardSizeSq);
            
            return pos;
        }
    }
}
