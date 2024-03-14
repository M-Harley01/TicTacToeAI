﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Settings;

namespace TicTacToe
{
    public static class General
    {
        public static char currentPlayer = 'X';
        public static char[] mainBoard = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        public static bool gameOver = false;

        public static int flatten(int x, int y)
        {
            return x + y * boardSizeLength;
        }

        public static Tuple<int, int> deFlatten(int f)
        {
            return new Tuple<int, int>(f % boardSizeLength, f / boardSizeLength);
        }

        public static void nextTurn()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        public static void resetBoard(char[] board)
        {
            for (int i = 0; i < boardSizeSq; i++)
            {
                board[i] = '\0';
            }
        }

        public static bool checkGameOver(char[] board)
        {
            if (getWinner(board) != '\0')
                return true;
            for (int i = 0; i < boardSizeSq; i++)
            {
                if (board[i] == '\0')
                    return false;
            }
            return true;
        }

        public static List<int> findActions(char[] board)
        {
            List<int> actions = new List<int>();
            for (int i = 0; i < boardSizeSq; i++)
            {
                if (board[i] == '\0')
                    actions.Add(i);
            }
            return actions;
        }
        
        //TODO: OPTIMISE THIS PLEASE
        public static char getWinner(char[] board)
        {
            char checking = 'X';
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
                        if (board[flatten(x, y)] == checking)
                            foundDown++;
                        if (board[flatten(y, x)] == checking)
                            foundAcross++;
                    }
                    if (foundDown == boardSizeLength)
                    {
                        return checking;
                    }
                    if (foundAcross == boardSizeLength)
                    {
                        return checking;
                    }

                    if (board[flatten(x, x)] == checking)
                        foundDiagonal1++;
                    if (board[flatten((boardSizeLength - 1) - x, x)] == checking)
                        foundDiagonal2++;
                }
                if (foundDiagonal1 == boardSizeLength)
                {
                    return checking;
                }
                if (foundDiagonal2 == boardSizeLength)
                {
                    return checking;
                }

                checking = 'O';
            }
            return '\0';
        }

    }
}