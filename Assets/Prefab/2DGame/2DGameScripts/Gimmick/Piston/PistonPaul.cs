using UnityEngine;
using System.Collections;

public class PistonPaul : MonoBehaviour {

	public  float ピストン開始待機時間 = 0.5f;

	public static bool isPistonDeath = false;

	void Start () {
		StartCoroutine ("StartPistonWait");
	}

	private IEnumerator StartPistonWait(){
		yield return new WaitForSeconds (ピストン開始待機時間);
		PaulPressStart ();
	}

	void PaulPressStart(){
		isPistonDeath = true;
		iTween.MoveTo (gameObject, iTween.Hash ("y", -1.34f, "time", 0.2f / WorldTimerControll.WorldSpeed, "delay", 0.3f / WorldTimerControll.WorldSpeed, "oncomplete", "CompletePaulPressStart", "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
	}

	void CompletePaulPressStart(){
		isPistonDeath = false;
		iTween.MoveTo (gameObject, iTween.Hash ("y",  0.44f, "time", 0.2f / WorldTimerControll.WorldSpeed, "delay", 0.3f / WorldTimerControll.WorldSpeed, "oncomplete", "PaulPressStart", "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
	}
}
