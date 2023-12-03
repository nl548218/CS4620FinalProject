using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;
using Mono.Data.Sqlite;

public class UINavigation : MonoBehaviour
{
    private string dbName = "URI=file:saves.sqlite";
    public GameObject container;
    public GameObject LoadItem;
    public RectTransform containerTransform;

    public Canvas StartingCanvas;
    public Canvas SelectionCanvas;
    public Canvas NewGameCanvas;
    public Canvas LoadSaveCanvas;
    public Canvas SettingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartingCanvas.enabled = true;
        SelectionCanvas.enabled = false;
        NewGameCanvas.enabled = false;
        LoadSaveCanvas.enabled = false;
        SettingsCanvas.enabled = false;
    }

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            dbGrab();
            resizeContainer();
        }*/
    }

    public void ClickBtn()
    {
        StartingCanvas.enabled = false;
        SelectionCanvas.enabled = true;
    }

    public void ReturnBtn()
    {
        SelectionCanvas.enabled = true;
        NewGameCanvas.enabled = false;
        LoadSaveCanvas.enabled = false;
        SettingsCanvas.enabled = false;
    }

    public void NewGameBtn()
    {
        SelectionCanvas.enabled = false;
        NewGameCanvas.enabled = true;
    }

    public void LoadSaveBtn()
    {
        SelectionCanvas.enabled = false;
        LoadSaveCanvas.enabled = true;
        DeleteChildren(container.transform);
        dbGrab();
        //resizeContainer();
    }

    public void SettingsBtn()
    {
        SelectionCanvas.enabled = false;
        SettingsCanvas.enabled = true;
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    private void dbGrab()
    {
        IDbConnection dbConnection = new SqliteConnection(dbName);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM SAVENAMES";
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            Transform childAtIndex = LoadItem.transform.GetChild(1);
            TMP_Text textComponent = childAtIndex.GetComponent<TMP_Text>();
            textComponent.text = (string)dataReader["Save"];
            Instantiate(LoadItem, container.transform);
        }
        dataReader.Close();
        dbConnection.Close();
    }

    public void deleteFromTable(TMP_Text deleteName)
    {
        string deleteValue = deleteName.text;
        DbCommand("DELETE FROM SAVENAMES WHERE Save='" + deleteValue + "'");
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
    void DeleteChildren(Transform parent)
    {
        // Iterate through all children and destroy them
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    /*void resizeContainer()
    {
        Vector2 containerHeight = containerTransform.anchoredPosition;
        containerHeight.y = 320f+(3)*2000f;
        containerTransform.anchoredPosition = containerHeight;
    }*/
}
