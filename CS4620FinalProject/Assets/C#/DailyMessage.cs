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

public class DailyMessage : MonoBehaviour
{
    public TMP_Text OverallDate;
    public TMP_Text messageTxt;
    public TMP_InputField newMessage;

    private string Message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabMessage()
    {
        Message = "";
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM DAYS WHERE OverallDate = '" + OverallDate.text + "'";
        IDataReader dataReader = dbCommand.ExecuteReader();
        if (dataReader.Read())
        {
            Message = (string)dataReader["Message"];
        }
        messageTxt.text = Message;
        dataReader.Close();
        dbConnection.Close();
    }

    public void PlaceMessage()
    {
        if(!CheckInput())
        {
            return;
        }
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT INTO DAYS VALUES ('" + OverallDate.text + "', '" + newMessage.text + "', 2)";
        IDataReader dataReader = dbCommand.ExecuteReader();
        dataReader.Close();
        dbConnection.Close();
        GrabMessage();
    }

    public static void GrabMessageStart(TMP_Text messageTxt, string OverallDate)
    {
        string dbname = "URI = file:" + SceneManagerADDED.PlayerName + ".sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbname);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM DAYS WHERE OverallDate = '" + OverallDate + "'";
        IDataReader dataReader = dbCommand.ExecuteReader();
        if (dataReader.Read())
        {
            messageTxt.text = (string)dataReader["Message"];
        }
        dataReader.Close();
        dbConnection.Close();
    }

    bool CheckInput()
    {
        messageTxt.text = "";
        string inputTxt = newMessage.text;
        //Inside max length
        if (inputTxt.Length > 100)
        {
            messageTxt.text = "There is a 100 charatcer save limit.";
            return false;
        }
        //Only letters and numbers
        for (int i = 0; i < inputTxt.Length; i++)
        {
            if ((inputTxt[i] >= 48 && inputTxt[i] <= 57) || (inputTxt[i] >= 65 && inputTxt[i] <= 90) || (inputTxt[i] >= 97 && inputTxt[i] <= 122) || (inputTxt[i] == 32 || inputTxt[i] == 46))
            {

            }
            else
            {
                messageTxt.text = "There can only be standard letters and numbers.";
                return false;
            }
        }
        return true;
    }
}