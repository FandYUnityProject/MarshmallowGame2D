using UnityEngine;
using System.Collections;

public class GameTitlePanel : MonoBehaviour {

	void Start () {
		// タイトルロゴ拡大アニメーション
		this.gameObject.transform.localScale = new Vector2 (0, 0);
		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", 1, "y", 1, "time", 0.6f, "delay", 0.5f , "islocal", true, "easetype", iTween.EaseType.easeOutQuad));
	}
}
