using UnityEngine;
using System.Collections;

public class PlaySaveDataNumber : MonoBehaviour {

	private static bool isCreated = false;	// シーン遷移時にオブジェクトを消滅させないためのフラグ
	public  static int  NowPlaySaveDataNumber = 0;

	void Awake() {

		// 現在プレイしているセーブデータの番号を表示
		Debug.Log ("SaveDataNumber: " + NowPlaySaveDataNumber);

		//--- シーン遷移時にオブジェクトを消滅させない ---//
		if (!isCreated) {
			isCreated = true;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}
}
