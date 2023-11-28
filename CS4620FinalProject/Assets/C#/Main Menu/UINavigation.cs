using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UINavigation : MonoBehaviour
{
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
}
