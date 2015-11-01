using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverSceneControll : MonoBehaviour {

	private Image  BlackOutImage;	// 画面暗転用イメージ

	void Start(){	
		BlackOutImage = GameObject.Find ("BlackOut").GetComponent<Image> ();

		// 画面暗転アニメーション
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 0.3f, "delay", 0.5f, "onUpdate", "UpdateGameOverSceneAnimation"));
	}

	void UpdateGameOverSceneAnimation(float ImageAlpha){
		// 徐々に画面暗転/明るくする
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}

	void Update () {
		if (Input.GetButtonDown ("Submit")) {
			// スタートボタンを押したらセーブデータ選択画面に移動
			Application.LoadLevel("SaveData");
		}
	}

}
