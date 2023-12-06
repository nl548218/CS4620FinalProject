using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Calender : MonoBehaviour
{
    public Canvas mapChoices;
    public Canvas calenderCanvas;
    public Canvas dayCanvas;
    private int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

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
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        /*dbCommand.CommandText = "SELECT * FROM CURRENTSTAT";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        string currentMonth = (string)dataReader["Month"];
        int currentDay = (int)dataReader["Year"];*/

        dbCommand.CommandText = "SELECT * FROM MONTHS M, CURRENTSTAT C WHERE C.Month=M.Name AND C.Year=M.Year";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        int StartDate = (int)dataReader["StartDate"];
        int monthNumber = (int)dataReader["MonthNumber"];
        int dateValue = 1;
        for (int i = 0; i < 42; i++)
        {
            if(i >= (StartDate-1) && i <= (StartDate+daysInMonth[monthNumber-1]-2))
            {
                Transform current = Days.transform.Find("Day (" + i + ")");
                current = current.Find("Number");
                TMP_Text number = current.GetComponent<TMP_Text>();
                number.text = dateValue.ToString();
                dateValue++;
            }
            else
            {
                Transform current = Days.transform.Find("Day (" + i + ")");
                current = current.Find("Number");
                TMP_Text number = current.GetComponent<TMP_Text>();
                number.text = "";
            }
        }
        dataReader.Close();
        dbConnection.Close();
    }
}
