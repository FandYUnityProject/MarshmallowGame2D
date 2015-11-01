using UnityEngine;
using System.Collections;

public class PlayerControll2D : MonoBehaviour {

	#region 変数宣言
	// フラグ管理
	public static bool isJumping  = false;	// ジャンプ中か
	public static bool isIceFloor = false;	// 氷の床上か
	public static bool isLaddar	  = false;	// ハシゴに登っているか
	public static bool isRight	  = true;	// プレイヤーの向き
	public static bool isBeforeTouchRightWall = false;	// 直前触れていた壁の左右判定	
	public static bool isBeforeTouchLeftWall  = false;	// 直前触れていた壁の左右判定

	// 数値管理
	public  float limitSpeed  		 = 0.04f;
	public  float limitFallDownSpeed = -10.0f;
	public  float jumpPower			 = 3000.0f;
	private float limitPlayerSpeed	 = 0.0f;
	private float horizontal		 = 0.0f;
	public  static float playerSpeed = 0.0f;
	public  static float playerSize  = 0.135f;

	// ハシゴ関連
	public static Collision2D LabbarCollision;
	public static Collider2D  LabbarCollider;
	#endregion

	void Update () {

		#region ゲートをくぐっている途中, かつセーブ中, プレイヤーのHPが0でない: 1,横キーの入力方向取得
		//--- ゲートをくぐっている途中, かつセーブ中, プレイヤーのHPが0, かつダメージ中でなければ, 横キーの入力方向取得 ---//
		if (!PlayerTouchObject.isGateMove && !SavePoint.isSaving && !PlayerGameOver.isGameOver && !PlayerUseAbility.isAvatarPlayerAnime && !PlayerTouchEnemy.isDamage) {
			horizontal = Input.GetAxis ("Horizontal") * 4;
		} else {
			// ゲート移動中, もしくはセーブ中, もしくはプレイヤーのHPが0の場合, スピードを0にする
			playerSpeed = 0;
			horizontal  = 0;
		}
		#endregion
		
		#region 左右キー入力時: 1,各方向にキャラクターを向かせる
		//--- 右を向いていて、左の入力があったとき、もしくは左を向いていて、右の入力があったとき ---//
		if((horizontal > 0  || horizontal < 0) && !PlayerUseAbility.isGravityChangeRotate )
		{
			//右を向いているかどうかを、入力方向から判断(重力反転中は逆転)
			if( !PlayerUseAbility.isGravityChange ) {
				isRight = (horizontal > 0);
				//右を向いているかどうかで更新する
				transform.localScale = new Vector2((isRight ?  playerSize : -playerSize), playerSize);
			} else {
				isRight = (horizontal > 0);
				//右を向いているかどうかで更新する
				transform.localScale = new Vector2((isRight ? -playerSize :  playerSize), playerSize);
			}
		}
		#endregion

		#region 壁にタッチしている時の処理
		if (PlayerUseAbility.isWallKickAbility) {
			//--- 壁キックのオートアビリティ発動時 ---//
			#region ジャンプボタン押下時: 1,左の壁についてたら右上に、右の壁についてたら左上にジャンプさせる
			// ジャンプボタン押下時, 左の壁についてたら右上に、右の壁についてたら左上にジャンプさせる
			if (Input.GetButtonDown("Jump"))
			{
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
				if( isBeforeTouchRightWall ){
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * jumpPower/100);
				}
				if( isBeforeTouchLeftWall ){
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * jumpPower/100);
				}
			}
			#endregion

			#region 床に設定していない: 1,壁にピタリと付かせる（スピードを0にする）
			// 床に設定していなければ、壁にピタリと付かせる（スピードを0にする）
			if( !PlayerTouchObject.isTouchFloor ){
				playerSpeed = 0;
				horizontal  = 0;
			}
			#endregion

