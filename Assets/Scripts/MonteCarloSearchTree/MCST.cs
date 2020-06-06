//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MonteCarlo
{
    public static class Randomizer
    {

        //static Random rand = new Random();

        public static int GetRandIndex( int length)
        {


            return Random.Range(0, length);
        }
        public static int Range(int min, int max)
        {


            return Random.Range(min, max);
        }

}
   

   
    class MCST
    {

    public    static int[,] findNextMove(int[,] data, int player, int opponent, int simulationLevel)
        {
            Node.PlayerWithTurn = player;

            Node root = new Node();
            root.board = AdvancedBoard.CreateBoardCopy(data);
            root.board.CurrentPlayer = opponent;
            root.board.CurrentOpponent = player;
            


            for (int i = 0; i < simulationLevel; i++)
            {
                Node n = getPromisingNode(root);

                n.Expand();
                Node nodeToSimulate = n.getRandomChild();
                int boardResult=    nodeToSimulate.PerformGamePlayout();

                nodeToSimulate.BackPropogation(boardResult);
                // выбрать узел
            // проверить можно ли добавлять и добавить новые узлы
            // выбрать случайно 1
            //симуляция
            //обратное распространение
            }

            Node winner = root.getBestChildByWinScore();

            return winner.board.data;
        }

        static Node getPromisingNode(Node root)
        {

            Node current = root;
            while (current.Children.Count != 0)
            {

                current = current.getBestChildByUTC();
            }

            return current;
        }
     public   static Vector2Int findDifference(int[,] original, int[,] newBoard)
        {

            Vector2Int move = Vector2Int.one * -1;
            for (int i = 0; i < original.GetLength(0); i++)
            {
                for (int j = 0; j < original.GetLength(1); j++)
                {
                    if (original[i, j] != newBoard[i, j])
                    {
                      //  Debug.Log("BOARDS NO E+MATCH" + i + "   " + j + "original[i, j]" + original[i, j] + "   " + newBoard[i, j]);
                        move = new Vector2Int(i, j);
                    }
                }
            }
            return move;
        }
    }
}



//static void Main(string[] args)
//{
//    Console.WriteLine("start");
//    Board.WIN_LENGTH = 3;
//    int[,] field = new int[5, 5];

//    int player = 1;
//    int opponent = 2;
//    int turn = player;
//    for (int i = 0; i < field.GetLength(0) * field.GetLength(1); i++)
//    {
//        if (turn == player)
//            field = findNextMove(field, player, opponent, 1000* field.GetLength(0) * field.GetLength(1));
//        else
//        {
//            int x = Convert.ToInt32(Console.ReadLine());
//            int y = Convert.ToInt32(Console.ReadLine());

//            field[x, y] = opponent;
//        }
//        for (int j = 0; j < field.GetLength(0); j++)
//        {
//            Console.Write($"{j,2}|");

//            for (int k = 0; k < field.GetLength(1); k++)
//            {

//                string sign = (field[j, k] == 1) ? "X" : "O";
//                if (field[j, k] == 0)
//                    sign = ".";

//                Console.Write($"{sign}");
//            }
//            Console.WriteLine();
//        }
//        Console.WriteLine();
//        Console.WriteLine();

//        turn = 3 - turn;
//        //int opp = player;
//        //player = opponent;
//        //opponent = opp;
//    }

//    //for (int sim = 0; sim < 20; sim++)
//    //{


//    //    int[,] field = new int[3, 3];
//    //    for (int j = 0; j < field.GetLength(0); j++)
//    //    {
//    //        Console.Write($"{j,2}|");

//    //        for (int k = 0; k < field.GetLength(1); k++)
//    //        {
//    //            field[j, k] = Randomizer.Range(0, 3);

//    //            string sign = (field[j, k] == 1) ? "X" : "O";
//    //            if (field[j, k] == 0)
//    //                sign = ".";

//    //            Console.Write($"{sign,1}");
//    //        }
//    //        Console.WriteLine();
//    //    }
//    //    AdvancedBoard b = AdvancedBoard.CreateBoardCopy(field);
//    //    // b.WIN_LENGTH = 3;
//    //    b.CurrentPlayer = 1;
//    //    b.CurrentOpponent = 2;


//    //    Console.WriteLine("statis" + b.checkStatus());
//    //    Console.WriteLine();
//    //}
//    // List<Vector2Int> winMoves = b.GetPossibleWinMoves();

//    // foreach (var m in winMoves)
//    // {
//    //     Console.WriteLine("WIN MOVE +" + m.x + "    " + m.y);
//    // }


//}