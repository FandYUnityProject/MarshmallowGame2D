using UnityEngine;
using System.Collections;

public class PowerGenerationBall : MonoBehaviour {

	// アニメーション関連
	private Animator PowerGenerationBallAnimator;
	private float AnimationOffset;
	private bool  isInCamera;

	// 円運動関連
	private float StartPositionX;
	private float StartPositionY;
	private float 円運動速度 = -30.0f;
	private float 円運動半径 =   0.75f;

	// ボールの回転角度
	private float BallRotate = 360.0f;

	// 時間減速から復帰後のボールの位置調整用変数
	private float PowerGenerationTimer = 0.0f;
	private float isCheckBallPositionX = 0.0f;
	private float isCheckBallPositionY = 0.0f;
	private bool  isStopBall         = false;
	
	// 現在フルパワーで発電機が動いているか
	public static bool isFullPowerGeneration = false;
	// 現在パワー０で発電機が動いているか
	public static bool isEmptyPowerGeneration = false;

	void Start () {

		// アニメーション取得
		PowerGenerationBallAnimator = GetComponent (typeof(Animator)) as Animator;

		// 初期位置取得
		StartPositionX = this.transform.position.x;
		StartPositionY = this.transform.position.y;

		isFullPowerGeneration  = false;
		isEmptyPowerGeneration = false;
	}

	void Update () {

		//--- カメラに映っているかつ、時間加速していればアニメーション動作 ---//
		if (PlayerUseAbility.isChronoHigh && isInCamera) {
			if( AnimationOffset >= 0.96f ){
				// フルパワーで発電している場合
				AnimationOffset  = 0.98f;
				isFullPowerGeneration = true;
			} else {
				AnimationOffset = AnimationOffset + 0.02f;
			}
		} else {
			
			// フルパワーで発電していない場合
			isFullPowerGeneration = false;

			if( AnimationOffset <= 0.04f ){
				AnimationOffset  = 0.02f;
			} else {
				AnimationOffset = AnimationOffset - 0.02f;
			}
		}
		// アニメーションのコマ反映
		PowerGenerationBallAnimator.Play (Animator.StringToHash ("PowerGenerationBall_"), 0, AnimationOffset);


		//--- ボールの回転角度 ---//
		if( BallRotate > 0.0f ) {
			BallRotate = BallRotate - ( 0.1f * WorldTimerControll.WorldSpeed);
		} else {
			BallRotate = 360.0f;
		}

		//--- ボールの円運動タイマー（オーバーフロー防止） ---//
		PowerGenerationTimer +=  Time.deltaTime * WorldTimerControll.WorldSpeed;
		if (PowerGenerationTimer > 100000.0f) {
			PowerGenerationTimer = 0;
		}

		//--- 時間減速能力を使っていない間は回り続ける ---//
		if (!PlayerUseAbility.isChronoSlow) {

			// パワー０ではない
			isEmptyPowerGeneration = false;

			//--- ボールが停止（一定以上時が減速）してから復帰後、再開位置に合わせて円運動タイマーを設定し直す ---//
			if( isStopBall ){
				// 再開地点の座標取得
				isCheckBallPositionX = this.transform.localPosition.x;
				isCheckBallPositionY = this.transform.localPosition.y;

				// 再開地点から、現在の円運動タイマーを設定し直す
				if( (isCheckBallPositionX > 0 && isCheckBallPositionX <= 0.770f ) && (isCheckBallPositionY <= 0 && isCheckBallPositionX > -0.770f )){
					PowerGenerationTimer = 0.165f - (( isCheckBallPositionX / 0.770f ) *  0.165f);
				} else if( (isCheckBallPositionX <= 0 && isCheckBallPositionX > -0.770f ) && (isCheckBallPositionY >= -0.770f && isCheckBallPositionX < 0 )){
					PowerGenerationTimer = (( isCheckBallPositionX / 0.770f ) * -0.165f) + 0.165f;
				} else if( (isCheckBallPositionX >= -0.770f && isCheckBallPositionX < 0 ) && (isCheckBallPositionY >=  0 && isCheckBallPositionX < 0.770f )){
					PowerGenerationTimer = (0.165f - (( isCheckBallPositionX / 0.770f ) * -0.165f)) + 0.330f;
				} else if( (isCheckBallPositionX <= 0 && isCheckBallPositionX > -0.770f ) && (isCheckBallPositionY >= 0.770f && isCheckBallPositionX < 0 )){
					PowerGenerationTimer = (( isCheckBallPositionX / 0.770f ) *  0.165f) + 0.495f;
				}

				isStopBall = false;
			}

			// 円運動
			this.transform.Rotate(0,0,BallRotate);
			this.transform.position = new Vector3 (StartPositionX + Mathf.Cos (PowerGenerationTimer * 円運動速度 / Mathf.PI) * 円運動半径, StartPositionY + Mathf.Sin (PowerGenerationTimer * 円運動速度 / Mathf.PI) * 円運動半径);
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

		} else {

			// 一定以上時間減速したら、Rigidbody2Dに物理演算を任せる
			if( WorldTimerControll.WorldSpeed <= 0.2f ){
				isStopBall = true;
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

				// パワー０
				isEmptyPowerGeneration = true;

			} else {

				// パワー０ではない
				isEmptyPowerGeneration = false;

				// 円運動
				this.transform.Rotate(0,0,BallRotate);
				this.transform.position = new Vector3 (StartPositionX + Mathf.Cos (PowerGenerationTimer * 円運動速度 / Mathf.PI) * 円運動半径, StartPositionY + Mathf.Sin (PowerGenerationTimer * 円運動速度 / Mathf.PI) * 円運動半径);
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			}
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
