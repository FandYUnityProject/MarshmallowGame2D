using UnityEngine;
using System.Collections;

public class DestroyPsychoBlock : MonoBehaviour {
	
	public bool isDestroy = false;	// 破壊が終わったタイミングはアニメーションで処理

	void Update(){
		if (isDestroy) {
			iTween.ScaleTo(gameObject, iTween.Hash("x", 100.0f, "y", 0.0f, "time", 0.6f, "delay", 0.3f, "oncomplete", "CompletePsychoBlockDestroyTweenAnime", "easetype", iTween.EaseType.easeInBack));
		}
	}

	void CompletePsychoBlockDestroyTweenAnime(){
		Destroy (this.gameObject);
	}
}
