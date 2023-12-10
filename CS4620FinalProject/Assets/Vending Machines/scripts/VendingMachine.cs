using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachine : MonoBehaviour {

	//This is related with wich object drop, the position and the number of object.
	[SerializeField] GameObject canSpawner;
	[SerializeField] GameObject can;
	[SerializeField] AudioClip canSound;
	[SerializeField] int numberOfCan;

	//This is for the text for explain the use of the machine.
	[SerializeField] Text interactionText;
	[SerializeField] string interactionString;
	[SerializeField] AudioClip audioPip;

	//This is the final text after use the machine.
	[SerializeField] Text canText;
	[SerializeField] string canString;
	[SerializeField] string emptyString;

	AudioSource audioSource;

	void Awake(){
		//Find the audio component and hidding the Texts.
		audioSource = GetComponent<AudioSource> ();
		interactionText.gameObject.SetActive (false);
		canText.gameObject.SetActive (false);
	}

	//When the player approach the machine.
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			//This Show the interaction text.
			interactionText.gameObject.SetActive (true);
			interactionText.text = interactionString;
		}
	}

	//When the player stay near the machine, can interact whit her.
	void OnTriggerStay(Collider col){
		if (col.tag == "Player") {
			if (Input.GetKeyDown (KeyCode.E)) {
				audioSource.PlayOneShot (audioPip);
				DropCan ();
			}
		}
	}

	//when the player left the machine, the texts dissappears.
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			interactionText.gameObject.SetActive (false);
			canText.gameObject.SetActive (false);
		}
	}

	//This is the method for drop de cans.
	void DropCan(){
		if (numberOfCan > 0) {
			interactionText.gameObject.SetActive (false);
			Instantiate (can, canSpawner.transform.position, Quaternion.identity, canSpawner.transform) ;
			numberOfCan--;
			audioSource.PlayOneShot (canSound);
			canText.gameObject.SetActive (true);
			canText.text = canString;
		} else {
			canText.gameObject.SetActive (true);
			canText.text = emptyString;
		}
	}

}
