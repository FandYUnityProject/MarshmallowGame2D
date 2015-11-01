using UnityEngine;
using System.Collections;

public class PowerGenerationHeater : MonoBehaviour {

	// アニメーション関連
	private Animator PowerGenerationHeaterlAnimator;
	private float AnimationOffset;
	private bool  isInCamera;

	void Start () {
		// アニメーション取得
		PowerGenerationHeaterlAnimator = GetComponent (typeof(Animator)) as Animator;	
	}

	void Update () {

		//--- カメラに映っているかつ、時間加速かつフルパワー稼働していればアニメーション動作 ---//
		if (PlayerUseAbility.isChronoHigh && isInCamera && PowerGenerationBall.isFullPowerGeneration) {
			if( AnimationOffset >= 0.96f ){
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
		
		// アニメーションのコマ反映
		PowerGenerationHeaterlAnimator.Play (Animator.StringToHash ("PowerGenerationHeater_"), 0, AnimationOffset);
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
