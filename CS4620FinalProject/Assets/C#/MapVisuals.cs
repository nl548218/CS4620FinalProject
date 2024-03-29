using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using static UnityEngine.Random; 

public class MapVisuals : MonoBehaviour
{
    public GameObject clicker; 
    public TMP_Text moneyTxt;
    public TMP_Text currentDateTxt;
    public Image messageDisplay;
    public TMP_Text messageTxt;
    public int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public string[] monthText = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    bool vending; 
    string dbname;


    // Start is called before the first frame update
    void Start()
    {
        UpdateDbMoney(0);
        UpdateMoney();
        UpdateDate();
        UpdateMessage();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoney();
        UpdateDate();
        UpdateMessage();
    }
    public void Click(){
        clicker.SetActive(true); 
    }

// Red 3 vending machines
// yellow 6 vending machines
// Orange 9 vending machines 
// Blue 12 vending machine s
// Green 15 vending machines
// Purple 18 vending machines

    public void vendingMachine(){
        //if(vending == true){
        int randy = Range(40, 60);
        UpdateDbMoney(randy);
        Taxes(randy);
        //}
    }

    private void Taxes(int increase)
    {
        string OverallDate = monthText[(DataBaseGrabInt("MonthNumber")-1)] + " " + daysInMonth[(DataBaseGrabInt("MonthNumber") - 1)] + ", " + DataBaseGrabInt("Year");
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM DAYS WHERE Information = 1 AND OverallDate = '" + OverallDate + "'";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        int currentTaxes = Convert.ToInt32(dataReader["Message"]);
        currentTaxes = currentTaxes - Convert.ToInt32(increase * .3);
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "Update DAYS SET Message = '" + currentTaxes + "' WHERE OverallDate = '" + OverallDate + "' AND Information = 1";
        dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        dataReader.Close();
        dbConnection.Close();
    }

    public void moneyLoss(int payment){

        UpdateDbMoney(payment);
    }

    private void UpdateMoney()
    {
        moneyTxt.text = "$" + DataBaseGrabInt("Money");
    }

