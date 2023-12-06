using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneManagerADDED : MonoBehaviour
{
    public static string PlayerName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateNameText(TMP_Text SelectedName)
    {
        PlayerName = SelectedName.text;
    }

    public void updateNameIF(TMP_InputField saveName)
    {
        PlayerName = saveName.text;
    }

    public void displayName(TMP_Text SelectedName)
    {
        SelectedName.text = PlayerName;
    }
}
