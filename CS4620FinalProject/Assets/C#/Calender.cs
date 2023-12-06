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

    public TMP_Text MonthName;

    private int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private int StartDate;
    private int MonthValue;
    private int Year;
    private string MonthText;

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
            GetCurrentValues();
            NumberButtons();
        }
    }

    private void GetCurrentValues()
    {
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM MONTHS M, CURRENTSTAT C WHERE C.Month=M.Name AND C.Year=M.Year";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        StartDate = (int)dataReader["StartDate"];
        MonthValue = (int)dataReader["MonthNumber"];
        Year = (int)dataReader["Year"];
        MonthText = (string)dataReader["Name"];
        dataReader.Close();
        dbConnection.Close();
    }

    private void NumberButtons()
    {
        MonthName.text = MonthText + " " + Year;
        int dateValue = 1;
        for (int i = 0; i < 42; i++)
        {
            if(i >= (StartDate-1) && i <= ((StartDate-2)+daysInMonth[MonthValue-1]))
            {
                Transform current = Days.transform.Find("Day (" + i + ")");
                Image lighten = current.GetComponent<Image>();
                lighten.color = new Color(1f, 1f, 1f, 1f);
                current = current.Find("Number");
                TMP_Text number = current.GetComponent<TMP_Text>();
                number.text = dateValue.ToString();
                dateValue++;
            }
            else
            {
                Transform current = Days.transform.Find("Day (" + i + ")");
                Image darken = current.GetComponent<Image>();
                darken.color = new Color(0.66f, 0.66f, 0.66f, 1f);
                current = current.Find("Number");
                TMP_Text number = current.GetComponent<TMP_Text>();
                number.text = "";
            }
        }
    }
    public void RightArrow()
    {
        if(MonthValue == 12)
        {
            MonthValue = 1;
            Year++;
        }
        else
        {
            MonthValue++;
        }
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM MONTHS M, CURRENTSTAT C WHERE "+ MonthValue+ "=M.MonthNumber AND "+Year+"=M.Year";
        IDataReader dataReader = dbCommand.ExecuteReader();
        if (!dataReader.Read())
        {
            //Undo
            if (MonthValue == 1)
            {
                MonthValue = 12;
                Year--;
            }
            else
            {
                MonthValue--;
            }
            return;
        }
        StartDate = (int)dataReader["StartDate"];
        MonthValue = (int)dataReader["MonthNumber"];
        Year = (int)dataReader["Year"];
        MonthText = (string)dataReader["Name"];
        dataReader.Close();
        dbConnection.Close();
        NumberButtons();
    }

    public void LeftArrow()
    {
        if(MonthValue == 1 && Year == 2000)
        {
            return;
        }
        if (MonthValue == 1)
        {
            MonthValue = 12;
            Year--;
        }
        else
        {
            MonthValue--;
        }
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM MONTHS M, CURRENTSTAT C WHERE " + MonthValue + "=M.MonthNumber AND " + Year + "=M.Year";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        StartDate = (int)dataReader["StartDate"];
        MonthValue = (int)dataReader["MonthNumber"];
        Year = (int)dataReader["Year"];
        MonthText = (string)dataReader["Name"];
        dataReader.Close();
        dbConnection.Close();
        NumberButtons();
    }
}
