using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTouchObject : MonoBehaviour {

	#region 変数宣言
	private Image BlackOutImage;	// 画面暗転用イメージ
	private GameObject  TargetGate;	// 移動先のゲート保存変数
	private GameObject  SaveMapObj;	// 移動先のセーブマップ保存変数

	private GameObject  CameraObj;			// カメラオブジェクト
	public static float CameraStartPosY;	// ゲーム開始時のカメラのY位置
	public static float CameraStartPosZ;	// ゲーム開始時のカメラのZ位置
	
	public static bool isGateMove = false; // ゲートをくぐっている途中か
	public static bool isSaveMap  = false; // ゲートの先がセーブマップかどうか(※セーブデータ先は必ず“SaveMap”と末尾に”_B”を入れること！)
	public static bool isGateB    = false; // くぐったゲートがゲートBかを判定
	
	public static bool isTouchFloor = false; // 床に触れているフラグ
	public static bool isTouchGravityFloor = false; // （重力反転中に）床に触れているフラグ

	public static bool isTouchRightWall = false; // 右側の壁に触れているフラグ
	public static bool isTouchLeftWall  = false; // 左側の壁に触れているフラグ
	public static bool isTouchWall      = false; // 壁に触れているフラグ(壁蹴りアビリティ用)
	
	public static float IntermediateGatePointX = 0.0f;	// 最後にくぐった移動先ゲートをライフ0後の再開地点とする
	public static float IntermediateGatePointY = 0.0f;	// 最後にくぐった移動先ゲートをライフ0後の再開地点とする
	#endregion

	void Start(){

		#region ゲームオブジェクトの取得
		// ゲームオブジェクトの取得 
		BlackOutImage   = GameObject.Find ("BlackOut").GetComponent<Image> ();
		CameraObj       = GameObject.Find ("Main Camera");
		#endregion

		#region ゲーム開始時のカメラオブジェクトの初期位置取得
		// ゲーム開始時のカメラオブジェクトの初期位置取得
		CameraStartPosY = CameraObj.transform.position.y;
		CameraStartPosZ = CameraObj.transform.position.z;
		#endregion

		#region フラグの初期化
		// フラグの初期化
		isGateMove = false; 
		isSaveMap  = false; 
		isGateB    = false; 
		isTouchRightWall = false;
		isTouchLeftWall  = false; 
		#endregion

		#region ゲーム開始時の画面を徐々に明るくするアニメーション
		FadeInAnimation ();
		#endregion
	}
	
	#region ゲーム開始時の画面明転アニメーション開始関数
	void FadeInAnimation(){
		// ゲート移動の時の画面暗転アニメーション
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 0.4f, "delay", 0.05f, "onUpdate", "UpdateFadeInAnimation"));
	}
	#endregion
	
	#region ゲーム開始時の画面明転アニメーション関数
	void UpdateFadeInAnimation(float ImageAlpha){
		// 徐々に画面明るくする
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}
	#endregion

	void Update(){

		#region どちらの壁に接触しているかを表示するデバッグログ
		// どちらの壁に接触しているかを表示するデバッグログ
		if (isTouchLeftWall) {
			Debug.Log (" 左の壁 ");
		} 
		if (isTouchRightWall) {
			Debug.Log (" 右の壁 ");
		} 
		#endregion
	}

	void OnCollisionStay2D(Collision2D col){

		#region ハシゴの頂点(Collision)に衝突中かつ下方向入力時: 1.一瞬ハシゴの頂点をトリガーにしてハシゴから降りられるようにする 2.Playerをハシゴ登り状態（無重力状態）に変更
		//--- ハシゴの頂点に衝突中かつ下方向入力時 ---//
		if (col.gameObject.tag == "Laddar" && Input.GetAxis("Vertical") < -0.99f) {

			// 一時的にハシゴの頂点のCollisionをTriggerにし、ハシゴ頂点のCollisionとColliderを保存
			PlayerControll2D.LabbarCollision = col;
			PlayerControll2D.LabbarCollision.collider.isTrigger = true;
			PlayerControll2D.LabbarCollider = PlayerControll2D.LabbarCollision.collider;

			// Playerをハシゴ登り状態（無重力状態）に変更
			PlayerControll2D.isLaddar   = true;
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
		#endregion
	}

	void OnTriggerEnter2D(Collider2D col){

		#region デスゾーンに触れる: 1.一撃死
		//--- デスゾーンに触れると一撃死 ---//
		if (col.gameObject.tag == "DeathZone") {
			//一撃死
			StatusUIControll.playerHP = 0;
			Debug.Log("GAME OVER");
		}
		#endregion
	}
	
	void OnTriggerStay2D(Collider2D col){
		
		#region ハシゴ(Trigger)に触れているかつ上下キー入力時: 1.Playerをハシゴ登り状態（無重力状態）に変更, 2.プレイヤーのX位置を登っているハシゴの位置に固定
		//--- ハシゴに触れているかつ、上下キー入力時 ---//
		if (col.gameObject.tag == "Laddar" && (Input.GetAxis("Vertical") > 0.99f || Input.GetAxis("Vertical") < -0.99f || Input.GetButton("Vertical")) ) {
			
			// 床接地（ハシゴを登り始め）の場合, Playerをハシゴ登り状態（無重力状態）に変更
			if( (!PlayerUseAbility.isGravityChange && isTouchFloor) || (PlayerUseAbility.isGravityChange && isTouchGravityFloor) ){
				if( Input.GetAxis("Vertical") > 0.99f ){
					PlayerControll2D.isLaddar   = true;
					this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				}
			} else {
				// 空中(ジャンプ中)から登り始めた場合, Playerをハシゴ登り状態（無重力状態）に変更
				PlayerControll2D.isLaddar   = true;
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			}
			
			// プレイヤーのX位置を登っているハシゴの位置に固定
			Vector2 Position = transform.position;
			Position.x = col.gameObject.transform.position.x;
			transform.position = Position;
		}
		#endregion

		#region 床接地時: 1.ジャンプ、接壁、壁キックフラグを解除
		//--- 床接地時、ジャンプ、接壁、壁キックフラグを解除 ---//
		if (col.gameObject.tag == "Floor" && !PlayerUseAbility.isGravityChange) {
			isTouchFloor = true;
			PlayerControll2D.isJumping = false;
			PlayerControll2D.isBeforeTouchRightWall = false;
			PlayerControll2D.isBeforeTouchLeftWall  = false;
			PlayerUseAbility.isWallKickAbility      = false;
		}
		#endregion

		#region (重力反転中)床接地時: 1.ジャンプ、接壁、壁キックフラグを解除)
		//--- 床接地時、ジャンプ、接壁、壁キックフラグを解除 ---//
		if (col.gameObject.tag == "GravityFloor" && PlayerUseAbility.isGravityChange) {
			isTouchGravityFloor = true;
			PlayerControll2D.isJumping = false;
			PlayerControll2D.isBeforeTouchRightWall = false;
			PlayerControll2D.isBeforeTouchLeftWall  = false;
			PlayerUseAbility.isWallKickAbility      = false;
		}
		#endregion

		#region 氷の床接地時: 1.氷の床フラグを立てる
		//--- 氷の床接地時、氷の床フラグを立てる ---//
		if (col.gameObject.name == "IceBlock") {
			PlayerControll2D.isIceFloor = true;
		}
		#endregion

		#region ゲート接触かつ上キー入力時、かつゲート移動中でない時: 1.ゲートAかBを判定, 2.ゲートAかBに移動
		//--- ゲート接触、かつ上キー入力時、かつゲート移動中でないとき ---//
		if (col.gameObject.tag == "Gate" && (Input.GetAxis("Vertical") > 0.99f || (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0)) && !isGateMove) {

			// ゲート名を取得し、ゲートAかBを判定
			string GateName      = col.gameObject.name;
			int    Name_A_Count  = GateName.LastIndexOf("_A");
			int    Name_B_Count  = GateName.LastIndexOf("_B");
			// ゲート名を取得し、ゲートがセーブマップ行きか確認
			int    SaveMap_Count = GateName.LastIndexOf("SaveMap");

			// ゲートAならゲートBの位置を取得し、移動
			if( Name_A_Count != -1 ){
				isGateB = false;

				string GateNameSub = GateName.Substring(0, Name_A_Count);
				GameObject GateB = GameObject.Find(GateNameSub + "_B");

				Debug.Log("GateB: " + GateNameSub);

				// セーブマップ行きであればセーブマップフラグを立て, 移動先のセーブマップのオブジェクトを取得
				if( SaveMap_Count != -1 ){
					isSaveMap = true;
					string SaveMapNameSub = GateName.Substring(0, SaveMap_Count);
					Debug.Log("SaveMap: " + SaveMapNameSub);
					SaveMapObj = GameObject.Find(SaveMapNameSub + "SaveMap");
				}

				// ゲート移動のアニメーションを設定
				TargetGate = GateB;
				GateAnimationIn();
			} else {
				// ゲートBならゲートAの位置を取得し、移動
				isGateB = true;

				string GateNameSub = GateName.Substring(0, Name_B_Count);
				GameObject GateA = GameObject.Find(GateNameSub + "_A");

				Debug.Log("GateA: " + GateNameSub);

				// ゲート移動のアニメーションを設定
				TargetGate = GateA;
				GateAnimationIn();
			}
		}
		#endregion

		#region プレイヤーが左右の壁に触れている時: 1,左右の壁に触れているフラグを立てる
		//--- プレイヤーが左の壁に触れている時 ---//
		if (col.gameObject.tag == "LeftWall") {

			if( (!isTouchFloor || !isTouchGravityFloor) && !PlayerUseAbility.isAvatarUse ){
				PlayerControll2D.isJumping = true;
			}

			if (Input.GetAxis ("Horizontal") < 0) {
				// 左に移動しながら左の壁に触れている時、左の壁に触れているフラグを立てる
				isTouchLeftWall  = true;
				isTouchRightWall = false;
			} else {
				isTouchLeftWall  = false;
				isTouchRightWall = false;
			}
		}
		//--- プレイヤーが左の壁に触れている時 ---//
		if (col.gameObject.tag == "RightWall") {

			if( (!isTouchFloor || !isTouchGravityFloor) && !PlayerUseAbility.isAvatarUse ){
				PlayerControll2D.isJumping = true;
			}

			if (Input.GetAxis ("Horizontal") > 0) {
				// 右に移動しながら右の壁に触れている時、左の壁に触れているフラグを立てる
				isTouchLeftWall  = false;
				isTouchRightWall = true;
			} else {
				isTouchLeftWall  = false;
				isTouchRightWall = false;
			}
		}
		#endregion
	}

	
	void OnTriggerExit2D(Collider2D col){
		
		#region ハシゴから離れた時: 0,ハシゴの頂点のTriggerを解除, 1,ハシゴ登り状態を解除
		//--- ハシゴから離れた時 ---//
		if (col.gameObject.tag == "Laddar") {
			
			// ハシゴの頂点から降りていれば、ハシゴの頂点のTriggerを解除
			if( PlayerControll2D.LabbarCollision != null ){
				PlayerControll2D.LabbarCollision.collider.isTrigger = false;
			}
			if( col != PlayerControll2D.LabbarCollider ){
				// 離れたTriggreがハシゴの頂点でなければ、ハシゴ登り状態を解除
				PlayerControll2D.isLaddar = false;
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			} else {
				if( Input.GetAxis("Vertical") > 0.99f ){
					// 離れたTriggreがハシゴの頂点、かつ上キー入力時であればハシゴ登り状態を解除
					PlayerControll2D.isLaddar = false;
					this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				}
			}
		}
		#endregion

		#region 何かから離れた瞬間（空中落下中）: 1,ジャンプ中と判定
		// 何かから離れた瞬間（空中落下中）、ジャンプ中と判定
		PlayerControll2D.isJumping  = true;
		#endregion

		#region 床離脱時: 1.床接地フラグを解除
		//--- 床接地フラグを解除 ---//
		if (col.gameObject.tag == "Floor") {
			isTouchFloor = false;
		}
		#endregion

		#region (重力反転中)床離脱時: 1.床接地フラグを解除
		//--- 床接地フラグを解除 ---//
		if (col.gameObject.tag == "GravityFloor") {
			isTouchGravityFloor = false;
		}
		#endregion
		
		#region 氷の床離脱時: 1.氷の床フラグを下ろす
		//--- 氷の床接地時、氷の床フラグを下ろす ---//
		if (col.gameObject.name == "IceBlock") {
			PlayerControll2D.isIceFloor = false;
		}
		#endregion

		#region プレイヤーが壁から離れた時: 1,壁接触フラグと壁蹴りフラグを下ろす
		//--- プレイヤーが壁から離れた時 ---//
		if (col.gameObject.tag == "LeftWall" || col.gameObject.tag == "RightWall" ) {
			isTouchRightWall = false;
			isTouchLeftWall  = false;
			PlayerUseAbility.isWallKickAbility = false;
		}
		#endregion
	}

	#region ゲート移動前中後のアニメーション関数

	#region ゲートに入った瞬間の関数
	void GateAnimationIn(){

		// ゲート移動中
		isGateMove = true;

		// ゲート移動の時の画面暗転アニメーション
		iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 1.0f, "time", 0.5f, "delay", 0.2f, "onUpdate", "UpdateGateAnimation", "onComplete", "CompleteGateAnimationIn"));
	}
	#endregion

	#region ゲートに入ってから出るまでの画面暗転/明転アニメーション関数
	void UpdateGateAnimation(float ImageAlpha){
		// 徐々に画面暗転/明るくする
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}
	#endregion

	#region 画面暗転後のアニメーション関数
	void CompleteGateAnimationIn(){
		//--- 画面暗転が完了したら ---//

		// 世界時間を0にする(時間に依存して動くオブジェクトを初期位置に戻す)
		WorldTimerControll.WorldTimer = 0;

		// ゲートBをくぐったなら、強制的にセーブマップフラグを下ろす
		if (isGateB) {
			isSaveMap = false;
		}

		// ゲート移動終了
		isGateMove = false;

		// プレイヤーの位置をゲートの出先位置に設定
		Vector2 Position = transform.position;
		Position.x = TargetGate.gameObject.transform.localPosition.x;
		Position.y = TargetGate.gameObject.transform.localPosition.y;
		transform.position = Position;

		// 再開地点を移動先のゲートに設定
		IntermediateGatePointX = TargetGate.gameObject.transform.localPosition.x;
		IntermediateGatePointY = TargetGate.gameObject.transform.localPosition.y;

		//--- セーブマップでない場合、プレイヤーの位置がゲームスタート時のカメラの位置より低いか高いかでカメラの位置を変更 ---//
		if (!isSaveMap) {
			if (CameraStartPosY > transform.position.y) {
				CameraObj.transform.position = new Vector3 (transform.position.x, CameraStartPosY, CameraStartPosZ);
			} else {
				CameraObj.transform.position = new Vector3 (transform.position.x, transform.position.y, CameraStartPosZ);
			}
		} else {
			//--- セーブマップの場合、カメラの位置をセーブマップの中心に設定 ---//
			CameraObj.transform.position = new Vector3 (SaveMapObj.transform.position.x, SaveMapObj.transform.position.y, CameraStartPosZ);
		}

		// ゲート移動のアニメーション
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 0.2f, "delay", 0.3f, "onUpdate", "UpdateGateAnimation", "onComplete", "CompleteGateAnimationOut"));

	}
	#endregion

	#region ゲート移動後の画面明転後の関数
	void CompleteGateAnimationOut(){
		//--- 画面が明るくなったら ---//
	}
	#endregion

	#endregion
}
