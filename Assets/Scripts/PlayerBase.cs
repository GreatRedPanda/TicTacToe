using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class PlayerBase: MonoBehaviour 
{
    public abstract void PassMove(int[,] data, int playerSign, int opponentSign);

   public  abstract   event System.Action<PlayerBase, int, int> OnMadeMove;


    public int PlayerSign;
}
