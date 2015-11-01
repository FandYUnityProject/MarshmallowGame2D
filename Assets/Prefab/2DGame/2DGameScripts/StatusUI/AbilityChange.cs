using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* --- 能力番号:オブジェクト名 ---
 * 0: StrengthImage -> 肉体強化
 * 1: GravityImage  -> 重力操作
 * 2: AvatarImage   -> 分身
 * 3: FlyImage      -> 飛行
 * 4: ChronoImage   -> 時間操作
 * 5: PsychoImage   -> 念力
 * 6: MagicImage    -> 魔法
 */

/* --- プレイヤーが能力を得た時に実行させる処理 ---
 * PlayerPrefs.SetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability[対応する能力番号]", 1);
 * AbilityChange.isGetAbility[対応する能力番号] = true;
 * AbilityChange.[得た能力に対応するオブジェクト名].SetActiveRecursively (true);
 * AbilityChange.[得た能力に対応するオブジェクト名].transform.localPosition = new Vector2(AbilityChange.GetAbility * AbilityChange.SlotImageWidth, 0);
 * AbilityChange.NowSlotAbilities.Add(AbilityChange.[得た能力に対応するオブジェクト名];
 * AbilityChange.SetSlotCount++;
 * AbilityChange.GetAbility++;
 * PlayerPrefs.SetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility", PlayerPrefs.GetInt("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility") + 1);
 */

public class AbilityChange : MonoBehaviour {

	#region 変数宣言
	//--- デバッグモード ---//
	public bool isDebugMode 	= false;
	public bool isAllAbillity   = false;

	//--- オブジェクト ---//
	// LRボタン関連
	private GameObject TriggerLR;
	private GameObject L_OFF;
	private GameObject L_ON;
	private GameObject R_OFF;
	private GameObject R_ON;
	// 能力スロット関連
	public static GameObject Abilities;
	public static GameObject NormalImage;
	public static GameObject AvatarImage;
	public static GameObject ChronoImage;
	public static GameObject FlyImage;
	public static GameObject GravityImage;
	public static GameObject MagicImage;
	public static GameObject PsychoImage;
	public static GameObject StrengthImage;
	public static GameObject[] GetAbilities;
	// プレイヤー
	private GameObject PlayerObj;

	//--- フラグ管理 ---//
	public  static bool[] isGetAbility  = new bool[7]{false, false, false, false, false, false, false};	// 能力獲得フラグ
	private bool   isAbilityChangeAnime = false;	// LRトリガーボタン押下時のアニメーションフラグ

	//--- 取得アビリティ関連 ---//
	public static int GetAbility = 0;	// 獲得アビリティ数

	//--- スロット関連 ---// 
	public static List<GameObject> NowSlotAbilities = new List<GameObject>();
	public static List<int> NowSlotAbilitiesNumList = new List<int>();
	public static int NowSlotAbilitiesNum   = 0;	// 現在選択している能力番号
	private float SlotSlideSpeed        	= 0.5f;	// スロットのスライド速度
	public static int SelectAbilityNumber   = 0;	// 現在表示している能力番号
	public static int SetSlotCount    		= 0;	// スロットにある(所持している)能力の数
	public static int SlotImageWidth  		= 140;	// スロットのイメージの横幅
	#endregion

	void Awake(){

		#region 現在プレイしているセーブデータの番号をデッバッグログで表示
		// 現在プレイしているセーブデータの番号を表示
		Debug.Log("PlaySaveDataNum: " + PlaySaveDataNumber.NowPlaySaveDataNumber);
		#endregion
	}

