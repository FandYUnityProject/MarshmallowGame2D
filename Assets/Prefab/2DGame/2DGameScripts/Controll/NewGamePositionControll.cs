using UnityEngine;
using System.Collections;

public class NewGamePositionControll : MonoBehaviour {
	
	public static float NewGamePositionX;
	public static float NewGamePositionY;

	void Awake(){
		NewGamePositionX = this.transform.localPosition.x;
		NewGamePositionY = this.transform.localPosition.y;

		Debug.Log ("NewGamePositionX: " + NewGamePositionX + "   NewGamePositionY:" + NewGamePositionY);
	}
}
