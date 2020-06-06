using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : PlayerBase
{
    
    public LayerMask FieldMask;

    public override event Action<PlayerBase, int, int> OnMadeMove;

   // bool canMove = true;
    public override void PassMove(int[,] data, int playerSign, int opponentSign)
    {
        //canMove = true;
    }

    void Update()
    {
       // Debug.Log(canMove);

     //
        bool click = Input.GetButton("Fire1");
        bool isOverUI = EventSystem.current.IsPointerOverGameObject();

        if (click && !isOverUI)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5000, FieldMask))
            {

               
                FieldCell fc = hit.transform.GetComponent<FieldCell>();

                if (fc != null)
                {
                   // canMove = false;
                    OnMadeMove?.Invoke(this, fc.PosX, fc.PosY);
                }               
            }
        }
    }
}
