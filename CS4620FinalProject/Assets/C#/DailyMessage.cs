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
}
