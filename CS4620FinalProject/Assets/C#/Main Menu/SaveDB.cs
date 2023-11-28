using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;


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
            Debug.Log(saveName.text);
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
        //Only letters and numbers
        for(int i = 0; i < inputTxt.Length; i++)
        {
            if((inputTxt[i] >= 49 && inputTxt[i] <= 57)||(inputTxt[i] >= 65 && inputTxt[i] <= 90) || (inputTxt[i] >= 97 && inputTxt[i] <= 122))
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
}
