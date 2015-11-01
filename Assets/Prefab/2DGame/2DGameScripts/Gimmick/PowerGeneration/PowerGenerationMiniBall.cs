using UnityEngine;
using System.Collections;

public class PowerGenerationMiniBall : MonoBehaviour {

	// アニメーション関連
	private Animator PowerGenerationMiniBallAnimator;
	private float AnimationOffset;
	private bool  isInCamera;

	public  float 遅延時間  = 0.0f;
	private float TimeRessTimer = 0.0f;

	// 全部のミニボールが光ったか
	public static bool isAllPowerGenerationMiniBallHeat = false;

	void Start () {

		// アニメーション取得
		PowerGenerationMiniBallAnimator = GetComponent (typeof(Animator)) as Animator;
		
		// 各ボールが光るタイミング調整
		TimeRessTimer = 遅延時間;
	}

	void Update () {

		//--- カメラに映っているかつ、時間加速かつフルパワー稼働していればアニメーション動作 ---//
		if (PlayerUseAbility.isChronoHigh && isInCamera && PowerGenerationBall.isFullPowerGeneration) {
			if( AnimationOffset >= 0.96f ){
				AnimationOffset  = 0.98f;

				// 全部のミニボールが光った
				if( this.gameObject.name == "PowerGenerationMiniBall05" ){
					isAllPowerGenerationMiniBallHeat = true;
				}

			} else {

				// 全部のミニボールは光っていない
				isAllPowerGenerationMiniBallHeat = false;

				if( TimeRessTimer > 0.0f ){
					// 各ボールが光るタイミング調整
					TimeRessTimer -= Time.deltaTime;
				} else {
					TimeRessTimer = 0.0f;
					AnimationOffset = AnimationOffset + 0.02f;
				}
			}
		} else {

			// 各ボールが光るタイミング調整
			TimeRessTimer = 遅延時間;
			
			// 全部のミニボールは光っていない
			isAllPowerGenerationMiniBallHeat = false;

			if( AnimationOffset <= 0.04f ){
				AnimationOffset  = 0.02f;
			} else {

				AnimationOffset = AnimationOffset - 0.02f;
			}
		}
		// アニメーションのコマ反映
		PowerGenerationMiniBallAnimator.Play (Animator.StringToHash ("PowerGenerationBall_"), 0, AnimationOffset);
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
