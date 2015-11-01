using UnityEngine;
using System.Collections;

public class LiftLeft : MonoBehaviour {

	//--- オブジェクト ---//
	private GameObject LiftObj;
	private GameObject LiftCenterObj;
	private GameObject LiftRightObj;
	private GameObject playerObj;

	//--- フラグ管理 ---//
	public static bool isOnTheLeft = false;

	// 初期位置取得変数
	private float LiftStartPositionX;
	private Vector2 StartPosition;
	private Quaternion StartRotation;

	void Start () {

		LiftObj       = GameObject.Find ("Lift");
		LiftCenterObj = GameObject.Find ("LiftCenter");
		LiftRightObj  = GameObject.Find ("LiftRight");
		playerObj     = GameObject.Find ("Player");

		// 初めは左側に乗っている（左側のリフトが下がっている）
		isOnTheLeft = true;

		LiftStartPositionX = LiftObj.transform.position.x;
		StartPosition = transform.position;
		StartRotation = transform.rotation;	
	}

	void Update () {
		if (PlayerGameOver.isAllObjectDefaultPosition) {
			// ゲームオーバーになったら、初期位置に戻す
			isOnTheLeft = true;
			transform.position = StartPosition;
			transform.rotation = StartRotation;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){

		//--- 右側のリフトが下がっている（左側のリフトが上がっている）場合 ---//
		if (col.gameObject.name == "Player" && LiftRight.isOnTheRight) {
			// 左側のリフトに乗っているフラグを立てる
			LiftRight.isOnTheRight = false;
			isOnTheLeft = true;

			// 一時的にプレイヤーをリフトの子オブジェクトにする
			playerObj.transform.parent = this.gameObject.transform;
			// 子オブジェクト化するに当って、プレイヤーの大きさを固定させる（親オブジェクトの大きさに依存させない）
			PlayerControll2D.playerSize = PlayerControll2D.playerSize / LiftObj.transform.localScale.x;
			if ( PlayerControll2D.isRight ){
				playerObj.transform.localScale = new Vector3( PlayerControll2D.playerSize, PlayerControll2D.playerSize, 0);
			} else {
				playerObj.transform.localScale = new Vector3(-PlayerControll2D.playerSize, PlayerControll2D.playerSize, 0);
			}

			// 左側のリフトを下げ、右側のリフトを上げ、リフトの中央部を回転させる
			iTween.MoveTo   (this.gameObject, iTween.Hash ("y",  -1.0f, "time", 0.3f / WorldTimerControll.WorldSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad));
			iTween.MoveTo   (LiftRightObj   , iTween.Hash ("y",   0.8f, "time", 0.3f / WorldTimerControll.WorldSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad));
			iTween.RotateTo (LiftCenterObj  , iTween.Hash ("z",  30.0f, "time", 0.3f / WorldTimerControll.WorldSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad));

			//--- リフトの動作限界数に達していなければ、リフトを右側に動かす ---// 
			if( Lift.LimitPositionCountX > Lift.NowPositionCountX ){
				Lift.NowPositionCountX++;
				iTween.MoveTo   (LiftObj, iTween.Hash ("x",  LiftStartPositionX + Lift.NowPositionCountX, "time", 0.3f / WorldTimerControll.WorldSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad));
			}
		}
	}

	void OnTriggerStay2D(Collider2D col){
		// 子オブジェクト化するに当って、プレイヤーの大きさを固定させる（親オブジェクトの大きさに依存させない）
		if (col.gameObject.name == "Player" && playerObj.transform.parent != null) {
			if ( PlayerControll2D.isRight ){
				playerObj.transform.localScale = new Vector3( PlayerControll2D.playerSize, PlayerControll2D.playerSize, 0);
			} else {
				playerObj.transform.localScale = new Vector3(-PlayerControll2D.playerSize, PlayerControll2D.playerSize, 0);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		// 子オブジェクトの解除
		playerObj.transform.parent = null;
		// 子オブジェクト化するに当って、プレイヤーの大きさを固定させる（親オブジェクトの大きさに依存させない）
		if (col.gameObject.name == "Player" && !LiftRight.isOnTheRight) {
			PlayerControll2D.playerSize = 0.135f;
			if ( PlayerControll2D.isRight ){
				playerObj.transform.localScale = new Vector3( PlayerControll2D.playerSize, PlayerControll2D.playerSize, 0);
			} else {
				playerObj.transform.localScale = new Vector3(-PlayerControll2D.playerSize, PlayerControll2D.playerSize, 0);
			}
		}
	}
}
