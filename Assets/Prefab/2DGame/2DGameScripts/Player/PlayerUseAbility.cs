using UnityEngine;
using System.Collections;

/* --- 能力番号:オブジェクト名 ---
 * 0: StrengthImage -> 肉体強化
 * 1: GravityImage  -> 重力操作
 * 2: AvatarImage   -> 分身
 * 3: FlyImage      -> 飛行
 * 4: ChronoImage   -> 時間操作
 * 5: PsychoImage   -> 念力
 * 6: MagicImage    -> 魔法
 */

public class PlayerUseAbility : MonoBehaviour {

	#region 変数宣言

	public static float  PunchSize = 0.27f;	// パンチのサイズ

	//--- 肉体強化関連 ---//
	public static bool isStrengthUse;
	public static bool isStrengthMode;
	private GameObject StrengthEffectObj;	// 肉体強化時のエフェクト

	//--- 重力操作関連 ---//
	public static bool isGravityUse;
	public static bool isGravityChange;
	public static bool isGravityChangeAnime  = false;
	public static bool isGravityChangeRotate = false;
	private GameObject GravityChangeEffectObj;	// 重力反転中のエフェクト

	//--- 分身関連 ---//
	public static bool isAvatarUse;
	public static bool isAvatarPlayer;
	public static bool isAvatarPlayerAnime = false;
	public  GameObject Shruiken;		// 手裏剣オブジェクト
	public  GameObject AvatarPlayer;	// 分身Playerオブジェクト

	//--- 飛行関連 ---//
	public static bool isFlyUse;
	public static bool isFlying;
	public  GameObject Feather;		// 羽オブジェクト

	//--- 時間操作関連 ---//
	public static bool isChronoUse;		// 時間操作使用中か
	public static bool isChronoHigh;	// 時間速度アップを使用中か
	public static bool isChronoSlow;	// 時間速度ダウンを使用中か
	private GameObject TimeEffectObj;	// 時間操作エフェクト用オブジェクト

	//--- 念力関連 ---//
	public  static bool isPsychoUse;
	public  static bool isPsychoEffect;   // サイコエフェクト使用中か
	public  GameObject PsychoBall;		  // サイコボールオブジェクト
	public  GameObject BigPsychoBall;	  // ビッグサイコボールオブジェクト
	private GameObject ChargePsychoBall;  // サイコチャージオブジェクト
	private GameObject PsychoEffectObj;   // サイコエフェクト用オブジェクト
	private float PsychoEffectRotate = 0; // サイコエフェクト用回転角度
	private bool isWorldSpeedUpdate = false;

	//--- 魔法関連 ---//
	public static bool isMagicUse;
	public  GameObject FireShield;			// ファイアシールドオブジェクト
	public  GameObject IceLance;			// アイスランスオブジェクト
	public  GameObject Lightning;			// 落雷オブジェクト
	private float LightningTimer = 0.0f;	// 落雷オブジェクト表示時間
	private bool  isLightning = false;		// 落雷オブジェクト表示中か

	//--- 共有アビリティ ---//
	public static bool isWallKickAbility;	// 壁キック
	public static bool isPunchAbility;		// パンチ
	public static bool isShotAbility;		// ショット
	private GameObject PunchObj;			// パンチ用オブジェクト
	public  float チャージ時間 = 1.5f;			// チャージ時間
	private float ShotButtonPushTime;		// ショットボタンを長押ししている時間

	#endregion

	void Start(){


		#region 各能力で使用するオブジェクト取得
		// 各能力で使用するオブジェクト
		StrengthEffectObj      = GameObject.Find ("StrengthModeEffect");
		GravityChangeEffectObj = GameObject.Find ("GravityChangeEffect");
		TimeEffectObj          = GameObject.Find ("TimeEffect");
		ChargePsychoBall       = GameObject.Find ("ChargePsychoBall");
		PsychoEffectObj        = GameObject.Find ("PsychoEffect");
		PunchObj               = GameObject.Find ("Punch");
		#endregion

		GravityChangeEffectObj.SetActiveRecursively (false);
		AvatarPlayer.SetActiveRecursively (false);
		ChargePsychoBall.SetActiveRecursively (false);
		Lightning.SetActiveRecursively (false);

		#region 全ての能力に関するフラグを下ろす
		// 全ての能力に関するフラグを下ろす
		IsNotAllAbility ();
		#endregion
	}

