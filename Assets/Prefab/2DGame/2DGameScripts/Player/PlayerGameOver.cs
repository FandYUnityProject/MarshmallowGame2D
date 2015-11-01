using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGameOver : MonoBehaviour {
	
	private Image      BlackOutImage;	// 画面暗転用イメージ
	private GameObject CameraObj;

	public static bool isGameOver;
	public static bool isAllObjectDefaultPosition;

	private Vector3 CameraStartPosition;

	void Start () {
		BlackOutImage   = GameObject.Find ("BlackOut").GetComponent<Image> ();
		CameraObj       = GameObject.Find ("Main Camera");
		
		CameraStartPosition = CameraObj.transform.position;

		isGameOver = false;
		isAllObjectDefaultPosition = false;
	}

	void Update () {
	
		// プレイヤーのHPが0になったらゲームオーバー
		if (StatusUIControll.playerHP <= 0 && !isGameOver) {
			isGameOver = true;

			// あらゆる物理演算を無効
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

			// 画面暗転アニメーション
			iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 1.0f, "time", 0.8f, "delay", 3.0f, "onUpdate", "UpdateGameOverAnimation", "onComplete", "CompleteGameOverAnimation"));

		}
	}

	void UpdateGameOverAnimation(float ImageAlpha){
		// 徐々に画面暗転/明るくする
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}

	void CompleteGameOverAnimation(){
		//--- 画面暗転が完了したら ---//

		// 全てのオブジェクトを元の場所に戻す
		isAllObjectDefaultPosition = true;

		// HP全快
		StatusUIControll.playerHP = 100;

		//--- 残機反映 ---//
		StatusUIControll.RemainingNumber--;
		// 残機0でHP0になったらゲームオーバ
		if( StatusUIControll.RemainingNumber < 0 ){ Application.LoadLevel("GameOver"); } 

		// プレイヤーの位置をゲートの出先（再開地点）に設定
		Vector2 Position = transform.position;

		// ゲームを始めて一回以上ゲートをくぐっている場合
		if (PlayerTouchObject.IntermediateGatePointX != 0 && PlayerTouchObject.IntermediateGatePointY != 0) {
			Position.x = PlayerTouchObject.IntermediateGatePointX;
			Position.y = PlayerTouchObject.IntermediateGatePointY;
			transform.position = Position;

			// プレイヤーの位置がゲームスタート時のカメラの位置より低いか高いかでカメラの位置を変更
			if (PlayerTouchObject.CameraStartPosY > transform.position.y) {
				CameraObj.transform.position = new Vector3 (transform.position.x, PlayerTouchObject.CameraStartPosY, PlayerTouchObject.CameraStartPosZ);
			} else {
				CameraObj.transform.position = new Vector3 (transform.position.x, transform.position.y, PlayerTouchObject.CameraStartPosZ);
			}

		} else {

			// “はじめから”始めていない、かつゲートを一度もくぐっていない場合ゲーム再開フラグを立てる
			if ( PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_WorldNum") != 0
			  && PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_FloorNum") != 0){

				SavePointPositionControll.isResumption = true;
				// ゲームを始めて一回以上ゲートをくぐっていない場合は、セーブポイントから再開
				transform.position = new Vector3 (  PlayerPrefs.GetFloat ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_SavePoint_X")
				                                  , PlayerPrefs.GetFloat ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_SavePoint_Y")
				                                  , 0);

				// プレイヤーの位置がゲームスタート時のカメラの位置より低いか高いかでカメラの位置を変更
				if (PlayerTouchObject.CameraStartPosY > transform.position.y) {
					CameraObj.transform.position = new Vector3 (transform.position.x, PlayerTouchObject.CameraStartPosY, PlayerTouchObject.CameraStartPosZ);
				} else {
					CameraObj.transform.position = new Vector3 (transform.position.x, transform.position.y, PlayerTouchObject.CameraStartPosZ);
				}

			} else {
				// “はじめから”はじめて一度もゲートをくぐっていない場合はスタート地点から
				transform.position = new Vector3 (NewGamePositionControll.NewGamePositionX, NewGamePositionControll.NewGamePositionY, 0);
				// カメラ位置も初期化
				CameraObj.transform.position = CameraStartPosition;
			}
		}

		// 物理演算を有効
		this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

		// 再開の暗転アニメーション
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 1.0f, "delay", 0.3f, "onUpdate", "UpdateGameOverAnimation","onComplete", "CompleteUpdateGameOverAnimation"));
		
		isGameOver = false; 
	}

	void CompleteUpdateGameOverAnimation(){
		// 全てのオブジェクトを元の場所に戻すフラグを下ろす
		isAllObjectDefaultPosition = false;
	}
}
