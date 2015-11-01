using UnityEngine;
using System.Collections;

public class IceLanceControll : MonoBehaviour {

	private int IceLanceCount = 0;
	public  static bool IceLanceCreateOver = false;
	
	void Update () {
		
		// 分身能力を装備している場合
		if (PlayerUseAbility.isMagicUse) {
			foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject))) {
				// 手裏剣があったら手裏剣の数をカウント
				if (obj.name == "IceLanceObj") {
					IceLanceCount++;
				}
			}
			
			if( IceLanceCount >= 1 ){ IceLanceCreateOver = true; }
			else { IceLanceCreateOver = false; }
			
			// 手裏剣カウント初期化
			IceLanceCount = 0;
		}
	}
}
