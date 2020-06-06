using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.MCTS;
public class AiPlayer : PlayerBase
{
    public override event Action<PlayerBase, int, int> OnMadeMove;


    public int AIRecursiveLevel = 4;

    int yourSign;
    int opponentSign;


    Vector2Int move;
     public   int winLEngth = 0;


    public override void PassMove(int[,] data, int playerSign, int opponentSign)
    {
        this.opponentSign = opponentSign;
        this.yourSign = playerSign;

         MCTreeSearch.findNextMove(data, playerSign, opponentSign, 200*winLEngth, winLEngth, ref move);
     //   findBestMove(data);
        OnMadeMove(this, move.x, move.y);




    }


    float runMinMax(int[,] data, int recursiveLevel, bool maximizing, float alpha, float beta)
    {

        List<Vector2Int> possibleMoves = AlgorithmUtil.GetPossibleMoves(data);

        if (recursiveLevel >= AIRecursiveLevel)
        {
            //Debug.Log("cannot stop");
            return 100000;
        }
        int  score = AlgorithmUtil.CheckAllPlayersWin(data, yourSign, opponentSign, winLEngth);
        if (score==1)
            return 10000;
   
        if (score==-1)
            return -10000;

        //int bestMove =  -1;
        if (possibleMoves.Count == 0)
            return 0;
      
        if (maximizing)
        {
            float best=Mathf.NegativeInfinity;

            foreach (var empty in possibleMoves)
            {
                    data[empty.x, empty.y] = yourSign;

             //  float posValue = AlgorithmUtil.GetPositionValue(empty.x, empty.y, data.GetLength(0),1);
              
                   float eval = runMinMax(data, recursiveLevel + 1, false, alpha, beta);
              //  eval += posValue;
                best = Math.Max(best, eval);//+posValue;                  
                    data[empty.x, empty.y] = 0;    
                
                     alpha = Math.Max(alpha, eval);
           
               
                 if (beta < alpha)
                     break;

            }

           
                
            return best;
        }
        // If this minimizer's move 
        else
        {
            float best = Mathf.Infinity;
            foreach (var empty in possibleMoves)
            {
              //  float posValue = AlgorithmUtil.GetPositionValue(empty.x, empty.y, data.GetLength(0), 1);

                data[empty.x, empty.y] = opponentSign;
                float eval = runMinMax(data, recursiveLevel + 1, true, alpha, beta);
               // eval -= posValue;
                best = Math.Min(best, eval);// -posValue;
            
                data[empty.x, empty.y] = 0;
                beta = Math.Min(beta, eval);
                if (beta < alpha)
                    break;

            }
            
            return best;
        }
    }


    

    void findBestMove(int[,] data)
    {
        float bestVal = Mathf.NegativeInfinity;
         move = Vector2Int.one * -1;
        List<Vector2Int> possibleMoves = AlgorithmUtil.GetPossibleMoves(data);


        if (possibleMoves.Count != 0)
        {
            foreach (var empty in possibleMoves)
            {
                data[empty.x, empty.y] = yourSign;
                float moveVal = minMax(data, 0, false,  Mathf.NegativeInfinity, Mathf.Infinity);
                //
                float posValue = AlgorithmUtil.GetPositionValue(empty.x, empty.y, data.GetLength(0), 1);

                moveVal += posValue;
                data[empty.x, empty.y] = 0;               
              if (moveVal > bestVal)
                {
                    move.x = empty.x;
                    move.y = empty.y;
                    bestVal = moveVal;
                }

            }
        }

    }



    //float minimax()
    //{

    //    return 1;
    //}
    //Vector2Int getHeuristicEvaluation()
    //{

    //    Vector2Int randMove = Vector2Int.one * -1;

    //    return randMove;
    //}



    float minMax(int[,] data, int recursiveLevel, bool maximizing, float alpha, float beta)
    {

        List<Vector2Int> possibleMoves = AlgorithmUtil.GetPossibleMoves(data);

        if (recursiveLevel >= AIRecursiveLevel)
        {
 
            return 0;
        }
        int score = AlgorithmUtil.CheckAllPlayersWin(data, yourSign, opponentSign, winLEngth);
        if (score == 1)
            return 1000;

        if (score == -1)
            return -1000;

        ////int bestMove =  -1;
        if (possibleMoves.Count == 0)
            return 0;

        if (maximizing)
        {
            float best = Mathf.NegativeInfinity;

            for (int i = 0; i < data.GetLength(0); i++)
            {

                for (int j = 0; j < data.GetLength(1); j++)
                {

                    if (data[i, j] == 0)
                    {
                        data[i, j] = yourSign;
                        float eval = minMax(data, recursiveLevel + 1, false, alpha, beta);
                        best = Math.Max(best, eval);
                        data[i, j] = 0;
                        alpha = Math.Max(alpha, eval);

                        if (beta<=alpha)
                            return best; 
                    }
                }
            }         
            return best;
        }
        else
        {
            float best = Mathf.Infinity;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    if (data[i, j] == 0)
                    {
                        data[i, j] = opponentSign;
                        float eval = minMax(data, recursiveLevel + 1, true, alpha, beta);
                        best = Math.Min(best, eval);
                        data[i, j] = 0;
                        beta = Math.Min(beta, eval);
                        if (beta <= alpha)
                            return best;
                    }
                }
            }

            return best;
        }
    }


}
