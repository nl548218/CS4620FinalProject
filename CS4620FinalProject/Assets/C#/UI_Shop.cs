using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    // Start is called before the first frame update
  private Transform container;
  private Transform shopItemTemplate;

private void start(){
    Hide(); 
}

private void awake(){
    container = transform.Find("container"); 
    shopItemTemplate = container.Find("shopItemTemplate");
    shopItemTemplate.gameObject.SetActive(false); 
  }
    

  public void Hide(){
    gameObject.SetActive(false); 
  }
}
