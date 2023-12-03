using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Calender : MonoBehaviour
{
    public Canvas mapChoices;
    public Canvas calenderCanvas;
    public Canvas dayCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mapChoices.enabled = true;
        calenderCanvas.enabled = false;
        dayCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenCalender();
        }
    }

    private void OpenCalender()
    {
        if (calenderCanvas.enabled)
        {
            mapChoices.enabled = true;
            calenderCanvas.enabled = false;
            dayCanvas.enabled = false;
        }
        else
        {
            mapChoices.enabled = false;
            calenderCanvas.enabled = true;
            dayCanvas.enabled = false;
        }
    }
}
