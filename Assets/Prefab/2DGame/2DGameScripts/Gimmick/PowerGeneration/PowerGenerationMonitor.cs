using UnityEngine;
using System.Collections;

public class PowerGenerationMonitor : MonoBehaviour {

	// アニメーション関連
	private Animator PowerGenerationMonitorAnimator;
	private bool  isInCamera;

	// アニメーション関連
	public  int   PowerTimer = 0;
	private float AnimationOffset = 0.3f;

	// 対応するゲート
	public GameObject TargetGateRed;
	public GameObject TargetGateGreen;

	void Start () {
		// アニメーション取得
		PowerGenerationMonitorAnimator = GetComponent (typeof(Animator)) as Animator;	

		// 初期化
		PowerTimer = 0;
		AnimationOffset = 0.3f;
	}

	void Update () {

		//カメラに映っていれば時間操作によるアニメーション可能
		if (isInCamera) {

			PowerTimer++;

			if (PowerGenerationMiniBall.isAllPowerGenerationMiniBallHeat) {

				//--- 全てのミニボールが光っていたら、徐々に電力ゲージを上げていく ---//
				if (PowerTimer >= 40) {
					PowerTimer = 0;
					if (AnimationOffset < 0.9f) {
						AnimationOffset = AnimationOffset + 0.1f;
					} else {
						// 電力がMAXになったら、赤いゲートを開く
						iTween.MoveTo (TargetGateRed.gameObject.transform.FindChild ("PowerGenerationGateRedTop").gameObject, iTween.Hash ("y", -2.88f, "time", 0.3f, "delay", 0.1f, "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
						iTween.MoveTo (TargetGateRed.gameObject.transform.FindChild ("PowerGenerationGateRedBottom").gameObject, iTween.Hash ("y", -6.72f, "time", 0.3f, "delay", 0.1f, "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
					}
				}
			} else if (PowerGenerationBall.isEmptyPowerGeneration) {

				//--- 現在パワー０で発電機が動いていたら、徐々に更に電力を下げる ---//
				if (PowerTimer >= 40) {
					PowerTimer = 0;
					if (AnimationOffset > 0.1f) {
						AnimationOffset = AnimationOffset - 0.1f;
					} else {
						// 電力が0になったら、緑のゲートを開く
						iTween.MoveTo (TargetGateGreen.gameObject.transform.FindChild ("PowerGenerationGateGreenTop").gameObject, iTween.Hash ("y", -2.88f, "time", 0.3f, "delay", 0.1f, "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
						iTween.MoveTo (TargetGateGreen.gameObject.transform.FindChild ("PowerGenerationGateGreenBottom").gameObject, iTween.Hash ("y", -6.72f, "time", 0.3f, "delay", 0.1f, "islocal", true, "easetype", iTween.EaseType.easeInOutQuad));
					}
				}
			} else {

				//--- それ以外は0.3に近づいていく ---//
				if (PowerTimer >= 10) {
					PowerTimer = 0;
					if (AnimationOffset > 0.3f) {
						AnimationOffset = AnimationOffset - 0.1f;
					} else if (AnimationOffset < 0.3f) {
						AnimationOffset = AnimationOffset + 0.1f;
					}
				}
			}

			// アニメーションのコマ反映
			PowerGenerationMonitorAnimator.Play (Animator.StringToHash ("PowerGenerationMonitor_"), 0, AnimationOffset);
		}
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
