using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;

public class TestDB : MonoBehaviour
{
    public TMP_InputField inputWord;
    public TMP_InputField inputInt;
    public TMP_Text output;

    private string dbName = "URI=file:test.sqlite";

    // Start is called before the first frame update
    void Start()
    {
        CreateDB();

        DisplayDB();
    }

    public void CreateDB()
    {
        DbCommand("CREATE TABLE IF NOT EXISTS TEST (word VARCHAR(30), number INTEGER)");
    }

    public void AddTuple()
    {
        DbCommand("INSERT INTO test(word, number) VALUES('" + inputWord.text + "', " + inputInt.text + ") ");

        DisplayDB();
    }

    public void DisplayDB()
    {
        output.text = "";
        DbCommandRead("SELECT * FROM test");  
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
        private void DbCommandRead(string text)
    {
        IDbConnection dbConnection = new SqliteConnection(dbName);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = text;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            output.text += dataReader["word"] + "\t\t" + dataReader["number"] + "\n";
        }
        dataReader.Close();
        dbConnection.Close();
    }
}
