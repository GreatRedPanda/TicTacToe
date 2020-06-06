using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    class Node
    {
        public static int PlayerWithTurn;
        public  Node parent;
        public List<Node> Children = new List<Node>();
        public Board board;
        int visits;
        int wins;
        int draws;
        double getSuccessPercentage()
        {

            if (visits == 0)
                return 0;// double.MaxValue;

            double success_percentage = (double)(wins) / visits;// s+ 1.41 * Math.Sqrt(Math.Log(parent.visits)*visits) ;
            return success_percentage;
        }
        double uctValue()
        {

            if (visits == 0)
                return double.MaxValue;

            double success_percentage = (double)(wins) / visits + 2 * Math.Sqrt(Math.Log(parent.visits) / visits);
            return success_percentage;
        }
        public     Node getBestChildByUTC()
        {

            Node n = this;

            if (Children.Count == 0)
                return n;
            n = Children[0];
            double max = n.uctValue();
            for (int i = 1; i < Children.Count; i++)
            {

                double newMax = Children[i].uctValue();

                if (newMax > max)
                {
                    max = newMax;
                    n = Children[i];
                }
            }
            return n;
        }
        public Node getBestChildByWinScore()
        {

            Node n = this;

            if (Children.Count == 0)
                return n;
            n = Children[0];
            double max = n.getSuccessPercentage();
            for (int i = 1; i < Children.Count; i++)
            {

                double newMax = Children[i].getSuccessPercentage();

                if (newMax > max)
                {
                    max = newMax;
                    n = Children[i];
                }
            }
            return n;
       }


        public void Expand()
        {

            if (!IsTerminal())
            {

                List<Board> boards = board.GetPossibleMovesBoards(board.CurrentOpponent);

                foreach (var b in boards)
                {
                    Node child = new Node();
                    child.board = b;
                    child.parent = this;
                    child.board.CurrentOpponent = board.CurrentPlayer;
                    child.board.CurrentPlayer = board.CurrentOpponent;
                    Children.Add(child);
                }

            }
        }


        public Node getRandomChild()
        {

            Node n = null;

            if (Children.Count == 0)
                return this;


            int randIndex = Randomizer.Range(0, Children.Count);

            n = Children[randIndex];

            return n;
        }

        public bool IsTerminal()
        {


            int status = board.checkStatus();

            if (status !=0)
                return true;

            return false;
        }

        public int PerformGamePlayout()
        {
            Board b = AdvancedBoard.CreateBoardCopy(board);

            int winner = b.checkStatus();
            while (winner == 0)
            {
                b.TogglePlayer();
                b.makeRandomMove();
                winner= b.checkStatus();

            }
            return winner;
        }

        public void BackPropogation(int winner)
        {


            Node node = this;
            // int prevWins = 0;
            while (node != null)
            {
                node.visits++;

               
                if (node.board.CurrentOpponent == winner)// && winner == PlayerWithTurn)
                {
                    node.wins--;
                
                }
                else
                    node.wins += 1;

             
                node = node.parent;
            }


            //Node node = this;



            //while (node != null)
            //{
            //    if (node.board.CurrentPlayer == winner)// && winner== PlayerWithTurn)
            //    {
            //        node.wins++;
            //    }
            //    else
            //    if (winner == -1)
            //    {
            //        node.draws++;
            //    }

            //    node.visits++;
            //    node = node.parent;

            //}

            //   Node node = this;
            ////  int prevWins = 0;
            //while (node != null)
            //{
            //    node.visits++;
            //    if (PlayerWithTurn == winner || winner == -1)// && winner == PlayerWithTurn)
            //    {
            //        node.wins++;
            //    }
            //    node = node.parent;
            //}

        }
    }
}