    private void UpdateMessage()
    {
        string OverallDate = DataBaseGrab("Month") + " " + DataBaseGrabInt("Day") + ", " + DataBaseGrabInt("Year");
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM DAYS";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            string grabbedValue = (string)dataReader["OverallDate"];
            if(OverallDate == grabbedValue)
            {
                if((int)dataReader["Information"] == 2)
                {
                    messageDisplay.enabled = true;
                    messageTxt.enabled = true;
                    messageTxt.text = (string)dataReader["Message"];
                    dataReader.Close();
                    dbConnection.Close();
                    return;
                }
            }
        }
        dataReader.Close();
        dbConnection.Close();
        messageDisplay.enabled = false;
        messageTxt.enabled = false;
    }

    //Negative changeValue decreases money
    public void UpdateDbMoney(int changeValue)
    {
        //Get Current Value 
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM CURRENTSTAT";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        int grabbedValue = (int)dataReader["Money"];
        dataReader.Close();
        dbConnection.Close();
        //Increase by amount
        grabbedValue = grabbedValue + changeValue;
        dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "UPDATE CURRENTSTAT SET Money = " + grabbedValue;
        dataReader = dbCommand.ExecuteReader();
        dataReader.Close();
        dbConnection.Close();
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

    public void AddYear()
    {
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT MAX(Year) AS MaxYear FROM MONTHS";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        int lastYear = Convert.ToInt32(dataReader["MaxYear"]);
        //New Command
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT StartDate FROM MONTHS WHERE Year = '" + lastYear + "' AND MonthNumber = 12";
        dataReader = dbCommand.ExecuteReader();
        dataReader.Read();
        int startOfDecember = (int)dataReader["StartDate"];

        dbCommand = dbConnection.CreateCommand();
        int[] startDays = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        startDays[0] = (startOfDecember + Calender.daysInMonth[11]) % 7;
        if(startDays[0] == 0)
        {
            startDays[0] = 7;
        }
        for(int i = 1; i < 12; i++)
        {
            startDays[i] = (startDays[i-1] + Calender.daysInMonth[i-1]) % 7;
            if (startDays[i] == 0)
            {
                startDays[i] = 7;
            }
        }
        dbCommand.CommandText = "INSERT INTO MONTHS VALUES('January', " + (lastYear + 1) + ", " + startDays[0] + ", 1), ('February', " + (lastYear + 1) + ", " + startDays[1] + ", 2), ('March', " + (lastYear + 1) + ", " + startDays[2] + ", 3), ('April', " + (lastYear + 1) + ", " + startDays[3] + ", 4), ('May', " + (lastYear + 1) + ", " + startDays[4] + ", 5), ('June', " + (lastYear + 1) + ", " + startDays[5] + ", 6), ('July', " + (lastYear + 1) + ", " + startDays[6] + ", 7), ('August', " + (lastYear + 1) + ", " + startDays[7] + ", 8), ('September', " + (lastYear + 1) + ", " + startDays[8] + ", 9), ('October', " + (lastYear + 1) + ", " + startDays[9] + ", 10), ('November', " + (lastYear + 1) + ", " + startDays[10] + ", 11), ('December', " + (lastYear + 1) + ", " + startDays[11] + ", 12)";
        dataReader = dbCommand.ExecuteReader();
        //Bill Time
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT INTO DAYS VALUES ('January 31, " + (lastYear + 1) + "', '0', 1), ('February 28, " + (lastYear + 1) + "', '0', 1), ('March 31, " + (lastYear + 1) + "', '0', 1), ('April 30, " + (lastYear + 1) + "', '0', 1), ('May 31, " + (lastYear + 1) + "', '0', 1), ('June 30, " + (lastYear + 1) + "', '0', 1), ('July 31, " + (lastYear + 1) + "', '0', 1), ('August 31, " + (lastYear + 1) + "', '0', 1), ('September 30, " + (lastYear + 1) + "', '0', 1), ('October 31, " + (lastYear + 1) + "', '0', 1), ('November 30, " + (lastYear + 1) + "', '0', 1), ('December 31, " + (lastYear + 1) + "', '0', 1)";
        dataReader = dbCommand.ExecuteReader();

        //Close
        dataReader.Close();
        dbConnection.Close();
    }

    private void CheckForBills()
    {
        int grabbedValue = 0;
        string OverallDate = DataBaseGrab("Month") + " " + DataBaseGrabInt("Day") + ", " + DataBaseGrabInt("Year");
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM DAYS";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            if((string)dataReader["OverallDate"] == OverallDate && (int)dataReader["Information"] == 1)
            {
                grabbedValue = Convert.ToInt32(dataReader["Message"]);
            }
        }

        if(grabbedValue != 0)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM CURRENTSTAT";
            dataReader = dbCommand.ExecuteReader();
            dataReader.Read();
            grabbedValue = grabbedValue + Convert.ToInt32(dataReader["Money"]);
            //Update
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "UPDATE CURRENTSTAT SET Money = '" + grabbedValue + "'";
            dataReader = dbCommand.ExecuteReader();
        }
        //Close
        dataReader.Close();
        dbConnection.Close();
    }

    private void moneyMade()
    {
        dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT SUM(VendingAmount) AS VM FROM OWNEDBUILDINGS";
        IDataReader dataReader = dbCommand.ExecuteReader();
        if (dataReader.Read())
        {
            int totalVending = Convert.ToInt32(dataReader["VM"]);
            //Close
            dataReader.Close();
            dbConnection.Close();
            for (int i = 0; i < totalVending; i++)
            {
                vendingMachine();
            }
        }
       
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
        if(MonthNumber == 6 && Day == 1)
        {
            AddYear();
        }
        DataBaseUpdate("Day", Day.ToString());
        DataBaseUpdate("MonthNumber", MonthNumber.ToString());
        DataBaseUpdate("Year", Year.ToString());
        DataBaseUpdate("DayLabel", DayLabel);
        moneyMade();
        CheckForBills();
        //Non-numeric changes
        UpdateMoney();
        UpdateDate();
        UpdateMessage();
    }
}
