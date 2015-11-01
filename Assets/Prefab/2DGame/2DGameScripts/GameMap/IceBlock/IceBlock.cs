using UnityEngine;
using System.Collections;

public class IceBlock : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col){
		#region 炎を受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "FireShield") {
			iTween.ScaleTo (gameObject, iTween.Hash ("x", 0,"y", 0, "time", 1.0f, "delay", 0.2f, "oncomplete", "CompleteIceBlockAnime", "easetype", iTween.EaseType.easeInQuad));
		}
		#endregion
	}

	void CompleteIceBlockAnime(){
		Destroy (this.gameObject);
	}
}
