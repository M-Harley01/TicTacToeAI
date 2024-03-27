using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
            HashQueue<Node> frontier = new();
            HashSet<Node> visited = [];

            //Enqueue the initial node with the starting board state
            frontier.Enqueue(new Node(null, board, -1, OtherPlayer(startPlayer)));

            //Loop until all nodes are explored
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Dequeue();
                visited.Add(currentNode);
                player = OtherPlayer(currentNode.lastPlayed);

                //Iterate through possible actions from the current node
                foreach (int action in FindActions(currentNode.board))
                {
                    //Create a copy of the board and take the action
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;

                    //Create a node representing this state
                    Node childNode = new(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        //Check if the current player has won after this action
                        if (GetWinner(childBoard, false) == startPlayer)
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        //If unable to win and the game is a draw
                        else if (!canWin && CheckGameOver(childBoard) && GetWinner(childBoard, false) != OtherPlayer(startPlayer))
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        else if (CheckGameOver(childBoard))
                        {
                            //Skip this action and continue with the next one
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
            Stack<Node> frontier = new();
            HashSet<Node> visited = [];

            frontier.Push(new Node(null, board, -1, OtherPlayer(startPlayer)));
            //Loop until all nodes are explored
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Pop();
                visited.Add(currentNode);
                player = OtherPlayer(currentNode.lastPlayed);

                //Iterate through possible actions from the current node
                foreach (int action in FindActions(currentNode.board))
                {
                    //Create a copy of the board and take the action
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;

                    //Create a node representing this state
                    Node childNode = new(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        //Check if the current player has won after this action
                        if (GetWinner(childBoard, false) == startPlayer)
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        //If unable to win and the game is a draw
                        else if (!canWin && CheckGameOver(childBoard) && GetWinner(childBoard, false) != OtherPlayer(startPlayer))
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        else if (CheckGameOver(childBoard))
                        {
                            //Skip this action and continue with the next one
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
            Stack<Node> frontier = new();
            Stack<Node> tooDeepStack = new();
            HashSet<Node> visited = [];

            frontier.Push(new Node(null, board, -1, OtherPlayer(startPlayer)));
            int maxDepth = 2;

            while (frontier.Count > 0 || tooDeepStack.Count > 0)
            {
                Node currentNode = frontier.Pop();
                
                //If the currentNode is too deep
                while(GetDepth(currentNode) > maxDepth)
                {
                    //Add it to the tooDeepStack
                    tooDeepStack.Push(currentNode);

                    //if the frontier is empty
                    if(frontier.Count == 0) {
                        //Swap frontier and tooDeepStack
                        (frontier, tooDeepStack) = (tooDeepStack, frontier);
                        maxDepth += 2;
                    }

                    currentNode = frontier.Pop();
                }
                
                visited.Add(currentNode);
                player = OtherPlayer(currentNode.lastPlayed);

                //Iterate through possible actions from the current node
                foreach (int action in FindActions(currentNode.board))
                {
                    //Create a copy of the board and take the action
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;

                    //Create a node representing this state
                    Node childNode = new(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode) && !frontier.Contains(childNode))
                    {
                        //Check if the current player has won after this action
                        if (GetWinner(childBoard, false) == startPlayer)
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        //If unable to win and the game is a draw
                        else if (!canWin && CheckGameOver(childBoard) && GetWinner(childBoard, false) != OtherPlayer(startPlayer))
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        else if (CheckGameOver(childBoard))
                        {
                            //Skip this action and continue with the next one
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
            PriorityQueue<Node, int> frontier = new();
            HashSet<Node> visited = [];

            frontier.Enqueue(new Node(null, board, -1, OtherPlayer(startPlayer)), 0);
            while (frontier.Count > 0)
            {
                Node currentNode = frontier.Dequeue();
                visited.Add(currentNode);
                player = OtherPlayer(currentNode.lastPlayed);

                //Iterate through possible actions from the current node
                foreach (int action in FindActions(currentNode.board))
                {
                    //Create a copy of the board and take the action
                    char[] childBoard = (char[])currentNode.board.Clone();
                    childBoard[action] = player;

                    //Create a node representing this state
                    Node childNode = new(currentNode, childBoard, action, player);

                    if (!visited.Contains(childNode))
                    {
                        //Check if the current player has won after this action
                        if (GetWinner(childBoard, false) == startPlayer)
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        //If unable to win and the game is a draw
                        else if (!canWin && CheckGameOver(childBoard) && GetWinner(childBoard, false) != OtherPlayer(startPlayer))
                        {
                            //Return the first action in the sequence
                            return GetFirstAction(childNode);
                        }
                        else if (CheckGameOver(childBoard))
                        {
                            //Skip this action and continue with the next one
                            continue;
                        }
                        else
                        {
                            //Add the node to the queue with a priorty of depth (path cost) + heuristic
                            frontier.Enqueue(childNode, GetDepth(childNode) + Heuristic(childNode, player, startPlayer)*10);
                        }
                    }
                } 
            }
            return -1;
        }

        private static int Heuristic(Node node, char player, char startPlayer)
        {
            char[] board = node.board;
            if (node.parent == null)
                return 0;

            char[] lastBoard = node.parent.board;
            int lastMove = node.action;
            int score = 0;

            //If the action blocks a winning move for the other player, it is good.
            if (GetNumberOfWinningMoves(lastBoard, OtherPlayer(startPlayer)) > 0 && GetNumberOfWinningMoves(board, OtherPlayer(startPlayer)) == 0)
            {
                score = -100;
            }
            //If the other player had any winning moves that haven't been blocked, it is bad.
            else if (GetNumberOfWinningMoves(lastBoard, OtherPlayer(startPlayer)) > 0)
            {
                score = 100;
            }
            else
            {
                //if the player has any winning moves, it is good.
                if (GetNumberOfWinningMoves(board, startPlayer) > 0)
                    score -= 10;
                //if the player controls the centre, it is good.
                else if ((boardSize % 2) != 0 && lastMove == boardSizeSq / 2 && player == startPlayer)
                    score -= 10;

                //If the other player has any winning moves, it is bad.
                score += (GetNumberOfWinningMoves(board, OtherPlayer(startPlayer))) * 10;
            }
            return score;

        }

        public static int GetNumberOfWinningMoves(char[] board, char player)
        {
            int winningMoves = 0;
            for (int i = 0; i < 2; i++)
            {

                int inADiagonal1 = 0;
                bool diagonal1Empty = true;
                int inADiagonal2 = 0;
                bool diagonal2Empty = true;

                for (int x = 0; x < boardSize; x++)
                {
                    int inAColumn = 0;
                    bool columnEmpty = true;
                    int inARow = 0;
                    bool rowEmpty = true;

                    for (int y = 0; y < boardSize; y++)
                    {
                        if (board[Flatten(x, y)] == player)
                            inAColumn++;
                        else if (board[Flatten(x, y)] == OtherPlayer(player))
                            columnEmpty = false;

                        if (board[Flatten(y, x)] == player)
                            inARow++;
                        else if(board[Flatten(y, x)] == OtherPlayer(player))
                            rowEmpty = false;
                    }

                    if (inAColumn == 2 && columnEmpty)
                        winningMoves++;
                    
                    if (inARow == 2 && rowEmpty)
                        winningMoves++;
                    

                    if (board[Flatten(x, x)] == player)
                        inADiagonal1++;
                    else if (board[Flatten(x, x)] == OtherPlayer(player))
                        diagonal1Empty = false;

                    if (board[Flatten((boardSize - 1) - x, x)] == player)
                        inADiagonal2++;
                    else if (board[Flatten((boardSize - 1) - x, x)] == OtherPlayer(player))
                        diagonal2Empty = false;

                }

                if (inADiagonal1 == 2 && diagonal1Empty)
                    winningMoves++;

                if (inADiagonal2 == 2 && diagonal2Empty)
                    winningMoves++;

            }
            return winningMoves;
        }
        private static int GetFirstAction(Node childNode)
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

        private static int GetDepth(Node childNode)
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

        private static int ScoreMiniMax(char[] board, char currentPlayer, char maximizingPlayer)
        {
            int score = 0;

            //If the game is over, determine the score based on the winner
            if (CheckGameOver(board))
            {
                if (GetWinner(board, false) == maximizingPlayer)
                    score = 10;
                else if (GetWinner(board, false) == OtherPlayer(maximizingPlayer))
                    score = -10;
            }
            //If the game is not over, evaluate the child states
            else
            {
                if(currentPlayer == maximizingPlayer)
                {
                    score = int.MinValue;
                    //Iterate over each possible action
                    foreach (int action in FindActions(board)) {
                        //Clone the board and do the action
                        char[] boardCopy = (char[])board.Clone();
                        boardCopy[action] = currentPlayer;

                        //Score the new board
                        int childScore = ScoreMiniMax(boardCopy, OtherPlayer(currentPlayer), maximizingPlayer);
                        if (childScore > score)
                            score = childScore;
                    }
                } else
                {
                    score = int.MaxValue;
                    //Iterate over each possible action
                    foreach (int action in FindActions(board))
                    {
                        //Clone the board and do the action
                        char[] boardCopy = (char[])board.Clone();
                        boardCopy[action] = currentPlayer;

                        //Score the new board
                        int childScore = ScoreMiniMax(boardCopy, OtherPlayer(currentPlayer), maximizingPlayer);
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
            List<int> actions = FindActions(board);

            //Iterate over each possible action
            for (int i = 0; i < actions.Count; i++)
            {
                //Make a copy of the board and do the action.
                char[] boardCopy = (char[])board.Clone();
                boardCopy[actions[i]] = player;

                //Score the board
                int score = ScoreMiniMax(boardCopy, OtherPlayer(player), player);

                //If the board has a higer score than all the others
                if (score > maxScore)
                {
                    maxScore = score;
                    index = i;
                }
            }
            //Return the action that has the highest score
            return actions[index];
        }

        public static int RandomAction(char[] board)
        {
            int pos = rng.Next(boardSizeSq);

            while (board[pos] != '\0')
                pos = rng.Next(boardSizeSq);
            
            return pos;
        }
    }
}
