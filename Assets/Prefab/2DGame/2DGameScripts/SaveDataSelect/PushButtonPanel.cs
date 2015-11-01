using UnityEngine;
using System.Collections;

public class PushButtonPanel : MonoBehaviour {

	public static bool isPushButtonAnimationEnd = false;

	void Start () {
		// PushButton拡大アニメーション
		this.gameObject.transform.localScale = new Vector2 (0, 0);
		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", 1, "y", 1, "time", 0.6f, "delay", 1.1f , "islocal", true, "onComplete", "OnCompletePushButtonAnimationEnd", "easetype", iTween.EaseType.easeOutQuad));
	}

	// アニメーションが終わったらアニメーション終了フラグを立てる
	void OnCompletePushButtonAnimationEnd(){
		if (!SaveScreenUIControll.isGoToSaveData) {
			// セーブデータ選択画面前であれば、”PUSH BUTTON SELECT”を上下にゆっくり動かす
			isPushButtonAnimationEnd = true;
			iTween.MoveTo (this.gameObject, iTween.Hash ("y", -170, "time", 2.0f, "delay", 0.8f, "islocal", true, "onComplete", "OnCompletePushButtonPingPongAnimationEnd", "easetype", iTween.EaseType.easeOutQuad));
		}
	}

	void OnCompletePushButtonPingPongAnimationEnd(){
		if (!SaveScreenUIControll.isGoToSaveData) {
			iTween.MoveTo (this.gameObject, iTween.Hash ("y", -175, "time", 2.0f, "delay", 0.8f, "islocal", true, "onComplete", "OnCompletePushButtonAnimationEnd", "easetype", iTween.EaseType.easeOutQuad));
		}
	}
}