			#region 同じ壁に続けて２回くっつかせない処理
			// 右の壁に接している時
			if( PlayerTouchObject.isTouchRightWall && !isBeforeTouchRightWall ){
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				isBeforeTouchRightWall = true;
				isBeforeTouchLeftWall  = false;
				isJumping = false;
			}
			// 左の壁に接している時
			if( PlayerTouchObject.isTouchLeftWall &&  !isBeforeTouchLeftWall ){
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				isBeforeTouchRightWall = false;
				isBeforeTouchLeftWall  = true;
				isJumping = false;
			}
			#endregion

		} else {
			//--- 壁にタッチしている間の処理 ---//
			#region 左の壁なら左方向、右の壁なら右方向の移動を受け付けない
			if (PlayerTouchObject.isTouchRightWall && playerSpeed >= 0) {
				// 右の壁に触れている時、右側のスピード加算を受け付けない
				playerSpeed = 0;
				horizontal = 0;
			} 
			if (PlayerTouchObject.isTouchLeftWall && playerSpeed <= 0) {
				// 左の壁に触れている時、右側のスピード加算を受け付けない
				playerSpeed = 0;
				horizontal = 0;
			}
			#endregion
		}
		#endregion

		#region プレイヤーの位置を取得
		// プレイヤーの位置を取得
		Vector2 Position = transform.position;
		#endregion

		#region ダッシュボタン押下時: 1,最大速度を上げる
		//--- ダッシュボタン押下時、最大速度を上げる ---//
		if (Input.GetButton ("Dash")) { limitPlayerSpeed = limitSpeed * 2; }
		else { limitPlayerSpeed = limitSpeed; }
		#endregion

		#region ハシゴに登ってい無い場合: 1,徐々に加速(最大速度を超えないようにする)
		//--- ハシゴに登ってい無い場合, 徐々に加速(最大速度を超えないようにする) ---//
		if (!isLaddar) {
			playerSpeed += horizontal / 500;
		} else {
			// ハシゴに登ってる時は横方向のスピードを０にし、ジャンプボタンを押したらハシゴを登るアクションをキャンセルする
			playerSpeed = 0;
			if (Input.GetButtonDown("Jump")){
				isLaddar = false;
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
		}
		#region プレイヤーの横スピードが一定以上超えないように制御
		// プレイヤーのスピードが一定以上超えないように制御
		if( playerSpeed >  limitPlayerSpeed ) { playerSpeed =  limitPlayerSpeed; }
		if( playerSpeed < -limitPlayerSpeed ) { playerSpeed = -limitPlayerSpeed; }
		#endregion
		#endregion

		#region 十字キー未入力時: 1,氷の床以外減速 2,氷の床では"徐々"に減速
		//--- 十字キー未入力時、徐々に減速（摩擦演出）---//
		if( !Input.GetButton("Horizontal") ) { 
			if( !isIceFloor ){
				if( playerSpeed >= -0.03f && playerSpeed <= 0.03f ){ playerSpeed *= 0.88f; }
				else { playerSpeed *= 0.94f; }
			} else {
				// 氷の上では摩擦力を下げる
				if( playerSpeed >= -0.03f && playerSpeed <= 0.03f ){ playerSpeed *= 0.95f; }
				else { playerSpeed *= 0.98f; }
			}
		}
		#endregion

		#region ハシゴに登っている間: 1,上下キーでY座標を移動させる
		//--- ハシゴに登っている間の処理 ---//
		if (isLaddar) {
			float vertical = Input.GetAxis("Vertical") / 20;
			Position.y += vertical;
		}
		#endregion

		#region プレイヤーの位置を反映
		//--- プレイヤーの位置を反映 ---//
		Position.x += playerSpeed;
		transform.position = Position;
		#endregion

		#region ジャンプボタンでジャンプ: 1,床接地状態の時にジャンプボタンでジャンプ 2,飛行モード中はいつでもジャンプ
		//--- ジャンプボタンでジャンプ ---//
		if (Input.GetButtonDown ("Jump") && !isJumping) {
			// 床接地状態の時にジャンプボタンでジャンプ
			if ( !PlayerUseAbility.isGravityChange ){
				this.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpPower);
			} else {
				// 重力反転時は下向きにジャンプ
				this.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.down * jumpPower);
			}
		} else if (Input.GetButtonDown ("Jump") && PlayerUseAbility.isFlyUse) {
			// 飛行モード中はいつでもジャンプ！
			this.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpPower * 0.6f);
		}
		#endregion

		#region ちょいジャンプの実装
		//--- ちょいジャンプの実装 ---//
		if (this.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 && Input.GetButtonUp("Jump")) {
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(this.gameObject.GetComponent<Rigidbody2D>().velocity.x, this.gameObject.GetComponent<Rigidbody2D>().velocity.y/1.5f);
		}
		#endregion

		#region 落下スピードの最大値を設定: 1,飛行モード中かつ下キー未入力の時は、落下スピードを下げる 2,それ以外は通常落下スピード
		//--- 落下スピードの最大値を設定 ---//
		// 飛行モード中かつ下キー未入力の時は、落下スピードを下げる
		if (PlayerUseAbility.isFlyUse && Input.GetAxisRaw("Vertical") >= 0 ) {
			limitFallDownSpeed = -0.05f;
		} else {
			limitFallDownSpeed = -10.0f;
		}
		if (this.gameObject.GetComponent<Rigidbody2D> ().velocity.y < limitFallDownSpeed) {
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(this.gameObject.GetComponent<Rigidbody2D>().velocity.x, limitFallDownSpeed);
		}
		#endregion
	}
}
