using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using System.Diagnostics;
namespace Assets.Scripts.MCTS
{
    class MCTreeSearch
    {

        static readonly int WIN__SCORE = 10;

        static int Opponent;
        public static void findNextMove(int[,] board, int player, int opponent, int searchLevel, int winCondition, ref Vector2Int move)
        {
            //define an end time which will act as a terminating condition
            Node.AllCreatedNodes = 0;
            move = Vector2Int.one * -1;
            // Tree tree = new Tree();

            Opponent = opponent;

            State newState = new State();
            newState.Board = Board.CreateBoardCopy(board);

            //if (player == 2)
            //{
            //    newState.player = player;
            //    newState.opponent = opponent;
            //}
            //else
            {

                newState.player = opponent;
               newState.opponent = player;
            }
            State.WinCondition = winCondition;


            Node rootNode = new Node(newState);
          //  tree.root = rootNode;


            for (int i = 0; i < searchLevel; i++)
            {
              
                Node promisingNode = selectPromisingNode(rootNode);

                State promNodeState = promisingNode.GetState();
                int boardStatus = AlgorithmUtil.GetPossibleMoves(promNodeState.Board.data).Count;


                if (boardStatus> 0)
                {
             
                expandNode(promisingNode);
                }
               
                Node nodeToExplore = promisingNode;

                List<Node> choldren = promisingNode.GetChildren();
           
                if (choldren.Count > 0)
                {
               
                nodeToExplore = promisingNode.getRandomChildNode();
                }

                
                int playoutResult = simulateRandomPlayout(nodeToExplore);

                backPropogation(nodeToExplore, playoutResult);
            }

            Node winnerNode = rootNode.getChildWithMaxScore();

            if (winnerNode == null)
                winnerNode = rootNode;
            move= findDifference(board, winnerNode.GetState().Board.data);
        }


        static Vector2Int findDifference(int[,] original, int[,] newBoard)
        {

            Vector2Int move = Vector2Int.one * -1;
            for (int i = 0; i < original.GetLength(0); i++)
            {
                for (int j = 0; j < original.GetLength(1); j++)
                {
                    if (original[i, j] != newBoard[i,j])
                    {
                        Debug.Log("BOARDS NO E+MATCH" +i+"   "+j+ "original[i, j]" + original[i, j]+ "   " + newBoard[i, j]);
                        move=  new Vector2Int(i,j);
                    }
                }
            }
            return move;
        }

        private static void expandNode(Node node)
        {

            State currentState = node.GetState();
            // получаем возможные состояния от состояния в текущем узле
            List<State> possibleStates = currentState.getAllPossibleStates();

            // добавляем в узел новые узлы с этими возможными сотсояниями/ходами
            foreach (var state in possibleStates)
            {
                Node newNode = new Node(state);
                state.player = currentState.opponent;
                state.opponent = currentState.player;
                newNode.parent = node;
                node.AddChild(newNode);
                //UnityEngine.Debug.Log("creating children nodes " + state.Board);
            }
                                    
        }




        private static Node selectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
           while(node.GetChildren().Count!=0)
            { 
         
                node = UTC.findBestNodeWithUCT(node);
            }
            return node;
        }



        private static void backPropogation(Node nodeToExplore, int playerNo)
        {
            Node tempNode = nodeToExplore;
            while (tempNode != null)
            {

                State s = tempNode.GetState();
                 s.incrementVisit();
                if (s.player == playerNo)
                {
                    s.addScore(WIN__SCORE);
                }
                tempNode = tempNode.parent;
            }
        }



        private static int simulateRandomPlayout(Node node)
        {
            Node tempNode  = node;

            State tempState =new State( tempNode.GetState());
            //int player = tempState.player;
            //int opponent = tempState.opponent;

            //bool haveEmptyPlaces = tempState.randomPlay();


            int boardStatus = AlgorithmUtil.CheckAllPlayersWin(tempState.Board.data, 
                tempState.player,
                tempState.opponent, State.WinCondition);


            if (boardStatus == Opponent)
            {

                //UnityEngine. Debug.Log("only parent" + node + "   " + node.GetChildren().Count);

                if(tempNode.parent!=null)
                  tempNode.parent.GetState().WinScore=(int.MinValue);
                //else
                //tempState.WinScore = (int.MinValue);
                return boardStatus;
            }
            while (boardStatus == 0)
            {
                tempState.togglePlayer();
              bool   haveEmptyPlaces = tempState.randomPlay();

                boardStatus = AlgorithmUtil.CheckAllPlayersWin(tempState.Board.data,
                tempState.player,
                tempState.opponent, State.WinCondition);

                  if (!haveEmptyPlaces)
                    break;
            }
            return boardStatus;
        }

    }
}
