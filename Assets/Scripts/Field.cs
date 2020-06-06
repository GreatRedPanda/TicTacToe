using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonteCarlo;
public class Field : MonoBehaviour
{
    public int FieldSize;
    public float Gap;

     PlayerBase PlayerTic;
     PlayerBase PlayerToe;


    public FieldCell FieldCellPrefab;
    public GameObject XPrefab;
    public GameObject OPrefab;

    public LineRenderer WinLine;


    Board origina;
    int PlayerTurn = 0;

    Vector3 cellSize;

    bool isWin = false;
    int cellsTaken = 0;
    List<FieldCell> cells = new List<FieldCell>();
    List<GameObject> xoObjects = new List<GameObject>();



    public int WinLength {
        get {
            switch (FieldSize)
            {
                case 3: return 3;
                case 5: return 4;
                case 6: return 4;
                case 4: return 4;
                default: return 3;
                    //if (FieldSize > 3)
                    //    return FieldSize-1;
                    //else return 3;
            }
        }

         }
    private void Start()
    {
        cellSize =FieldCellPrefab.GetComponent<Renderer>().bounds.size;// *FieldCellPrefab.transform.localScale;

        cellSize += Vector3.one * Gap;
        cellSize.y *= FieldCellPrefab.transform.localScale.y;
        //AlgorithmUtil.WIN_LENGTH = 3;
        //Board.CellSize = cellSize;
    }



    public void StartGame(int size,  PlayerBase playerTic, PlayerBase playerToe)
    {
        FieldSize = size;

        origina = new AdvancedBoard();
        origina.data = new int[FieldSize, FieldSize];
        origina.CurrentPlayer = 1;
        origina.CurrentOpponent = 2;
        generateField();
        MonteCarlo.Board.WIN_LENGTH = WinLength;

        PlayerTic = playerTic;
        PlayerToe= playerToe;


        AiPlayer aiPlayerTic = playerTic as AiPlayer;
        AiPlayer aiPlayerToe = playerToe as AiPlayer;

        //if (aiPlayerTic != null)
        //    aiPlayerTic.winLEngth = WinLength;
        //if (aiPlayerToe != null)
        //    aiPlayerToe.winLEngth = WinLength;
        PlayerTic.OnMadeMove += onPlayerMadeMove;
        PlayerToe.OnMadeMove += onPlayerMadeMove;

        PlayerTurn = 1;
        PlayerTic.PassMove(origina.data, 1, 2);
        
    }


    public void Clear()
    {
        PlayerTic.OnMadeMove -= onPlayerMadeMove;
        PlayerToe.OnMadeMove -= onPlayerMadeMove;
        WinLine.positionCount = 0;
        cellsTaken = 0;
        foreach (var item in cells)
        {
            Destroy(item.gameObject);
        }
   
        foreach (var item in xoObjects)
        {
            Destroy(item.gameObject);
        }
     cells.Clear();

        xoObjects.Clear();
    }
    

    void generateField()
    {
        isWin = false;

        for (int i = 0; i < FieldSize; i++)
        {
            for (int j = 0; j < FieldSize; j++)
            {
                FieldCell cell = Instantiate(FieldCellPrefab);

                cells.Add(cell);
                cell.transform.position = new Vector3(i*(cellSize.x), 0, j*(cellSize.z));
                cell.PosX = i;
                cell.PosY = j;
            }
        }


    }

  void  onPlayerMadeMove(PlayerBase player, int  x, int  y)
    {
        //Debug.Log("Cannot make move"+player+ "x "+x+"   "+y);

        if (x == -1 && y == -1  || isWin)
        {
            return;
        }

        PlayerBase winner = null;
       

        Vector2Int startWinPos = Vector2Int.one * -1;
        Vector2Int endWinPos = Vector2Int.one * -1;
        int status = 0;
        if (player == PlayerTic && PlayerTurn == 1)
        {
            if (x != -1 && y != -1)
            {

                if (origina.data[x, y] == 0)
                {
                    GameObject tic = Instantiate(XPrefab);

                    cellsTaken++;
                    tic.transform.position = new Vector3(x * cellSize.x, cellSize.y / 2, y * cellSize.z);
                    xoObjects.Add(tic);
                    origina.data[x, y] = 1;
                    status = origina.checkStatus(out startWinPos, out endWinPos);

                    isWin = status != 0;

                    if (!isWin)
                    {

                        PlayerTurn = 2;
                        PlayerToe.PassMove(origina.data, 2, 1);
                        return;
                    }
                    //else
                    //{
                    //   if(status==1)
                    //    winner = player;

                    //}
                }
            }
           
        }
         else  if (player == PlayerToe && PlayerTurn == 2)
        {

            if (x != -1 && y != -1)
            {
                if (origina.data[x, y] == 0)
                {
                    GameObject toe = Instantiate(OPrefab);
                    cellsTaken++;
                    toe.transform.position = new Vector3(x * cellSize.x, cellSize.y / 2, y * cellSize.z);
                    xoObjects.Add(toe);
                    origina.data[x, y] = 2;

                     status = origina.checkStatus(out startWinPos, out endWinPos);
                    isWin = status != 0;
                    if (!isWin)
                    {

                        
                        PlayerTurn = 1;
                        PlayerTic.PassMove(origina.data, 1, 2);
                        return;
                    }
                    //else
                    //{
                    //    if (status == 2)
                    //        winner = player;
                    //}
                }
            }           
        }
                    Debug.Log("STATUS AFTER PLAYER X MOVE"+status);

        if (status!=0)
        {

            //int status = 0;

            if (status != -1)
            {
            //    //if (winner is Player)
            //    //    status = 1;
            //    //else
            //    //    status = -1;

                WinLine.positionCount = 2;
                WinLine.SetPositions(new Vector3[] { new Vector3(startWinPos.x * cellSize.x, cellSize.y / 2+0.5f, startWinPos.y * cellSize.z),
                        new Vector3(endWinPos.x * cellSize.x , cellSize.y/2+0.5f , endWinPos.y * cellSize.z ) });
            }
            FindObjectOfType<GameSystem>().Win(status);
        }
    }
}
