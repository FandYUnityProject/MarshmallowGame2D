using UnityEngine;
using System.Collections;

public class ShurikenControll : MonoBehaviour {

	private int ShurikenCount = 0;
	public  static bool ShurikenCreateOver = false;

	void Update () {

		// 分身能力を装備している場合
		if (PlayerUseAbility.isAvatarUse) {
			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) {
				// 手裏剣があったら手裏剣の数をカウント
				if (obj.name == "Shuriken") {
					ShurikenCount++;
				}
			}


			if( PlayerUseAbility.isAvatarPlayer ){
				// 分身時、手裏剣が６つ以上存在したら表示させない
				if( ShurikenCount >= 6 ){ ShurikenCreateOver = true; }
				else { ShurikenCreateOver = false; }
			} else {
				// 手裏剣が３つ以上存在したら、手裏剣を出させない
				if( ShurikenCount >= 3 ){ ShurikenCreateOver = true; }
				else { ShurikenCreateOver = false; }
			}

			// 手裏剣カウント初期化
			ShurikenCount = 0;
		}
	}
}
