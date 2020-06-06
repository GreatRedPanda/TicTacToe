using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{



    public GameObject PreviousPanel;

    public GameObject CurrentPanel;


    public void Back()
    {
        CurrentPanel.gameObject.SetActive(false);
        PreviousPanel.gameObject.SetActive(true);
    }


    public void SetPreviousPanel(GameObject panel)
    {

        PreviousPanel = panel;
    }
}
