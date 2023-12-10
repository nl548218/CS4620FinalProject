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


public class SaveDB : MonoBehaviour
{
    private string dbName = "URI=file:saves.sqlite";
    public TMP_InputField saveName;
    public TMP_Text errorTxt;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void NewGameBtn()
    {
        CreateDB();
        if (CheckInput())
        {
            DbCommand("INSERT INTO SAVENAMES VALUES('" + saveName.text + "')");
            BuildNewDB("URI = file:" + saveName.text + ".sqlite");
            SceneManager.LoadScene("Map");
        } 
    }

    bool CheckInput()
    {
        errorTxt.text = "";
        string inputTxt = saveName.text;
        //Inside max length
        if(inputTxt.Length > 30)
        {
            errorTxt.text = "There is a 30 charatcer save limit.";
            return false;
        }
        else if (inputTxt == "saves" || inputTxt.Length < 1)
        {
            errorTxt.text = "It can not be this value.";
            return false;
        }
        //Only letters and numbers
        for (int i = 0; i < inputTxt.Length; i++)
        {
            if((inputTxt[i] >= 48 && inputTxt[i] <= 57)||(inputTxt[i] >= 65 && inputTxt[i] <= 90) || (inputTxt[i] >= 97 && inputTxt[i] <= 122))
            {

            }
            else
            {
                errorTxt.text = "There can only be standard letters and numbers.";
                return false;
            }
        }
        //Check if it does not exist
        if(DbCommandRead("SELECT * FROM SAVENAMES"))
        {
            errorTxt.text = "There is already a name with that save.";
            return false;
        }
        return true;
    }

    public void CreateDB()
    {
        DbCommand("CREATE TABLE IF NOT EXISTS SAVENAMES (Save VARCHAR(30))");
    }

    private void DbCommand(string text)
    {
        IDbConnection dbConnection = new SqliteConnection(dbName);
        dbConnection.Open();
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = text;
        dbCommandCreateTable.ExecuteReader();
        dbConnection.Close();
    }

    private void DbCommand(string text, string custumdbname)
    {
        IDbConnection dbConnection = new SqliteConnection(custumdbname);
        dbConnection.Open();
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = text;
        dbCommandCreateTable.ExecuteReader();
        dbConnection.Close();
    }

    bool DbCommandRead(string text)
    {
        IDbConnection dbConnection = new SqliteConnection(dbName);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = text;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            if(saveName.text == (string)dataReader["Save"])
            {
                dataReader.Close();
                dbConnection.Close();
                return true;
            }
        }
        dataReader.Close();
        dbConnection.Close();
        return false;
    }

    public void DBDelete()
    {
        File.Delete(dbName);
    }

    public void BuildNewDB(string newdbname)
    {
        DbCommand("CREATE TABLE IF NOT EXISTS MONTHS (Name VARCHAR(9), Year INT, StartDate INT, MonthNumber INT)", newdbname);
        //1-Bills 2-Usermessage
        DbCommand("CREATE TABLE IF NOT EXISTS DAYS (OverallDate VARCHAR(50), Message VARCHAR(100), Information INT)", newdbname);

        DbCommand("CREATE TABLE IF NOT EXISTS CURRENTSTAT (Money INT, Year INT, Month VARCHAR(9), MonthNumber INT, Day INT, DayLabel VARCHAR(9))", newdbname);

        DbCommand("CREATE TABLE IF NOT EXISTS AVAILABLEBUILDINGS (Cost INT, Name VARCHAR(20), VendingAmount INT)", newdbname);

        DbCommand("CREATE TABLE IF NOT EXISTS OWNEDBUILDINGS (Cost INT, Name VARCHAR(20), VendingAmount INT)", newdbname);

        //Months 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
        DbCommand("INSERT INTO MONTHS VALUES ('January', 2000, 7, 1), ('February', 2000, 3, 2), ('March', 2000, 4, 3), ('April', 2000, 7, 4), ('May', 2000, 2, 5), ('June', 2000, 5, 6), ('July', 2000, 7, 7), ('August', 2000, 3, 8), ('September', 2000, 6, 9), ('October', 2000, 1, 10), ('November', 2000, 4, 11), ('December', 2000, 6, 12)", newdbname);
        DbCommand("INSERT INTO MONTHS VALUES ('January', 2001, 2, 1), ('February', 2001, 5, 2), ('March', 2001, 5, 3), ('April', 2001, 1, 4), ('May', 2001, 3, 5), ('June', 2001, 6, 6), ('July', 2001, 1, 7), ('August', 2001, 4, 8), ('September', 2001, 7, 9), ('October', 2001, 2, 10), ('November', 2001, 5, 11), ('December', 2001, 7, 12)", newdbname);
        //Stats
        DbCommand("INSERT INTO CURRENTSTAT VALUES (20000, 2000, 'January', 1, 1, 'Saturday')", newdbname);

        //Test Bill
        //DbCommand("INSERT INTO DAYS VALUES ('January 31, 2000', '-2000', 1)", newdbname);

        // Red 3 vending machines
        DbCommand("INSERT INTO AVAILABLEBUILDINGS VALUES (5000, 'Red', 3)", newdbname);
        // Yellow 6 vending machines
        DbCommand("INSERT INTO AVAILABLEBUILDINGS VALUES (10000, 'Yellow', 6)", newdbname);
        // Orange 9 vending machines 
        DbCommand("INSERT INTO AVAILABLEBUILDINGS VALUES (15000, 'Orange', 9)", newdbname);
        // Blue 12 vending machines
        DbCommand("INSERT INTO AVAILABLEBUILDINGS VALUES (20000, 'Blue', 12)", newdbname);
        // Green 15 vending machines
        DbCommand("INSERT INTO AVAILABLEBUILDINGS VALUES (25000, 'Green', 15)", newdbname);
        // Purple 18 vending machines
        DbCommand("INSERT INTO AVAILABLEBUILDINGS VALUES (30000, 'Purple', 18)", newdbname);

    }
}
