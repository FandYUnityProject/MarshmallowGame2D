using UnityEngine;
using System.Collections;

public class WorldTimerControll : MonoBehaviour {

	
	public static float WorldTimer = 0.0f;
	public static float WorldSpeed = 1.0f;

	void Update () {

		// セーブ中以外は常に時間を動かす
		if (!SavePoint.isSaving) {
			WorldTimer += Time.deltaTime * WorldSpeed;
			if (WorldTimer > 100000.0f) {
				WorldTimer = 0;
			}
		}
	}
}
