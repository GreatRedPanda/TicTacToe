using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class AlgorithmUtil 
{


    public static float GetPositionValue(int x, int y, int size, int radius)
    {
        float value = 0;
        int center = size / 2;
        float distanceToCenter = Vector2Int.Distance(new Vector2Int(x, y), Vector2Int.one * center);
        value += 1/ (1 + distanceToCenter) * 100;

        return value;

    }
    public static bool CheckWin(int[,] data, int winLength, int targetPlayerSign, int lastMoveX, int lastMoveY, out Vector2Int startWinPos, out Vector2Int endWinPos)
    {
         startWinPos = Vector2Int.one * -1;
         endWinPos = Vector2Int.one * -1;
        int sign = 0;
        bool res = checkRow(data, lastMoveY, lastMoveX, winLength, out sign);

        if (res && sign==targetPlayerSign)
        {
            startWinPos = new Vector2Int(0, lastMoveY);
            endWinPos = new Vector2Int(data.GetLength(0)-1, lastMoveY);
            return true;
        }
        res = checkColumn(data,  lastMoveX, lastMoveY, winLength, out sign);
      
        if (res && sign == targetPlayerSign)
        {
            startWinPos = new Vector2Int(lastMoveX, 0);
            endWinPos = new Vector2Int(lastMoveX,data.GetLength(1) - 1);
            return true;
        }

        //if (lastMoveX != lastMoveY && lastMoveX != data.GetLength(1) - lastMoveY - 1)
        //    return false;

         res= checkMainDiagonal(data ,  lastMoveX,  lastMoveY, winLength, out sign);

        if (res && sign == targetPlayerSign)
        {
            startWinPos = new Vector2Int(0, 0);
            endWinPos = new Vector2Int(data.GetLength(1) - 1, data.GetLength(1) - 1);
            return true;
        }

        res = checkOpposeDiagonal(data ,  lastMoveX,  lastMoveY, winLength, out sign);
       
        if (res && sign == targetPlayerSign)
        {
            startWinPos = new Vector2Int(data.GetLength(0) - 1, 0);
            endWinPos = new Vector2Int(0, data.GetLength(1) - 1);
            return true;
        }

        return false;
    }



    public static int CheckAllPlayersWin(int[,] data, int targetPlayerSign, int opponentSign, int winLength)
    {
        int sign = 0;
        //check all colums
        for (int i = 0; i < data.GetLength(0); i++)
        {
           
            bool res = checkRow(data, i, winLength,out sign);
            if (res)
                if(sign==targetPlayerSign)
                return targetPlayerSign;
                else  if (sign == opponentSign)
                return opponentSign;
        }
        //check all rows
        for (int i = 0; i < data.GetLength(0); i++)
        {
            bool res = checkColumn(data,  i, winLength, out sign);
            if (res)
                if (sign == targetPlayerSign)
                    return targetPlayerSign;
                else if (sign == opponentSign)
                    return opponentSign;
        }


        sign = checkAllMainDiagonala(data, winLength, targetPlayerSign, opponentSign);

        if (sign != 0)
            if (sign == 1)
                return targetPlayerSign;
            else if (sign == -1)
                return opponentSign;

        sign = checkAllOppDiagonals(data, winLength, targetPlayerSign, opponentSign);

        if (sign != 0)
            if (sign == 1)
                return targetPlayerSign;
            else if (sign == -1)
                return opponentSign;


        //bool  resDiag = checkMainDiagonal(data, out sign);
        //  if (resDiag)
        //  {

        //          if (sign == targetPlayerSign)
        //              return 1;
        //          else if (sign == opponentSign)
        //              return -1;
        //  }
        //  resDiag = checkOpposeDiagonal(data, out sign);
        //  if (resDiag)
        //  {

        //          if (sign == targetPlayerSign)
        //              return 1;
        //          else if (sign == opponentSign)
        //              return -1;
        //  }


        return 0;


    }
    public static List<Vector2Int> GetPossibleMoves(int[,] data)
    {


        List<Vector2Int> possibleMoves = new List<Vector2Int>();

        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (data[i, j] == 0)
                {

                    possibleMoves.Add(new Vector2Int(i, j));
                }
            }

        }


        return possibleMoves;
    }



    static bool checkRow(int[,] data, int column,  int winLength, out int tarfetSign)
    {
        tarfetSign = data[0, column];
      
        int longestChain = 0;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            if (data[i, column] == tarfetSign)
            {
                longestChain++;
            }
            else
            {
                tarfetSign = data[i, column];
                longestChain = 0;
            }

            if (longestChain == winLength)
                return true;

        }
        if (longestChain == winLength)
            return true;

        return false;
    }

    static bool checkColumn(int[,] data, int row, int winLength, out int tarfetSign)
    {
        tarfetSign = data[row, 0];

        int longestChain = 0;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            if (data[row, i] == tarfetSign)
            {
                longestChain++;
            }
            else
            {
                tarfetSign = data[row, i];
                longestChain = 0;
            }

            if (longestChain == winLength)
                return true;

        }
        if (longestChain == winLength)
            return true;

        return false;
    }


    static bool checkRow(int[,] data, int column, int curRow, int winLength, out int tarfetSign)
    {
         tarfetSign = data[curRow, column];
        int count = 1;
        // check horizontal
        for (int i = curRow+1; i < data.GetLength(0); i++)
        {
            if (data[i, column] == tarfetSign)
            {
                count++;
            }
            else
                break;
        }
        for (int i = curRow - 1; i >=0; i--)
        {
            if (data[i, column] == tarfetSign)
            {
                count++;
            }
            else
                break;

        }

        if (count == winLength)
            return true;

        return false;
    }


   static   bool checkColumn(int[,] data, int row, int curColumn, int winLength, out int tarfetSign)
    {

         tarfetSign = data[row, curColumn];
        int  count = 1;
        for (int j = curColumn+1; j < data.GetLength(1); j++)
        {
            if (data[row, j] == tarfetSign)
            {
                count++;
            }
            else
                break;

        }
        for (int j = curColumn - 1; j >=0; j--)
        {
            if (data[row, j] == tarfetSign)
            {
                count++;
            }
            else
                break;

        }


        if (count == winLength)
            return true;
        return false;
    }





    static bool checkMainDiagonal(int[,] data, int row, int column, int winLength, out int tarfetSign)
    {
         tarfetSign = data[row, column];
        int  count = 1;
        for (int j = column+1, i = row+1; j < data.GetLength(1) && i < data.GetLength(0); j++, i++)
        {
            if (data[i, j] == tarfetSign)
            {

                count++;
            }
            else
                break;
        }
        for (int j = column-1, i = row-1; j >=0 && i>=0; j--, i--)
        {
            if (data[i, j] == tarfetSign)
            {

                count++;
            }
            else
                break;
        }
        if (count == winLength)
            return true;
        return false;
    }

    static int checkAllMainDiagonala(int[,] data, int winLength,  int playerSign, int opponentSign)
    {


        for (int i = 0; i < data.GetLength(0)-winLength; i++)
        {
            int sign = 0;
            bool res = checkMainDiagonal(data, i, 0, winLength, out sign);
            if (res)
            {
                if (sign == playerSign)
                    return 1;
                else if (sign == opponentSign)
                    return -1;
            }
        }
        for (int j = 1; j < data.GetLength(1) - winLength; j++)
        {

            int sign = 0;

            bool res = checkMainDiagonal(data, 0, j, winLength, out sign);
            if (res)
            {
                if (sign == playerSign)
                    return 1;
                else if (sign == opponentSign)
                    return -1;
            }
        }

        return 0;
    }


    static int checkAllOppDiagonals(int[,] data, int winLength, int playerSign, int opponentSign)
    {


        for (int i = 0; i < data.GetLength(0) - winLength; i++)
        {
            int sign = 0;
            bool res = checkOpposeDiagonal(data, i, data.GetLength(0)-1, winLength, out sign);
            if (res)
            {
                if (sign == playerSign)
                    return 1;
                else if (sign == opponentSign)
                    return -1;
            }
        }
        for (int j = data.GetLength(1) - winLength; j < data.GetLength(1); j++)
        {

            int sign = 0;

            bool res = checkOpposeDiagonal(data, j, 0, winLength, out sign);
            if (res)
            {
                if (sign == playerSign)
                    return 1;
                else if (sign == opponentSign)
                    return -1;
            }
        }

        return 0;
    }

    static bool checkOpposeDiagonal(int[,] data, int row, int column,  int winLength,out int tarfetSign)
    {
        tarfetSign = data[row,column];
        int count = 1;

        for (int j = column - 1, i = row + 1; j >=0 && i < data.GetLength(0); j--, i++)
        {
            if (data[i, j] == tarfetSign)
            {
                count++;
            }
            else
                break;
        }
        for (int j = column + 1, i = row - 1; j < data.GetLength(0) && i >= 0; j++, i--)
        {
            if (data[i, j] == tarfetSign)
            {

                count++;
            }
            else
                break;
        }

        if (count == winLength)
            return true;

        return false;
    }




    public static List<int[,]> GetPossibleMovesBoards(int[,] data, int player)
    {


        List<int[,]> possibleMoves = new List<int[,]>();

        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (data[i, j] == 0)
                {

                    int[,] newBoard = new int[data.GetLength(0), data.GetLength(1)];

                    Array.Copy(data, newBoard, data.Length);
                    newBoard[i, j] = player;

                    possibleMoves.Add(newBoard);
                }
            }

        }


        return possibleMoves;
    }

    public static List<Vector2Int> GetPossibleWinMoves(int[,] data, int player, int winLength)
    {


        List<Vector2Int> movesWni = new List<Vector2Int>();

        for (int i = 0; i < 3; i++)
        {
            if (data[0, i] == player && data[1, i] == player && data[2, i] == 0)
                movesWni.Add(new Vector2Int(2,i));
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
    //public static List<Vector2Int> GetPossibleWinMoves(int[,] data, int player, int winLength)
    //{
    //    winLength -= 1;

    //    List<Vector2Int> possibleMoves = new List<Vector2Int>();



    //    for (int i = 0; i < data.GetLength(0); i++)
    //    {
    //        Vector2Int winMove = new Vector2Int(-1, -1);
    //        int count = 0;
    //        int prevNum = 0;
    //        for (int j = 0; j < data.GetLength(1); j++)
    //        {
    //            if (data[i, j] == 0)
    //            {

    //                winMove = new Vector2Int(i, j);
    //            }
    //            else if (data[i, j] == player)
    //            {
    //                count++;
    //            }
    //            else
    //            {

    //                Debug.Log("count of player signs "+count);
    //                if (count == winLength && winMove!=Vector2Int.one*-1)
    //                {
    //                    possibleMoves.Add(winMove);
    //                    break;
    //                }
    //                count = 0;
    //            }


    //            prevNum = data[i, j];
    //        }

    //        Debug.Log("count of player signs " + count);

    //        if (count == winLength && winMove != Vector2Int.one * -1)
    //        {
    //            possibleMoves.Add(winMove);
    //        }

    //    }

    //    for (int i = 0; i < data.GetLength(0); i++)
    //    {     Vector2Int winMove = new Vector2Int(-1, -1);
    //    int count = 0;
    //      int   prevNum = 0;
    //        for (int j = 0; j < data.GetLength(1); j++)
    //        {
    //            if (data[j, i] == 0)
    //          {

    //                winMove = new Vector2Int(j, i);
    //            }
    //            else if (data[j, i] == player)
    //            {
    //                count++;
    //            }
    //            else
    //            {
    //                Debug.Log("count of player signs in row " + count);

    //                if (count == winLength && winMove != Vector2Int.one * -1)
    //                {
    //                    possibleMoves.Add(winMove);
    //                    break;
    //                }
    //                count = 0;
    //              }
    //            prevNum = data[j, i];
    //        }
    //        Debug.Log("count of player signs  in row" + count);

    //        if (count == winLength && winMove != Vector2Int.one * -1)
    //        {
    //            possibleMoves.Add(winMove);
    //        }

    //    }


    //    return possibleMoves;
    //}
}