	void Start () {

		#region デバッグモード時に使用
		//--- デバッグモード ---//
		if (isDebugMode) {
			if( isAllAbillity ) {
				for (int i=0; i<=6; i++) {
					PlayerPrefs.SetInt("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability" + i, 1);
					PlayerPrefs.SetInt("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility" , 7);
				}
			} else {
				for (int i=0; i<=6; i++) {
					PlayerPrefs.SetInt("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability" + i, 0);
					PlayerPrefs.SetInt("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility" , 0);
				}
			}
		}
		#endregion

		#region 各ゲームオブジェクトを取得
		//--- 各ゲームオブジェクトを取得 ---//
		// LRボタンイメージ
		TriggerLR  = GameObject.Find ("TriggerLR");
		L_OFF      = GameObject.Find ("L_OFF");
		L_ON       = GameObject.Find ("L_ON");
		R_OFF      = GameObject.Find ("R_OFF");
		R_ON       = GameObject.Find ("R_ON");

		// 各能力のイメージ
		Abilities     = GameObject.Find ("Abilities");
		NormalImage   = GameObject.Find ("NormalImage");
		AvatarImage   = GameObject.Find ("AvatarImage");
		ChronoImage   = GameObject.Find ("ChronoImage");
		FlyImage      = GameObject.Find ("FlyImage");
		GravityImage  = GameObject.Find ("GravityImage");
		MagicImage    = GameObject.Find ("MagicImage");
		PsychoImage   = GameObject.Find ("PsychoImage");
		StrengthImage = GameObject.Find ("StrengthImage");

		// プレイヤー
		PlayerObj = GameObject.Find ("Player");
		#endregion

		#region 各アビリティを配列で管理
		// 各アビリティを配列で管理
		GetAbilities = new GameObject[7]{StrengthImage, GravityImage, AvatarImage, FlyImage, ChronoImage, PsychoImage, MagicImage};
		#endregion

		#region セーブデータから取得しているアビリティフラグを取得
		// セーブデータから取得しているアビリティフラグを取得
		GetAbility = PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility");
		Debug.Log ("GetAbility: " + GetAbility);
		for (int i=0; i<=6; i++) {
			if (PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability" + i) == 1) {
				isGetAbility[i] = true;
				// Debug.Log ("isGetAbility" + i + ": " + isGetAbility[i]);
			}
			if (PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability" + i) != 1) {
				isGetAbility[i] = false;
				// Debug.Log ("isGetAbility" + i + ": " + isGetAbility[i]);
			}
		}
		#endregion

		#region 非表示にするオブジェクト（プレイヤー以外）
		//--- LRボタンを非表示にする ---//
		TriggerLR.SetActiveRecursively (false);
		L_OFF.SetActiveRecursively (false);
		L_ON.SetActiveRecursively (false);
		R_OFF.SetActiveRecursively (false);
		R_ON.SetActiveRecursively (false);

		//--- 各能力イメージを非表示にする ---//
		NormalImage.SetActiveRecursively (false);
		StrengthImage.SetActiveRecursively (false);
		GravityImage.SetActiveRecursively (false);
		AvatarImage.SetActiveRecursively (false);
		FlyImage.SetActiveRecursively (false);
		ChronoImage.SetActiveRecursively (false);
		PsychoImage.SetActiveRecursively (false);
		MagicImage.SetActiveRecursively (false);
		#endregion

		#region 何も能力を取得していなければ、Normalイメージを表示、それ以外はNormalイメージを非表示
		// 何も能力を取得していなければ、Normalイメージを表示、それ以外はNormalイメージを非表示
		if (GetAbility == 0) {
			NormalImage.SetActiveRecursively (true);
		} else {
			NormalImage.SetActiveRecursively (false);
			
			int x=0;	// アイテムイメージの位置
			
			// 現在取得しているアビリティを画面に反映
			for (int i=0; i<isGetAbility.Length; i++){
				if( isGetAbility[i] ){
					GetAbilities[i].SetActiveRecursively (true);
					GetAbilities[i].transform.localPosition = new Vector2(x * SlotImageWidth, 0);
					x++;
					// 現在スロットに配置されているアビリティを格納
					NowSlotAbilities.Add(GetAbilities[i]);
					NowSlotAbilitiesNumList.Add(i);
					SetSlotCount++;
				}
			}
		}
		#endregion
	}

