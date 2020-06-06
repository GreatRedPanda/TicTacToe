using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonteCarlo
{
    class Board
    {
        public static int WIN_LENGTH;
        //public static int Size;

        public int[,] data;
        int boardNumber;
        static int boardsCreated = 0;

      public  int CurrentPlayer;
        public int CurrentOpponent;

     
        public static Board CreateBoardCopy(Board original)
        {
            boardsCreated++;
            Board board = new Board();
            board.boardNumber = boardsCreated;
            board.data = new int[original.data.GetLength(0), original.data.GetLength(1)];
            Array.Copy(original.data, board.data, original.data.Length);



            board.CurrentOpponent = original.CurrentOpponent;
            board.CurrentPlayer = original.CurrentPlayer;


            return board;
        }

        public static Board CreateBoardCopy(int[,] data)
        {
            boardsCreated++;
            Board board = new Board();
            board.boardNumber = boardsCreated;
            board.data = new int[data.GetLength(0), data.GetLength(1)];
            Array.Copy(data, board.data, data.Length);
            //board.CurrentOpponent = original.CurrentPlayer;
            //board.CurrentPlayer = original.CurrentOpponent;

            return board;
        }




        public void TogglePlayer()
        {


                int currentPlayer= CurrentPlayer;
         int currentOpponent= CurrentOpponent;
            CurrentOpponent = currentPlayer;
            CurrentPlayer = currentOpponent;
    }

        public void makeRandomMove()
        {

            List<Vector2Int> winMoves = GetPossibleWinMoves();


            if (winMoves.Count != 0)
            {

                Vector2Int move = winMoves[Randomizer.GetRandIndex(winMoves.Count)];

                data[move.x, move.y] = CurrentPlayer;
                return;
            }
            List<Vector2Int> possibleMoves = GetAllPossibleMoves();

            if (possibleMoves.Count != 0)
            {

                Vector2Int move = possibleMoves[Randomizer.GetRandIndex(possibleMoves.Count)];

                data[move.x, move.y] = CurrentPlayer;
                return;
            }


         }



        public virtual int checkStatus(out Vector2Int startPos, out Vector2Int endPos)
        {

            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;

            return 0;

        }
        /// <summary>
        ///  -1 - draw result, 0 = in progress, 1- x player win, 2- 0 player win
        /// </summary>
        /// <param name="player"></param>
        /// <param name="opponent"></param>
        /// <returns></returns>
        public virtual int checkStatus()
        {
            int player = CurrentPlayer;  int opponent = CurrentOpponent;
            int cellsTaken = 0;
            int result = 0;

            for (int i = 0; i < 3; i++)
            {
                if (data[0, i] == player && data[1, i] == player && data[2, i] == player)
                    result = player;
                else if (data[0, i] == opponent && data[1, i] == opponent && data[2, i] == opponent)
                    result = opponent;
                else if (data[0, i] !=0 && data[1, i] !=0 && data[2, i]!=0)
                {
                    cellsTaken += 3;
                }
            }


            for (int i = 0; i < 3; i++)
            {
                if (data[ i,0] == player && data[i,1] == player && data[i,2] == player)
                    result = player;
                else if (data[i, 0] == opponent && data[i, 1] == opponent && data[i, 2] == opponent)
                    result = opponent;            
            }
            //check main diagonal    

            if (data[1, 1] == player && data[2, 2] == player && data[0, 0] == player)
                result = player;
            else if(data[1, 1] == opponent && data[2, 2] == opponent && data[0, 0] == opponent)
                result = opponent;

            // check second diagonal
            if (data[0, 2] == opponent && data[1, 1] == opponent && data[2, 0] == opponent)
                result = opponent;
            else if (data[0, 2] == player && data[1, 1] == player && data[2, 0] == player)
                result = player;



            if (cellsTaken == 9  && result!=player   && result!=opponent)
            {
                return -1;
            }


            return result;
        }
        public virtual  List<Vector2Int> GetPossibleWinMoves()
        {
            int player = CurrentPlayer; int opponent = CurrentOpponent;

            List<Vector2Int> movesWni = new List<Vector2Int>();

            for (int i = 0; i < 3; i++)
            {
                if (data[0, i] == player && data[1, i] == player && data[2, i] == 0)
                    movesWni.Add(new Vector2Int(2, i));
                if (data[0, i] == player && data[1, i] == player && data[1, i] == 0)
                    movesWni.Add(new Vector2Int(2, i));
                if (data[1, i] == player && data[2, i] == player && data[0, i] == 0)
                    movesWni.Add(new Vector2Int(0, i));
            }
            for (int i = 0; i < 3; i++)
            {
                if (data[i, 1] == player && data[i, 2] == player && data[i, 0] == 0)
                    movesWni.Add(new Vector2Int(i, 0));
                if (data[i, 2] == player && data[i, 0] == player && data[i, 1] == 0)
                    movesWni.Add(new Vector2Int(i, 1));
                if (data[i, 0] == player && data[i, 1] == player && data[i, 2] == 0)
                    movesWni.Add(new Vector2Int(i, 2));
            }

            if (data[1, 1] == player && data[2, 2] == player && data[0, 0] == 0)
                movesWni.Add(new Vector2Int(0, 0));
            if (data[2, 2] == player && data[0, 0] == player && data[1, 1] == 0)
                movesWni.Add(new Vector2Int(1, 1));
            if (data[0, 0] == player && data[1, 1] == player && data[2, 2] == 0)
                movesWni.Add(new Vector2Int(2, 2));

            if (data[0, 2] == player && data[1, 1] == player && data[2, 0] == 0)
                movesWni.Add(new Vector2Int(2, 0));
            if (data[2, 0] == player && data[0, 2] == player && data[1, 1] == 0)
                movesWni.Add(new Vector2Int(1, 1));
            if (data[2, 0] == player && data[1, 1] == player && data[0, 2] == 0)
                movesWni.Add(new Vector2Int(0, 2));

            return movesWni;
        }


        public List<Vector2Int> GetAllPossibleMoves()
        {


            List<Vector2Int> possibleMoves = new List<Vector2Int>();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == 0)
                    {
                      
                        possibleMoves.Add(new Vector2Int(i,j));
                    }
                }
            }
            return possibleMoves;

        }


        public  List<Board> GetPossibleMovesBoards(int player)
        {


            List<Board> possibleBoards = new List<Board>();

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == 0)
                    {
                        Board newBoard = AdvancedBoard.CreateBoardCopy(data);
                        newBoard.data[i, j] = player;
                        possibleBoards.Add(newBoard);
                    }
                }
            }
            return possibleBoards;
        }
    }
}
