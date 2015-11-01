using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventSceneControll : MonoBehaviour {

	private Image      BlackOutImage;	// 画面暗転用イメージ

	// イベントフラグ管理
	public static bool EventScene = false;
	public  bool isEventScene;
	
	public static bool isTextEnd = true;	// 会話が終了したか

	void Start () {
		// イベントシーンなら画面を明るくする
		EventScene = isEventScene;

		if (isEventScene) {
			BlackOutImage = GameObject.Find ("BlackOut").GetComponent<Image> ();
			// 画面暗転アニメーション
			iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 3.0f, "delay", 0.5f, "onUpdate", "UpdateFadeInAnimation", "onComplete", "CompleteFadeInAnimation"));
		}
	}

	void UpdateFadeInAnimation(float ImageAlpha){
		// 徐々に画面明るくする
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}
}
