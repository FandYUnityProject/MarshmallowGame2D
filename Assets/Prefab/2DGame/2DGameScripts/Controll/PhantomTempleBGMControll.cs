using UnityEngine;
using System.Collections;

public class PhantomTempleBGMControll : MonoBehaviour {
	
	void Awake() {
		Debug.Log ("Application.loadedLevelName: " + Application.loadedLevelName);
		//--- シーン遷移先が"Event01", "PhantomTemple"ならオブジェクトを消滅させない ---//
		if (Application.loadedLevelName == "PhantomTemple" || Application.loadedLevelName == "Event01" ) {
			DontDestroyOnLoad (this.gameObject);
		}
	}
}
