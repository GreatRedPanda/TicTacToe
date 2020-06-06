using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public  class Board //: MonoBehaviour
  {

    public int[,] data;

    int boardNumber;
    public static Vector3 CellSize;

    static int boardsCreated = 0;
    public static Board CreateBoard(int size)
    {
        boardsCreated++;
        // GameObject b = new GameObject();
        //Board board = b.AddComponent<Board>();
        Board board = new Board();
        board.data = new int[size, size];
        board.boardNumber = boardsCreated;

        return board;
    }
    public static Board CreateBoardCopy(Board original)
    {
        boardsCreated++;


        // GameObject b = new GameObject();
        // Board board = b.AddComponent<Board>();
        Board board = new Board();
        board.boardNumber = boardsCreated;

        board.data = new int[original.data.GetLength(0), original.data.GetLength(1)];
        Array.Copy(original.data, board.data, original.data.Length);
        return board;
    }

    public static Board CreateBoardCopy(int[,] data)
    {

        boardsCreated++;

        //  GameObject b = new GameObject();
        //Board board = b.AddComponent<Board>();
        Board board = new Board();
        board.boardNumber = boardsCreated;

        board.data = new int[data.GetLength(0), data.GetLength(1)];
        Array.Copy(data, board.data, data.Length);
        return board;
    }
    //private void OnDrawGizmos()
    //{

    //    if (data != null)
    //    {

    //        for (int i = 0; i < data.GetLength(0); i++)
    //        {
    //            for (int j = 0; j < data.GetLength(0); j++)
    //            {
    //                Gizmos.color = Color.gray;
    //                if (data[i, j] == 1)
    //                    Gizmos.color = Color.red;
    //                else if (data[i, j] == 2)
    //                    Gizmos.color = Color.blue;
    //                Gizmos.DrawSphere( new Vector3(i * (CellSize.x), boardNumber*0.2f, j * (CellSize.z)), 0.05f);
                 
    //            }
    //        }

    //    }
        
    //}

}

