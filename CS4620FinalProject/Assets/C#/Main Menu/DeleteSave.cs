using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class DeleteSave : MonoBehaviour
{
    private string dbName = "URI=file:saves.sqlite";

    public void DeleteSaveName(TMP_Text savename)
    {
        DbCommand("DELETE FROM SAVENAMES WHERE Save='" + savename.text + "'");
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

}
