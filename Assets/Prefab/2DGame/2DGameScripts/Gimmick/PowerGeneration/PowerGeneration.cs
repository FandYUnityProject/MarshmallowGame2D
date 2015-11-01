using UnityEngine;
using System.Collections;

public class PowerGeneration : MonoBehaviour {

	// アニメーション関連
	private Animator PowerGenerationAnimator;
	private float AnimationOffset;
	private bool  isInCamera;

	void Start () {
		// アニメーション取得
		PowerGenerationAnimator = GetComponent (typeof(Animator)) as Animator;
	}

	void Update () {

		// カメラに映っているかつ、時間加速していればアニメーション動作
		if (PlayerUseAbility.isChronoHigh && isInCamera) {
			if( AnimationOffset >= 0.96f ){
				// フルパワーで発電している場合
				AnimationOffset  = 0.98f;
			} else {
				AnimationOffset = AnimationOffset + 0.02f;
			}
		} else {
			if( AnimationOffset <= 0.04f ){
				AnimationOffset  = 0.02f;
			} else {
				AnimationOffset = AnimationOffset - 0.02f;
			}
		}
		PowerGenerationAnimator.Play (Animator.StringToHash ("PowerGeneration_"), 0, AnimationOffset);
	}

	void OnBecameVisible (){
		//カメラに映っていれば時間操作によるアニメーション可能
		isInCamera = true;
	}

	void OnBecameInvisible () {
		//カメラに映っていなければ時間操作によるアニメーション無効
		isInCamera = false;
	}
}
