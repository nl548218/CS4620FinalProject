using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_appear : MonoBehaviour
{


[SerializeField] private Image customImage; 
[SerializeField] private TextMeshProUGUI textDisplay; 
[SerializeField] private Image customImage1; 
void start(){
customImage.enabled = false; 

}

void OnTriggerEnter(Collider other){

    if(other.CompareTag("Player")){
        customImage.enabled = true; 
        customImage1.enabled = true; 
        textDisplay.enabled = true;
    }
}

void OnTriggerExit(Collider other){
    if(other.CompareTag("Player")){
        customImage.enabled = false;
        customImage1.enabled = false; 
        textDisplay.enabled = false;
    }
}
}
