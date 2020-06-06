using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSystem : MonoBehaviour
{

    public PlayerBase player;
    public PlayerBase aiPlayer;

    public int Size;
    public int Turn;

    public Field Field;

    public RectTransform WinLosePanel;
    public RectTransform GamePanel;

    public TMPro.TextMeshProUGUI WinText;
    public TMPro.TextMeshProUGUI TargetWinMovesText;


    private void Start()
    {
        List<Vector2Int> m = new List<Vector2Int>();


        m.Add(new Vector2Int(3, 4));


        Debug.Log(m.Contains(new Vector2Int(3,4)));
    }
    public void SetSize(int size)
    {

        Size = size;
    }


    public void SetTurn(int turn)
    {

        Turn = turn;

    }


    public void StartGame()
    {
        if(Turn==1)
        Field.StartGame(Size, player, aiPlayer);
        else
        Field.StartGame(Size, aiPlayer, player);

        string sign = (Turn == 1) ? "X" : "O";

        TargetWinMovesText.text = "Make " + Field.WinLength + " " +sign + " in line to win";
    }
    public void Restart()
    {
        Field.Clear();
      //  Field.StartGame(Size, Turn);
    }


    public void Win( int status)
    {

      //  Debug.Log("STATUE" +status);
        WinLosePanel.transform.parent.gameObject.SetActive(true);
        WinLosePanel.gameObject.SetActive(true);

        GamePanel.transform.parent.gameObject.SetActive(false);
        GamePanel.gameObject.SetActive(false);
        if (status == -1)
            WinText.text = "It's a draw";
        else if(status==Turn)
            WinText.text = "You win";
        else  if(status!=Turn)
            WinText.text = "You lost";

    }


    public void OpenURL(string url)
    {

        Application.OpenURL(url);
    }
}