	void Update () {

		#region 能力変更（LRボタン押下）時はオートアビリティを一度解除する
		//--- 能力変更（LRボタン押下）時はオートアビリティを一度解除する ---//
		if (!isAbilityChangeAnime && (Input.GetButton ("L1") || Input.GetButton ("R1"))) {
			
			// 壁蹴りアビリティの解除
			PlayerControll2D.isJumping = true;
			PlayerControll2D.isBeforeTouchRightWall = false;
			PlayerControll2D.isBeforeTouchLeftWall  = false;
			PlayerUseAbility.isWallKickAbility      = false;
			if( !PlayerControll2D.isLaddar ){
				PlayerObj.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			}
		}
		#endregion

		#region アビリティを２つ以上獲得した場合のLRボタン表示処理
		//--- アビリティを２つ以上獲得したらLRボタンを表示する ---//
		if( GetAbility >= 2 ){
			TriggerLR.SetActiveRecursively (true);

			#region L1ボタン押下時のLボタンイメージの処理
			// L1ボタンを押してる時はL_ONを表示、それ以外はL_OFFを表示
			if( Input.GetButton ("L1")){
				L_OFF.SetActiveRecursively (false);
				L_ON.SetActiveRecursively  (true);

				// L押下時にアビリティ変更アニメーションが動作してない場合は、アニメーションさせる
				if( !isAbilityChangeAnime ){
					isAbilityChangeAnime = true;

					// TODO 能力変更時にアニメーションが入ると思うので、コルーチンか何かで処理追加

					// もし現在設定している能力が1番目(スロットの左端)以外なら、
					if( SelectAbilityNumber != 0 ){
						// 設定番号を1下げてスロットをスライドさせる
						SelectAbilityNumber--;
						NowSlotAbilitiesNum--;
						iTween.MoveTo (Abilities, iTween.Hash ("x", SelectAbilityNumber * -SlotImageWidth, "time", SlotSlideSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "CompleteSlotSlide", "oncompletetarget", gameObject));
					} else {				
						// もし現在設定している能力が1番目(スロットの左端)なら、
						// スロットの右端の能力を左端にセットし、能力設定画面を左にスライドする
						NowSlotAbilities[NowSlotAbilities.Count -1].transform.localPosition = new Vector2(-140, 0);
						iTween.MoveTo (Abilities, iTween.Hash ("x", SlotImageWidth, "time", SlotSlideSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "CompleteSlotSlideLeftEnd", "oncompletetarget", gameObject));
					}
				}
			} else {
				L_OFF.SetActiveRecursively (true);
				L_ON.SetActiveRecursively  (false);
			}
			#endregion

			#region R1ボタン押下時のLボタンイメージの処理
			// R1ボタンを押してる時はR_ONを表示、それ以外はR_OFFを表示
			if( Input.GetButton ("R1")){
				R_OFF.SetActiveRecursively (false);
				R_ON.SetActiveRecursively  (true);
				
				// R押下時にアビリティ変更アニメーションが動作してない場合は、アニメーションさせる
				if( !isAbilityChangeAnime ){
					isAbilityChangeAnime = true;

					// TODO 能力変更時にアニメーションが入ると思うので、コルーチンか何かで処理追加


					// もし現在設定している能力が右端の番号(SetSlotCount-1)以外なら、
					if( SelectAbilityNumber != (SetSlotCount-1) ){
						// 設定番号を1上げてスロットをスライドさせる
						SelectAbilityNumber++;
						NowSlotAbilitiesNum++;
						iTween.MoveTo (Abilities, iTween.Hash ("x", SelectAbilityNumber * -SlotImageWidth, "time", SlotSlideSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "CompleteSlotSlide", "oncompletetarget", gameObject));
					} else {				
						// もし現在設定している能力が右端の番号(SetSlotCount)なら、
						// スロットの左端の能力を右端にセットし、能力設定画面を右にスライドする
						NowSlotAbilities[0].transform.localPosition = new Vector2(SetSlotCount * SlotImageWidth, 0);
						iTween.MoveTo (Abilities, iTween.Hash ("x", SetSlotCount * -SlotImageWidth, "time", SlotSlideSpeed, "islocal", true, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "CompleteSlotSlideRightEnd", "oncompletetarget", gameObject));
					}
				}
			} else {
				R_OFF.SetActiveRecursively (true);
				R_ON.SetActiveRecursively  (false);
			}
			#endregion
		}
		#endregion
	}

	#region スロットアニメーション終了後の処理関数
	//--- スロットアニメーション終了後の処理関数 ---//
	private void CompleteSlotSlide(){
		// スロットの端でなければアニメーションフラグを下ろす
		Debug.Log("SlotNumber: " + SelectAbilityNumber);
		isAbilityChangeAnime = false;
	}
	#endregion

	#region スロットが左端でLボタン押下時のアニメーション終了後
	// スロットが左端でLボタン押下時のアニメーション終了後
	private void CompleteSlotSlideLeftEnd(){
		// 左端なら、現在選択している能力を右端の能力に変更
		SelectAbilityNumber = NowSlotAbilities.Count -1;
		NowSlotAbilitiesNum = NowSlotAbilities.Count -1;
		Debug.Log("SlotNumber: " + SelectAbilityNumber);
		NowSlotAbilities[NowSlotAbilities.Count -1].transform.localPosition = new Vector2((SetSlotCount - 1) * SlotImageWidth, 0);
		Abilities.transform.localPosition = new Vector2((SetSlotCount - 1) * -SlotImageWidth, 0);
		isAbilityChangeAnime = false;
	}
	#endregion

	#region スロットが右端でRボタン押下時のアニメーション終了後
	// スロットが右端でRボタン押下時のアニメーション終了後
	private void CompleteSlotSlideRightEnd(){
		// 右端なら、現在選択している能力を左端の能力に変更
		SelectAbilityNumber = 0;
		NowSlotAbilitiesNum = 0;
		Debug.Log("SlotNumber: " + SelectAbilityNumber);
		NowSlotAbilities[0].transform.localPosition = new Vector2(0, 0);
		Abilities.transform.localPosition = new Vector2(0, 0);
		isAbilityChangeAnime = false;
	}
	#endregion
}
