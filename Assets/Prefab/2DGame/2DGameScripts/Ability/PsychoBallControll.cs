using UnityEngine;
using System.Collections;

public class PsychoBallControll : MonoBehaviour {

	private int        PsychoBallCount    = 0;
	private int        BigPsychoBallCount = 0;
	public static bool PsychoBallCreateOver    = false;
	public static bool BigPsychoBallCreateOver = false;

	void Update () {

		// 念力能力を装備している場合
		if (PlayerUseAbility.isPsychoUse) {
			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) {
				// サイコボールがあったらサイコボールの数をカウント
				if (obj.name == "PsychoBall") {
					PsychoBallCount++;
				}
				// ビッグサイコボールがあったらビッグサイコボールの数をカウント
				if (obj.name == "BigPsychoBall") {
					BigPsychoBallCount++;
				}
			}

			// サイコボールが2つ以上存在したら、サイコボールを出させない
			if( PsychoBallCount >= 2 ){ PsychoBallCreateOver = true; }
			else { PsychoBallCreateOver = false; }
			// サイコボールカウント初期化
			PsychoBallCount = 0;
			
			
			// ビッグサイコボールが2つ以上存在したら、ビッグサイコボールを出させない
			if( BigPsychoBallCount >= 2 ){ BigPsychoBallCreateOver = true; }
			else { BigPsychoBallCreateOver = false; }
			// ビッグサイコボールカウント初期化
			BigPsychoBallCount = 0;
		}
	}
}
