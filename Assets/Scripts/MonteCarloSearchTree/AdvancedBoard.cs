using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonteCarlo
{
    class AdvancedBoard: Board
    {

        public static AdvancedBoard CreateBoardCopy(Board original)
        {

            AdvancedBoard board = new AdvancedBoard();
           
            board.data = new int[original.data.GetLength(0), original.data.GetLength(1)];
            Array.Copy(original.data, board.data, original.data.Length);



            board.CurrentOpponent = original.CurrentOpponent;
            board.CurrentPlayer = original.CurrentPlayer;


            return board;
        }
        public static AdvancedBoard CreateBoardCopy(int[,] data)
        {

            AdvancedBoard board = new AdvancedBoard();
            
            board.data = new int[data.GetLength(0), data.GetLength(1)];
            Array.Copy(data, board.data, data.Length);
            //board.CurrentOpponent = original.CurrentPlayer;
            //board.CurrentPlayer = original.CurrentOpponent;

            return board;
        }
        public override List<Vector2Int> GetPossibleWinMoves()
        {
            int player = CurrentPlayer; int opponent = CurrentOpponent;

            List<Vector2Int> movesWni = new List<Vector2Int>();

            // получить все свободные клетки, от каждой клетки в 6 сторон проверять выигрыш
            List<Vector2Int> freeMoves = GetAllPossibleMoves();

            movesWni = getRowWinMoves(freeMoves, player);


            return movesWni;
        }
        List<Vector2Int> getRowWinMoves(List<Vector2Int> freeMoves,int player)
        {
            List<Vector2Int> rowWinMoves = new List<Vector2Int>();



            foreach (var move in freeMoves)
            {

                int count = 0;
                // check row
                for (int i = move.x+1; i < data.GetLength(0); i++)
                {
                    if (data[i, move.y] == player)
                    {
                        count++;
                    }
                    else break;
                }
                for (int i = move.x-1; i >=0; i--)
                {
                    if (data[i, move.y] == player)
                    {
                        count++;
                    }
                    else break;
                }
                if (count == WIN_LENGTH - 1 && !rowWinMoves.Contains(move))
                {
                    rowWinMoves.Add(move);
                    continue;
                }


                // if move not win in row, then check  column
                count = 0;
                
                for (int i = move.y+1; i < data.GetLength(1); i++)
                {
                    if (data[move.x, i] == player)
                    {
                        count++;
                    }
                    else break;
                }
                for (int i = move.y - 1; i >= 0; i--)
                {
                    if (data[move.x, i] == player)
                    {
                        count++;
                    }
                    else break;
                }
                if (count == WIN_LENGTH - 1 && !rowWinMoves.Contains(move))
                {
                    rowWinMoves.Add(move);
                    continue;
                }


                // if move not win in row and column , then check  diagi=onals
                count = 0;

                count = checkMainDiagonal(move.x, move.y, player);


                if (count == WIN_LENGTH - 1 && !rowWinMoves.Contains(move))
                {
                    rowWinMoves.Add(move);
                    continue;
                }
                count = 0;
                count = checkOpposeDiagonal(move.x, move.y, player);

                if (count == WIN_LENGTH - 1 && !rowWinMoves.Contains(move))
                {
                    rowWinMoves.Add(move);
                    continue;
                }
            }


            return rowWinMoves;
        }



        public override int checkStatus(out Vector2Int startPos, out Vector2Int endPos)
        {
            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;
            int player = CurrentPlayer; int opponent = CurrentOpponent;
            int cellsTaken = 0;
            int result = 0;
            // check by colums
            for (int i = 0; i < data.GetLength(0); i++)
            {
                int countOfMovesPlayer = 0;
                int countOfMovesOpponent = 0;

                for (int j = 0; j < data.GetLength(1); j++)
                {

                    if (data[i, j] == player)
                    {
                        if (countOfMovesPlayer == 0)
                            startPos = new Vector2Int(i, j);

                        countOfMovesOpponent = 0;
                        countOfMovesPlayer++;
                        cellsTaken++;

                        if (countOfMovesPlayer == WIN_LENGTH)
                        {

                            endPos = new Vector2Int(i, j);

                            return player;
                        }
                    }
                    else if (data[i, j] == opponent)
                    {
                        if (countOfMovesOpponent == 0)
                            startPos = new Vector2Int(i, j);
                        countOfMovesPlayer = 0;
                        countOfMovesOpponent++;
                        cellsTaken++;
                      

                        if (countOfMovesOpponent == WIN_LENGTH)
                        {   endPos = new Vector2Int(i, j);
                        
                            return opponent;
                        }

                    }
                    else
                    {
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer = 0;
                        startPos = Vector2Int.one * -1;
                        endPos = Vector2Int.one * -1;
                    }
                }           
            }

            // check by rows
            for (int i = 0; i < data.GetLength(1); i++)
            {
                int countOfMovesPlayer = 0;
                int countOfMovesOpponent = 0;

                for (int j = 0; j < data.GetLength(0); j++)
                {

                    if (data[j, i] == player)
                    {
                        if (countOfMovesPlayer == 0)
                            startPos = new Vector2Int(j, i);
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer++;
                  

                        if (countOfMovesPlayer == WIN_LENGTH)
                        {
                            endPos = new Vector2Int(j, i);

                            return player;
                        }
                    }
                    else if (data[j, i] == opponent)
                    {
                        if (countOfMovesOpponent == 0)
                            startPos = new Vector2Int(j, i);

                        countOfMovesPlayer = 0;
                        countOfMovesOpponent++;
                
                        if (countOfMovesOpponent == WIN_LENGTH)
                        {
                            endPos = new Vector2Int(j, i);
                            return opponent;
                        }
                    }
                    else
                    {
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer = 0;
                        startPos = Vector2Int.one * -1;
                        endPos = Vector2Int.one * -1;
                    }
                }
            }

            // check all  main diagonals


            result = checkAllMainDiagonala(player, opponent, out startPos, out endPos);

            if (result == player || result == opponent)
            {
                return result;

            }
            // check all  opposite diagonals
            result = checkAllOppDiagonals(player, opponent, out startPos, out endPos);

            if (result == player || result == opponent)
            {
                return result;
            }


            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;
            // if all cells taken, but no one wins return -1
            if (cellsTaken == data.GetLength(0) * data.GetLength(1))
            {
                return -1;
            }


            return 0;
        }
        public override int checkStatus()
        {
            int player = CurrentPlayer; int opponent = CurrentOpponent;
            int cellsTaken = 0;
            int result = 0;
            // check by colums
            for (int i = 0; i < data.GetLength(0); i++)
            {
                int countOfMovesPlayer = 0;
                int countOfMovesOpponent = 0;

                for (int j = 0; j < data.GetLength(1); j++)
                {

                    if (data[i, j] == player)
                    {
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer++;
                        cellsTaken++;

                        if (countOfMovesPlayer == WIN_LENGTH)
                            return player;
                    }
                    else if (data[i, j] == opponent)
                    {
                        countOfMovesPlayer = 0;
                        countOfMovesOpponent++;
                        cellsTaken++;

                        if (countOfMovesOpponent == WIN_LENGTH)
                            return opponent;
                    }
                    else
                    {
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer = 0;

                    }
                }
                if (countOfMovesPlayer == WIN_LENGTH)
                {
                    return player;
                }
                if (countOfMovesOpponent == WIN_LENGTH)
                    return opponent;
            }

            // check by rows
            for (int i = 0; i < data.GetLength(1); i++)
            {
                int countOfMovesPlayer = 0;
                int countOfMovesOpponent = 0;

                for (int j = 0; j < data.GetLength(0); j++)
                {

                    if (data[j, i] == player)
                    {
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer++;

                        if (countOfMovesPlayer == WIN_LENGTH)
                            return player;
                    }
                    else if (data[j, i] == opponent)
                    {
                        countOfMovesPlayer = 0;
                        countOfMovesOpponent++;

                        if (countOfMovesOpponent == WIN_LENGTH)
                            return opponent;
                    }
                    else
                    {
                        countOfMovesOpponent = 0;
                        countOfMovesPlayer = 0;

                    }
                }
                if (countOfMovesPlayer == WIN_LENGTH)
                {
                    return player;
                }
                if (countOfMovesOpponent == WIN_LENGTH)
                    return opponent;
            }

            // check all  main diagonals


            result = checkAllMainDiagonala(player, opponent);

            if (result == player || result == opponent)
                return result;
            // check all  opposite diagonals
            result = checkAllOppDiagonals(player, opponent);

            if (result == player || result == opponent)
                return result;

            // if all cells taken, but no one wins return -1
            if (cellsTaken == data.GetLength(0) * data.GetLength(1))
            {
                return -1;
            }


            return 0;
        }








        int checkAllMainDiagonala( int player, int opponent)
        {

            for (int i = 0; i <= data.GetLength(0) - WIN_LENGTH; i++)
            {

                 int countOfPlayerFigures = checkMainDiagonal( i, 0, player);
                  if (countOfPlayerFigures == WIN_LENGTH)
                    return player;

                int countOfOpponentFigures = checkMainDiagonal(i, 0, opponent);
                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponent;
                 

            }
            for (int j = 1; j < data.GetLength(1) - WIN_LENGTH; j++)
            {
                int countOfPlayerFigures = checkMainDiagonal( 0, j, player);
                 if (countOfPlayerFigures == WIN_LENGTH)
                    return player;

                int countOfOpponentFigures =  checkMainDiagonal(0, j, opponent);
                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponent;
                 
            }
            return 0;
        }


         int checkAllOppDiagonals( int playerSign, int opponentSign)
        {
            for (int i = 0; i <= data.GetLength(0) - WIN_LENGTH; i++)
            {          
                int countOfPlayerFigures = checkOpposeDiagonal(i, data.GetLength(0) - 1, playerSign);
                if (countOfPlayerFigures == WIN_LENGTH)
                    return playerSign;


                int countOfOpponentFigures = checkOpposeDiagonal(i, data.GetLength(0) - 1, opponentSign);

                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponentSign;
                 
            }
            for (int j = data.GetLength(1) - WIN_LENGTH; j < data.GetLength(1); j++)
            {
                int countOfPlayerFigures = checkOpposeDiagonal( j, 0, playerSign);
                if (countOfPlayerFigures == WIN_LENGTH)
                    return playerSign;

                int countOfOpponentFigures = checkOpposeDiagonal(j, 0, opponentSign);

                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponentSign;
                 
            }
            return 0;
        }



        //returns count of x or 0 in one line
        int checkMainDiagonal( int row, int column, int player)
        {
          
            int count = 0;
            if (data[row, column] == player)
                count += 1;
            for (int j = column + 1, i = row + 1; j < data.GetLength(1) && i < data.GetLength(0); j++, i++)
            {
                if (data[i, j] == player)
                {

                    count++;
                }
                else
                    break;
            }
            for (int j = column - 1, i = row - 1; j >= 0 && i >= 0; j--, i--)
            {
                if (data[i, j] == player)
                {

                    count++;
                }
                else
                    break;
            }
           
            return count;
        }


        //returns count of x or 0 in one line

         int checkOpposeDiagonal( int row, int column, int player)
        {
           
            int count = 0;
            if (data[row, column] == player)
                count += 1;

            for (int j = column - 1, i = row + 1; j >= 0 && i < data.GetLength(0); j--, i++)
            {
                if (data[i, j] == player)
                {
                    count++;
                }
                else
                    break;
            }
            for (int j = column + 1, i = row - 1; j < data.GetLength(0) && i >= 0; j++, i--)
            {
                if (data[i, j] == player)
                {

                    count++;
                }
                else
                    break;
            }  
            return count;
        }





        //////
        ///

        int checkAllMainDiagonala(int player, int opponent, out Vector2Int startPos, out Vector2Int endPos)
        {
            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;
            for (int i = 0; i <= data.GetLength(0) - WIN_LENGTH; i++)
            {

                int countOfPlayerFigures = checkMainDiagonal(i, 0, player, out startPos, out endPos);
                if (countOfPlayerFigures == WIN_LENGTH)
                    return player;

                int countOfOpponentFigures = checkMainDiagonal(i, 0, opponent, out startPos, out endPos);
                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponent;
                 

            }
            for (int j = 1; j < data.GetLength(1) - WIN_LENGTH; j++)
            {
                int countOfPlayerFigures = checkMainDiagonal(0, j, player, out startPos, out endPos);
                if (countOfPlayerFigures == WIN_LENGTH)
                    return player;
                int countOfOpponentFigures = checkMainDiagonal(0, j, opponent, out startPos, out endPos);
                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponent;
                 
            }
            return 0;
        }


        int checkAllOppDiagonals(int playerSign, int opponentSign, out Vector2Int startPos, out Vector2Int endPos)
        {
            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;
            for (int i = 0; i <= data.GetLength(0) - WIN_LENGTH; i++)
            {
                int countOfPlayerFigures = checkOpposeDiagonal(i, data.GetLength(0) - 1, playerSign, out startPos, out endPos);
                if (countOfPlayerFigures == WIN_LENGTH)
                    return playerSign;


                int countOfOpponentFigures = checkOpposeDiagonal(i, data.GetLength(0) - 1, opponentSign, out startPos, out endPos);
                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponentSign;

         
            }
            for (int j = data.GetLength(1) - WIN_LENGTH; j < data.GetLength(1); j++)
            {
                int countOfPlayerFigures = checkOpposeDiagonal(j, 0, playerSign, out startPos, out endPos);
                if (countOfPlayerFigures == WIN_LENGTH)
                    return playerSign;

                int countOfOpponentFigures = checkOpposeDiagonal(j, 0, opponentSign, out startPos, out endPos);
                if (countOfOpponentFigures == WIN_LENGTH)
                    return opponentSign;
                
            }
            return 0;
        }



        //returns count of x or 0 in one line
        int checkMainDiagonal(int row, int column, int player, out Vector2Int startPos, out Vector2Int endPos)
        {

            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;
            int count = 0;
            if (data[row, column] == player)
            {

                startPos = new Vector2Int(row, column);
                endPos = new Vector2Int(row, column);

                count += 1;
            }
            for (int j = column + 1, i = row + 1; j < data.GetLength(1) && i < data.GetLength(0); j++, i++)
            {
                if (data[i, j] == player)
                {
                    endPos = new Vector2Int(i, j);

                    count++;
                }
                else
                    break;
            }
            for (int j = column - 1, i = row - 1; j >= 0 && i >= 0; j--, i--)
            {
                if (data[i, j] == player)
                {
                    startPos = new Vector2Int(i, j);

                    count++;
                }
                else
                    break;
            }

            return count;
        }


        //returns count of x or 0 in one line

        int checkOpposeDiagonal(int row, int column, int player, out Vector2Int startPos, out Vector2Int endPos)
        {
            startPos = Vector2Int.one * -1;
            endPos = Vector2Int.one * -1;
            int count = 0;
            if (data[row, column] == player)
            {
                startPos = new Vector2Int(row, column);
                endPos = new Vector2Int(row, column);

                count += 1;
            }
            for (int j = column - 1, i = row + 1; j >= 0 && i < data.GetLength(0); j--, i++)
            {
                if (data[i, j] == player)
                {
                    endPos = new Vector2Int(i, j);

                    count++;
                }
                else
                    break;
            }
            for (int j = column + 1, i = row - 1; j < data.GetLength(0) && i >= 0; j++, i--)
            {
                if (data[i, j] == player)
                {
                    startPos = new Vector2Int(i, j);

                    count++;
                }
                else
                    break;
            }
            return count;
        }










    }


}
