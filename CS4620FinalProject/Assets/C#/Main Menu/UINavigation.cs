using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UINavigation : MonoBehaviour
{
    public Canvas StartingCanvas;
    public Canvas SelectionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartingCanvas.enabled = true;
        SelectionCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBtn()
    {
        StartingCanvas.enabled = false;
        SelectionCanvas.enabled = true;
    }
}
