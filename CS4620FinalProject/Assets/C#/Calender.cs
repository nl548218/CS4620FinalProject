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

    public GameObject Days;

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
            NumberButtons();
        }
    }

    private void NumberButtons()
    {
        Transform current = Days.transform.Find("Day");
        current = current.Find("Number");
        TMP_Text number = current.GetComponent<TMP_Text>();
        number.text = "1";
        for (int i = 1; i < 42; i++)
        {
            current = Days.transform.Find("Day ("+ i +")");
            current = current.Find("Number");
            number = current.GetComponent<TMP_Text>();
            number.text = "1";
        }
    }
}
