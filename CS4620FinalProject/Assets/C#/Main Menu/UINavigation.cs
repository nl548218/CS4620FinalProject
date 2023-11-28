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

    // Start is called before the first frame update
    void Start()
    {
        StartingCanvas.enabled = true;
        SelectionCanvas.enabled = false;
        NewGameCanvas.enabled = false;
        LoadSaveCanvas.enabled = false;
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
}