	void Update () {

		// ゲート移動時、セーブ中、ゲームオーバー中はアビリティ禁止
		if( PlayerTouchObject.isGateMove || SavePoint.isSaving || PlayerGameOver.isGameOver ){
			IsNotAllAbility();
			return;
		}

		#region 能力を装備している間（オートアビリティ）
		if (AbilityChange.GetAbility != 0) {
			//--- 能力を装備している間（オートアビリティ）---//
			#region 肉体強化
			// 0: StrengthImage -> 肉体強化
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 0) {
				isStrengthUse = true;
				Debug.Log ("肉体強化");

				PunchSize = 0.42f;
			} else {
				isStrengthUse  = false;
				isStrengthMode = false;
				StrengthEffectObj.SetActiveRecursively (false);
				PunchSize = 0.27f;
			}
			#endregion
			#region 重力操作
			// 1: GravityImage  -> 重力操作
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 1) {
				isGravityUse = true;
				Debug.Log ("重力操作");
			} else {
				isGravityUse    = false;
				isGravityChange = false;
				this.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
			}
			#endregion
			#region 分身
			// 2: AvatarImage   -> 分身
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 2) {
				Debug.Log ("分身");
				isAvatarUse = true;

				// 壁にタッチしたらオートアビリティを発動
				if ((PlayerTouchObject.isTouchRightWall || PlayerTouchObject.isTouchLeftWall) && !PlayerTouchObject.isTouchFloor) {
					isWallKickAbility = true;
				}
			} else {
				AvatarPlayer.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
				isAvatarUse = false;
				isAvatarPlayerAnime = false;
				AvatarPlayer.SetActiveRecursively (false);

			}
			#endregion
			#region 飛行
			// 3: FlyImage      -> 飛行
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 3) {
				Debug.Log ("飛行");
				isFlyUse = true;
			} else {
				isFlyUse = false;
			}
			#endregion
			#region 時間操作
			// 4: ChronoImage   -> 時間操作
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 4) {
				Debug.Log ("時間操作");
				isChronoUse  = true;
			} else {
				isChronoUse  = false;
				isChronoHigh = false;
				isChronoSlow = false;
				TimeEffect.TimeEffectNeedleRotateSpeed = TimeEffect.RotateSpeedDefaultStatic;
				TimeEffect.TimeEffectNeedleNowRotate   = 0;
				if( !isWorldSpeedUpdate ){
					isWorldSpeedUpdate = true;
					iTween.ValueTo (gameObject, iTween.Hash ("from", WorldTimerControll.WorldSpeed, "to", 1.0f, "time", 1.0f, "onUpdate", "UpdateWorldSpeed", "onComplete", "CompleteWorldSpeed"));
				}
				TimeEffectObj.SetActiveRecursively (false);
			}
			#endregion
			#region 念力
			// 5: PsychoImage   -> 念力
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 5) {
				Debug.Log ("念力");
				isPsychoUse = true;
			} else {
				isPsychoUse = false;
				// チャージを解除
				ChargePsychoBall.SetActiveRecursively (false);
				// サイコエフェクトを解除
				PsychoEffectRotate = 0;
				PsychoEffectObj.SetActiveRecursively (false);
				isPsychoEffect = false;

			}
			#endregion
			#region 魔法
			// 6: MagicImage    -> 魔法
			if (AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 6) {
				Debug.Log ("魔法");
				isMagicUse = true;
				if( LightningTimer <= 0 ){
					LightningTimer = 0.0f;
					isLightning = false;
					Lightning.SetActiveRecursively (false);
				} else {
					LightningTimer -= Time.deltaTime;
					Lightning.SetActiveRecursively (true);
				}
			} else {
				isMagicUse = false;
				isLightning = false;
				Lightning.SetActiveRecursively (false);
				FireShield.SetActiveRecursively (false);
			}
			#endregion
		}
		#endregion

		#region 能力を装備している間（共有アビリティ）
		//--- 能力を装備している間（共有アビリティ）---//
		if (AbilityChange.GetAbility != 0) {
			if (   AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 0
				|| AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 1
				|| AbilityChange.NowSlotAbilitiesNumList [AbilityChange.NowSlotAbilitiesNum] == 4) {
				// 肉体強化, 重力操作, 時間操作能力はパンチ
				isPunchAbility = true;
				isShotAbility  = false;
			} else {
				// それ以外の能力はショット
				isPunchAbility = false;
				PunchObj.SetActiveRecursively (false);
				isShotAbility  = true;
			}
		} else {
			// ノーマル時もパンチを使用
			isPunchAbility = true;
			isShotAbility  = false;
		}
		#endregion

		//--- ゲートをくぐっている途中, かつセーブ中, プレイヤーのHPが0でなければ ---//
		#region ボタンと設定中の能力に応じて能力使用
		if (Input.GetButton ("R2") && !PlayerTouchObject.isGateMove && !SavePoint.isSaving && !PlayerGameOver.isGameOver) {
			//--- R2ボタン押下時 ---//
			#region 肉体強化
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 0) {
				// 0: StrengthImage -> 肉体強化

				// 肉体強化モードに入る
				isStrengthMode = true;
				StrengthEffectObj.SetActiveRecursively (true);
				StrengthEffectObj.transform.position = new Vector3 (transform.position.x, transform.position.y, -0.4f);
			}
			#endregion
			#region 重力操作
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 1) {
				// 1: GravityImage  -> 重力操作
			}
			#endregion
			#region 分身
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 2) {
				// 2: AvatarImage   -> 分身
				
				AvatarPlayer.SetActiveRecursively (true);

				if( !isAvatarPlayer ){
					isAvatarPlayerAnime = true;
					this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
					AvatarPlayer.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
					iTween.MoveTo (AvatarPlayer, iTween.Hash ("y", this.transform.position.y + 2.0f, "time", 0.2f, "oncomplete", "CompleteAvatarPlayerAnime", "oncompletetarget", gameObject));
				} else {
					AvatarPlayer.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2.0f);
				}

				isAvatarPlayer = true;

				if( PlayerControll2D.isRight ){
					AvatarPlayer.transform.localScale = new Vector2( 0.135f, 0.135f);
				} else {
					AvatarPlayer.transform.localScale = new Vector2(-0.135f, 0.135f);
				}
			}
			#endregion
			#region 飛行
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 3) {
				// 3: FlyImage      -> 飛行
				isFlying = true; 
			}
			#endregion
			#region 時間操作
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 4) {
				// 4: ChronoImage   -> 時間操作
				isChronoHigh = true;
				isChronoSlow = false;
				TimeEffectObj.SetActiveRecursively (true);
				TimeEffectObj.transform.position = new Vector2 (transform.position.x, transform.position.y);

				if( !isWorldSpeedUpdate ){
					isWorldSpeedUpdate = true;
					iTween.ValueTo (gameObject, iTween.Hash ("from", WorldTimerControll.WorldSpeed, "to", 2.0f, "time", 1.0f, "onUpdate", "UpdateWorldSpeed", "onComplete", "CompleteWorldSpeed"));
				}
			}
			#endregion
			#region 念力
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 5) {
				// 5: PsychoImage   -> 念力
				isPsychoEffect = true;
				PsychoEffectRotate += Time.deltaTime;
				PsychoEffectObj.SetActiveRecursively (true);
				PsychoEffectObj.transform.position = new Vector2 (transform.position.x, transform.position.y);
				PsychoEffectObj.transform.Rotate(new Vector3(0,0,PsychoEffectRotate));
			}
			#endregion
			#region 魔法
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 6) {
				// 6: MagicImage    -> 魔法
				if(Input.GetButtonDown ("R2") && !isLightning){
					isLightning = true;
					LightningTimer = 0.8f;
					if( PlayerControll2D.isRight ){
						Lightning.transform.position = new Vector3 (transform.position.x + 1.5f, transform.position.y + 9.9f, -0.2f);
					} else {
						Lightning.transform.position = new Vector3 (transform.position.x - 1.5f, transform.position.y + 9.9f, -0.2f);
					}
				}
			}
			#endregion
		} else if (Input.GetButton ("L2") && !PlayerTouchObject.isGateMove && !SavePoint.isSaving) {

			//--- L2ボタン押下時 ---//
			#region 肉体強化
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 0) {
				// 0: StrengthImage -> 肉体強化

				// 肉体強化モードに入る
				isStrengthMode = true;
				StrengthEffectObj.SetActiveRecursively (true);
				StrengthEffectObj.transform.position = new Vector3 (transform.position.x, transform.position.y, -0.4f);
			}
			#endregion
			#region 重力操作
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 1) {
				// 1: GravityImage  -> 重力操作
				// 重力リセットゾーンに触れていなければ、重力反転
				isGravityChange = true;
				if( !PlayerTouchZone.isGravityResetZone ){
					this.GetComponent<Rigidbody2D>().gravityScale = -1.0f;
					GravityChangeEffectObj.SetActiveRecursively (true);
					GravityChangeEffectObj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);

					if( !isGravityChangeAnime ){
						isGravityChangeAnime = true;
						isGravityChangeRotate = true;
						GravityChangeEffectObj.transform.localScale = new Vector2(0, 0);
						iTween.ScaleTo (GravityChangeEffectObj, iTween.Hash ("x", 0.8f, "y", 0.8f, "time", 0.3f, "oncompletetarget", gameObject, "easetype", iTween.EaseType.easeInQuad));
						iTween.RotateTo (gameObject, iTween.Hash ("z", -180, "time", 0.3f,"oncomplete", "CompleteGravityChangeRotate", "easetype", iTween.EaseType.easeInQuad));
					}
				} else {
					isGravityChangeAnime = false;
					isGravityChangeRotate = true;
					iTween.ScaleTo (GravityChangeEffectObj, iTween.Hash ("x", 0.0f, "y", 0.0f, "time", 0.3f,"oncomplete", "CompleteGravityChangeAnime", "oncompletetarget", gameObject, "easetype", iTween.EaseType.easeInQuad));
					iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "time", 0.3f,"oncomplete", "CompleteGravityChangeRotate", "easetype", iTween.EaseType.easeInQuad));
				}
			}
			#endregion
			#region 分身
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 2) {
				// 2: AvatarImage   -> 分身

				AvatarPlayer.SetActiveRecursively (true);
				
				if( !isAvatarPlayer ){
					isAvatarPlayerAnime = true;
					this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
					AvatarPlayer.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
					iTween.MoveTo (AvatarPlayer, iTween.Hash ("y", this.transform.position.y + 2.0f, "time", 0.2f, "oncomplete", "CompleteAvatarPlayerAnime", "oncompletetarget", gameObject));
				} else {
					AvatarPlayer.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2.0f);
				}
				
				isAvatarPlayer = true;
				
				if( PlayerControll2D.isRight ){
					AvatarPlayer.transform.localScale = new Vector2( 0.135f, 0.135f);
				} else {
					AvatarPlayer.transform.localScale = new Vector2(-0.135f, 0.135f);
				}
			}
			#endregion
			#region 飛行
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 3) {
				// 3: FlyImage      -> 飛行
				isFlying = false;
			}
			#endregion
			#region 時間操作
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 4) {
				// 4: ChronoImage   -> 時間操作
				isChronoUse  = true;
				isChronoHigh = false;
				isChronoSlow = true;
				TimeEffectObj.SetActiveRecursively (true);
				TimeEffectObj.transform.position = new Vector2 (transform.position.x, transform.position.y);

				if( !isWorldSpeedUpdate ){
					isWorldSpeedUpdate = true;
					iTween.ValueTo (gameObject, iTween.Hash ("from", WorldTimerControll.WorldSpeed, "to", 0.2f, "time", 1.0f, "onUpdate", "UpdateWorldSpeed", "onComplete", "CompleteWorldSpeed"));
				}
			}
			#endregion
			#region 念力
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 5) {
				// 5: PsychoImage   -> 念力
				isPsychoEffect = true;
				PsychoEffectRotate -= Time.deltaTime;
				PsychoEffectObj.SetActiveRecursively (true);
				PsychoEffectObj.transform.position = new Vector2 (transform.position.x, transform.position.y);
				PsychoEffectObj.transform.Rotate(new Vector3(0,0,PsychoEffectRotate));
			}
			#endregion
			#region 魔法
			if (AbilityChange.NowSlotAbilitiesNumList[AbilityChange.NowSlotAbilitiesNum] == 6) {
				// 6: MagicImage    -> 魔法
				FireShield.SetActiveRecursively (true);
				FireShield.transform.position = new Vector2 (transform.position.x, transform.position.y);
			}
			#endregion
		} else {
			#region 全ての能力に関するフラグを下ろす
			// 全ての能力に関するフラグを下ろす
			IsNotAllAbility ();
			#endregion
		}
		#endregion

		#region Shot(○)ボタン押下時
		if (Input.GetButtonDown ("Shot") && !PlayerTouchObject.isGateMove && !SavePoint.isSaving) {

			// ショット長押し時間をリセット
			ShotButtonPushTime = 0.0f;

			//--- Shot(○)ボタン押下時 ---//
			if (isPunchAbility) {
				// パンチアビリティならパンチを表示
				PunchObj.SetActiveRecursively (true);
			} else {
				//--- ショットアビリティならショットを表示	 ---//

				// 分身能力かつ、手裏剣が一定以上表示されていなければ手裏剣を表示
				if( isAvatarUse && !ShurikenControll.ShurikenCreateOver ){
					// 向いている方向に応じて、生成する位置を変更
					if( PlayerControll2D.isRight ){
						Instantiate(Shruiken, new Vector2(this.transform.position.x + 0.8f, this.transform.position.y), Quaternion.identity);
						if( isAvatarPlayer ){
							Instantiate(Shruiken, new Vector2(this.transform.position.x + 0.8f, this.transform.position.y + 2.0f), Quaternion.identity);
						}
					} else {
						Instantiate(Shruiken, new Vector2(this.transform.position.x - 0.8f, this.transform.position.y), Quaternion.identity);
						if( isAvatarPlayer ){
							Instantiate(Shruiken, new Vector2(this.transform.position.x - 0.8f, this.transform.position.y + 2.0f), Quaternion.identity);
						}
					}
				}

				// 飛行能力
				if( isFlyUse ){
					if( PlayerControll2D.isRight ){
						Instantiate(Feather, new Vector2(this.transform.position.x + 0.8f, this.transform.position.y), Quaternion.identity);
					} else {
						Instantiate(Feather, new Vector2(this.transform.position.x - 0.8f, this.transform.position.y), Quaternion.identity);
					}
				}

				// 念力能力かつ、サイコボールが一定以上表示されていなければサイコボールを表示
				if( isPsychoUse && !PsychoBallControll.PsychoBallCreateOver ){
					// 向いている方向に応じて、生成する位置を変更
					if( PlayerControll2D.isRight ){
						Instantiate(PsychoBall, new Vector2(this.transform.position.x + 0.8f, this.transform.position.y), Quaternion.identity);
					} else {
						Instantiate(PsychoBall, new Vector2(this.transform.position.x - 0.8f, this.transform.position.y), Quaternion.identity);
					}
				}

				// 魔法能力かつ、アイスランスが一定以上表示されていなければアイスランスを表示
				if( isMagicUse && !IceLanceControll.IceLanceCreateOver){
					if( PlayerControll2D.isRight ){
						Instantiate(IceLance, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.8f), Quaternion.identity);
						Instantiate(IceLance, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y + 0.0f), Quaternion.identity);
						Instantiate(IceLance, new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - 0.8f), Quaternion.identity);
					} else {
						Instantiate(IceLance, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y + 0.8f), Quaternion.identity);
						Instantiate(IceLance, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y + 0.0f), Quaternion.identity);
						Instantiate(IceLance, new Vector2(this.transform.position.x - 0.5f, this.transform.position.y - 0.8f), Quaternion.identity);
					}
				}

			}
		} else {
			// ショットボタンを押してない場合はパンチもショットも非表示
			// PunchObj.SetActiveRecursively (false);
		}
		#endregion

		#region Shot(○)ボタン押し貯めている間
		if (Input.GetButton ("Shot") && !PlayerTouchObject.isGateMove && !SavePoint.isSaving) {
			ShotButtonPushTime += Time.deltaTime;
			if( ShotButtonPushTime >= チャージ時間 ){
				ShotButtonPushTime = チャージ時間;
				if( isPsychoUse ){
					ChargePsychoBall.SetActiveRecursively (true);
					if( PlayerControll2D.isRight ){
						ChargePsychoBall.transform.localPosition = new Vector2(4.0f, 0);
					} else {
						ChargePsychoBall.transform.localPosition = new Vector2(4.0f, 0);
					}
					ChargePsychoBall.transform.localScale = new Vector2(2.1f, 2.1f);
				}
			} else if( ShotButtonPushTime >= チャージ時間/2 ){
				if( isPsychoUse ){
					ChargePsychoBall.SetActiveRecursively (true);
					if( PlayerControll2D.isRight ){
						ChargePsychoBall.transform.localPosition = new Vector2(4.0f, 0);
					} else {
						ChargePsychoBall.transform.localPosition = new Vector2(4.0f, 0);
					}
					ChargePsychoBall.transform.localScale = new Vector2(1.4f, 1.4f);
				}
			} else {
				ChargePsychoBall.SetActiveRecursively (false);
			}
		}
		#endregion

		#region Shot(○)ボタンを離した時
		if (Input.GetButtonUp ("Shot") && !PlayerTouchObject.isGateMove && !SavePoint.isSaving) {

			// チャージを解除
			ChargePsychoBall.SetActiveRecursively (false);

			// 一定以上チャージしたらビッグサイコボール
			if( ShotButtonPushTime >= チャージ時間 ){
				// 念力能力かつ、ビッグサイコボールが一定以上表示されていなければビッグサイコボールを表示
				if( isPsychoUse && !PsychoBallControll.BigPsychoBallCreateOver){
					// 向いている方向に応じて、生成する位置を変更
					if( PlayerControll2D.isRight ){
						Instantiate(BigPsychoBall, new Vector2(this.transform.position.x + 0.8f, this.transform.position.y), Quaternion.identity);
					} else {
						Instantiate(BigPsychoBall, new Vector2(this.transform.position.x - 0.8f, this.transform.position.y), Quaternion.identity);
					}
				}
			} else if( ShotButtonPushTime >= チャージ時間/2 && ShotButtonPushTime < チャージ時間){
				// チャージが足りなかったらサイコボール
				ShotButtonPushTime = 0.0f;

				// 念力能力かつ、サイコボールが一定以上表示されていなければサイコボールを表示
				if( isPsychoUse && !PsychoBallControll.PsychoBallCreateOver ){
					// 向いている方向に応じて、生成する位置を変更
					if( PlayerControll2D.isRight ){
						Instantiate(PsychoBall, new Vector2(this.transform.position.x + 0.8f, this.transform.position.y), Quaternion.identity);
					} else {
						Instantiate(PsychoBall, new Vector2(this.transform.position.x - 0.8f, this.transform.position.y), Quaternion.identity);
					}
				}
			}
		} 
		if(PlayerTouchObject.isGateMove || SavePoint.isSaving || PlayerGameOver.isGameOver) {
			// セーブ中やゲート移動、ゲームオーバー時は、チャージや使用している能力を停止
			ShotButtonPushTime = 0.0f;

			isPsychoEffect = false;
			PsychoEffectRotate = 0;
			ChargePsychoBall.SetActiveRecursively (false);
			PsychoEffectObj.SetActiveRecursively (false);
		}
		#endregion
	}

	#region 全ての能力に関するフラグを下ろす関数
	//--- 全ての能力に関するフラグを下ろす関数 ---//
	void IsNotAllAbility(){

		#region 肉体強化能力
		// 肉体強化モードを解除
		isStrengthMode = false;
		StrengthEffectObj.SetActiveRecursively (false);
		#endregion

		#region 重力操作能力
		// 重力操作解除
		isGravityChange = false;
		this.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
		if( isGravityChangeAnime ){	
			isGravityChangeAnime = false;
			isGravityChangeRotate = true;
			iTween.ScaleTo (GravityChangeEffectObj, iTween.Hash ("x", 0.0f, "y", 0.0f, "time", 0.3f, "oncompletetarget", gameObject, "oncomplete", "CompleteGravityChangeAnime", "easetype", iTween.EaseType.easeInQuad));
			iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "time", 0.3f,"oncomplete", "CompleteGravityChangeRotate", "easetype", iTween.EaseType.easeInQuad));
		}
		#endregion
		
		#region 分身能力
		if( isAvatarPlayer ){
			isAvatarPlayerAnime = true;
			this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
			iTween.MoveTo (AvatarPlayer, iTween.Hash ("x", this.transform.position.x, "y", this.transform.position.y, "time", 0.2f, "oncomplete", "CompleteAvatarFadeOutPlayerAnime", "oncompletetarget", gameObject));
		}
		isAvatarPlayer = false;
		#endregion
		
		#region 飛行能力
		// 飛行能力
		isFlying = false;
		#endregion

		#region 時間操作能力
		// 時間操作能力
		isChronoHigh = false;
		isChronoSlow = false;
		TimeEffect.TimeEffectNeedleRotateSpeed = TimeEffect.RotateSpeedDefaultStatic;
		TimeEffect.TimeEffectNeedleNowRotate   = 0;
		if( !isWorldSpeedUpdate ){
			isWorldSpeedUpdate = true;
			iTween.ValueTo (gameObject, iTween.Hash ("from", WorldTimerControll.WorldSpeed, "to", 1.0f, "time", 1.0f, "onUpdate", "UpdateWorldSpeed", "onComplete", "CompleteWorldSpeed"));
		}
		TimeEffectObj.SetActiveRecursively (false);
		#endregion

		#region 念力能力
		isPsychoEffect = false;
		PsychoEffectRotate = 0;
		PsychoEffectObj.SetActiveRecursively (false);
		#endregion

		#region 魔法能力
		FireShield.SetActiveRecursively (false);
		#endregion
	}
	#endregion


	void CompleteGravityChangeRotate(){
		isGravityChangeRotate = false;
	}
	void CompleteGravityChangeAnime(){
		GravityChangeEffectObj.SetActiveRecursively (false);
	}

	
	void CompleteAvatarPlayerAnime(){
		this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
		isAvatarPlayerAnime = false;
	}

	void CompleteAvatarFadeOutPlayerAnime(){
		this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1.0f);
		isAvatarPlayerAnime = false;
		AvatarPlayer.SetActiveRecursively (false);
	}


	void UpdateWorldSpeed(float WorldSpeed){
		WorldTimerControll.WorldSpeed = WorldSpeed;
	}
	void CompleteWorldSpeed (){
		isWorldSpeedUpdate = false;
	}
}
