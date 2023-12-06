using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
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
