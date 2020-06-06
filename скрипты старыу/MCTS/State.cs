
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.MCTS
{
    class State
    {


        public Board Board;
        public static int WinCondition;
       
        public   int player;
        public int opponent;

        public  int VisitCount;
        public float WinScore;

        //copy constructor, getters, and setters
        public State( )
        { }
        public State(State source)
        {
            this.player = source.player;
            this.opponent = source.opponent;

            this.VisitCount = source.VisitCount;

            this.WinScore = source.WinScore;
            Board = Board.CreateBoardCopy(source.Board);

        }
        public List<State> getAllPossibleStates()
        {
            List<State> empty = new List<State>();



            List<int[,]> boards=        AlgorithmUtil.GetPossibleMovesBoards(Board.data, opponent);

           // Debug.Log(boards.Count);
            foreach (var b in boards)
            {
                State s = new State();
                s.player = opponent;
                s.opponent = player;
               // Debug.Log("new board" + b);
                s.Board = Board.CreateBoardCopy(b);
                empty.Add(s);
            }

            return empty;
            //constructs a list of all possible states from current state
        }

        public bool randomPlay()
        {
            List<Vector2Int> movesWin = AlgorithmUtil.GetPossibleWinMoves(Board.data, player, WinCondition);

            if (movesWin.Count != 0)
            {
                int randWin = UnityEngine.Random.Range(0, movesWin.Count);

                Vector2Int randWinMove = movesWin[randWin];           
                if (randWinMove != Vector2Int.one * -1)

                {
                    Board.data[randWinMove.x, randWinMove.y] = player;
                    return true;
                }
            }
            List<Vector2Int> moves = AlgorithmUtil.GetPossibleMoves(Board.data);
            if (moves.Count == 0)
                return false;
            int rand = UnityEngine.Random.Range(0, moves.Count);

            Vector2Int randMove = moves[rand];

            Board.data[randMove.x, randMove.y] = player;
            return true;
        }
        public void incrementVisit()
        {

            VisitCount++;
        }

        public void addScore(int score)
        {
            if (WinScore != int.MinValue)
                WinScore += score;
        }

        public void togglePlayer()
        {
            int pla = player;

            int opp = opponent;
            opponent = pla;
            player = opp;


        }


    }
}
