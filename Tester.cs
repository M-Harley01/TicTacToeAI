﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TicTacToe.Algorithms;

namespace TicTacToe
{
    public class Tester
    {

        public enum Difficulty
        {
            EMPTY,
            HARD,
            MEDIUM,
            EASY,
            END,
        }

        static int getHowManyToFill(Difficulty difficulty)
        {
            switch (difficulty) {
                case Difficulty.EMPTY:
                    return 0;
                case Difficulty.HARD: 
                    return 2;
                case Difficulty.MEDIUM:
                    return 4;
                case Difficulty.EASY:
                    return 6;
                case Difficulty.END:
                    return 8;
            }

            return 8;
        }

        static char[] randomBoard(int fill)
        {
            char[] board = new char[Settings.boardSizeSq];

            for(int x = 0; x < Settings.boardSizeSq; x++)
            {
                board[x] = '\0';
            }

            char currentTurn = 'X';
            for (int i = 0; i < fill; i++) {
                int x = rng.Next(Settings.boardSizeSq);
                while (board[x] != '\0')
                {
                    x = rng.Next(Settings.boardSizeSq);
                }
                board[x] = currentTurn;
                currentTurn = currentTurn == 'X' ? 'O' : 'X';
            }
            return board;
        }

        public static long testAlgorithm(Func<char[], int> algorithm, Difficulty difficulty)
        {
            char[] board = randomBoard(getHowManyToFill(difficulty));
            General.mainBoard = (char[])board.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            algorithm.Invoke(board);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

    }
}
