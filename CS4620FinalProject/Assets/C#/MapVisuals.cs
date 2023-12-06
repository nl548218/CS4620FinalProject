using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class MapVisuals : MonoBehaviour
{
    public TMP_Text moneyTxt;
    public TMP_Text currentDateTxt;
    string dbname;


    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
        UpdateDate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateMoney()
    {
        moneyTxt.text = "$" + DataBaseGrabInt("Money");
    }

    private void UpdateDate()
    {
        currentDateTxt.text = DataBaseGrab("DayLabel") + " " + DataBaseGrabInt("MonthNumber") + "/" + DataBaseGrabInt("Day") + "/" + DataBaseGrabInt("Year");
    }
    private string DataBaseGrab(string column)
    {
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM CURRENTSTAT";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        string grabbedValue = (string)dataReader[column];
        dataReader.Close();
        dbConnection.Close();
        return grabbedValue;
    }

    private int DataBaseGrabInt(string column)
    {
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM CURRENTSTAT";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        int grabbedValue = (int)dataReader[column];
        dataReader.Close();
        dbConnection.Close();
        return grabbedValue;
    }

    private void DataBaseUpdate(string column, string newValue)
    {
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "UPDATE CURRENTSTAT SET " + column + " = '" + newValue + "'";
        dbCommand.ExecuteReader();
        dbConnection.Close();
    }

    public void NextDay()
    {
        int Day = DataBaseGrabInt("Day");
        int MonthNumber = DataBaseGrabInt("MonthNumber");
        int Year = DataBaseGrabInt("Year");
        string DayLabel = DataBaseGrab("DayLabel");
        //Leap year
        if ((Year%4)==0 && MonthNumber == 2) { 
            if(Day == 29)
            {
                MonthNumber = 3;
                Day = 1;
            }
            else
            {
                Day++;
            }
        }
        else if (Day == Calender.daysInMonth[MonthNumber-1])
        {
            Day = 1;
            if(MonthNumber == 12)
            {
                MonthNumber = 1;
                Year++;
            }
            else
            {
                MonthNumber++;
            }
        }
        else
        {
            Day++;
        }
        for(int i = 0; i < Calender.dayText.Length; i++)
        {
            if(Calender.dayText[i] == DayLabel)
            {
                if(i == Calender.dayText.Length-1)
                {
                    DayLabel = Calender.dayText[0];
                    i = 9; // end
                }
                else
                {
                    DayLabel = Calender.dayText[i+1];
                    i = 9; // end
                }
            }
        }
        DataBaseUpdate("Day", Day.ToString());
        DataBaseUpdate("MonthNumber", MonthNumber.ToString());
        DataBaseUpdate("Year", Year.ToString());
        DataBaseUpdate("DayLabel", DayLabel);
        //Non-numeric changes
        UpdateMoney();
        UpdateDate();
    }
}
