using UnityEngine;
using System.Collections;

public class PressSwitchTop : MonoBehaviour {

	public static bool isPressSwitch = false;
	public GameObject LaserBiimObj;

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "GravityBlock" || col.gameObject.name == "Player") {
			iTween.ScaleTo (gameObject, iTween.Hash ("y", 0.4f, "time", 0.3f, "oncomplete", "CompletePressInSwitchTop", "easetype", iTween.EaseType.easeInQuad));
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "GravityBlock" || col.gameObject.name == "Player") {
			iTween.ScaleTo (gameObject, iTween.Hash ("y", 1.0f, "time", 0.3f, "oncomplete", "CompletePressOutSwitchTop", "easetype", iTween.EaseType.easeInQuad));
		}
	}

	void CompletePressInSwitchTop(){
		isPressSwitch = true;
		LaserBiimObj.SetActiveRecursively (false);
	}

	void CompletePressOutSwitchTop(){
		isPressSwitch = false;
		LaserBiimObj.SetActiveRecursively (true);
	}
}
