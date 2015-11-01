using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveScreenUIControll : MonoBehaviour {

	//--- オブジェクト ---//
	// GameTitleCanvas
	private GameObject GameTitleCanvasObj;
	private GameObject GameTitlePanelObj;
	private GameObject PushButtonPanelObj;
	private GameObject CopyrigthPanelObj;
	// SaveDataCanvas
	private GameObject SaveDataCanvasObj;
	private GameObject TitlePanelObj;
	private GameObject SaveDataButton1Obj;
	private GameObject SaveDataButton2Obj;
	private GameObject SaveDataButton3Obj;
	private GameObject CopyButtonObj;
	private GameObject DeleteButtonObj;
	// DeleteCanvas
	private GameObject DeleteCanvas;
	
	private GameObject EventSystemObj;

	//--- 透過処理用変数 ---//
	private float GameTitlePanelAlpha;
	private float PushButtonPanelAlpha;
	private float TitlePanelAlpha;
	private float SaveDataButton1Alpha;
	private float SaveDataButton2Alpha;
	private float SaveDataButton3Alpha;
	private float CopyButtonAlpha;
	private float DeleteButtonAlpha;

	//--- フラグ管理 ---//
	public static bool isGoToSaveData = false;
	private bool  isCanvasActive = false;
	private bool  isLoadSaveDataCanvas = false;

	//--- デフォルトで選択されているボタンを格納 ---//
	public Animator DefaltButtonAnime;
	public Button   DefaltSelectButton;
	
	void Start () {
		//--- 各ゲームオブジェクトを取得 ---//
		GameTitleCanvasObj = GameObject.Find ("GameTitleCanvas");
		GameTitlePanelObj  = GameObject.Find ("GameTitlePanel");
		PushButtonPanelObj = GameObject.Find ("PushButtonPanel");
		CopyrigthPanelObj  = GameObject.Find ("CopyrightPanel");
		SaveDataCanvasObj  = GameObject.Find ("SaveDataCanvas");
		TitlePanelObj      = GameObject.Find ("TitlePanel");
		SaveDataButton1Obj = GameObject.Find ("SaveDataButton1");
		SaveDataButton2Obj = GameObject.Find ("SaveDataButton2");
		SaveDataButton3Obj = GameObject.Find ("SaveDataButton3");
		CopyButtonObj      = GameObject.Find ("CopyButton");
		DeleteButtonObj    = GameObject.Find ("DeleteButton");
		DeleteCanvas       = GameObject.Find ("DeleteCanvas");
		EventSystemObj     = GameObject.Find ("EventSystem");

		//--- 透過処理用変数 ---//
		GameTitlePanelAlpha  = 1.2f;
		PushButtonPanelAlpha = 2.0f;
		TitlePanelAlpha      = -0.5f;
		SaveDataButton1Alpha = -1.0f;
		SaveDataButton2Alpha = -1.5f;
		SaveDataButton3Alpha = -2.0f;
		CopyButtonAlpha      = -2.5f;
		DeleteButtonAlpha    = -3.0f;

		//--- 活性.非活性にするゲームオブジェクト ---//
		GameTitleCanvasObj.SetActiveRecursively(true);
		SaveDataCanvasObj.SetActiveRecursively (false);
		EventSystemObj.SetActiveRecursively (false);
		DeleteCanvas.SetActiveRecursively (false);

		// フラグの初期化
		isGoToSaveData = false;
		isCanvasActive = false;
		isLoadSaveDataCanvas = false;
	}

	void Update () {

		//--- セーブデータ画面へ遷移する際のアニメーション処理 ---//
		if (isGoToSaveData) {
			GameTitlePanelAlpha  -= 1.0f * Time.deltaTime;
			PushButtonPanelAlpha -= 4.0f * Time.deltaTime;
			GameTitlePanelObj.GetComponent<Image>().color  = new Color(1.0f, 1.0f, 1.0f, GameTitlePanelAlpha);
			PushButtonPanelObj.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, PushButtonPanelAlpha);
			CopyrigthPanelObj.GetComponent<Image>().color  = new Color(1.0f, 1.0f, 1.0f, PushButtonPanelAlpha);

			if( GameTitlePanelAlpha < -0.5f ){ 

				//--- 遷移後、タイトル画面を非活性にし、セーブ選択画面を活性する。 ---//
				if ( !isLoadSaveDataCanvas ) {
					isLoadSaveDataCanvas = true;
					GameTitleCanvasObj.SetActiveRecursively(false); 
					SaveDataCanvasObj.SetActiveRecursively (true);
					
					// セーブデータ情報を画面に反映
					SaveDataStatusControll.SaveDataStatusSetting();
				}

				//--- 不透明化アニメーション（最後のボタンのアニメーション終了後、下記の処理を実行させない） ---//
				if ( DeleteButtonAlpha < 1.0f ){
					// セーブ選択画面タイトル
					TitlePanelAlpha += 4.0f * Time.deltaTime;
					SaveDataUIAnime(TitlePanelObj, TitlePanelAlpha);
					// セーブファイル1
					SaveDataButton1Alpha += 2.0f * Time.deltaTime;
					SaveDataUIAnime(SaveDataButton1Obj, SaveDataButton1Alpha);
					// セーブファイル2
					SaveDataButton2Alpha += 2.0f * Time.deltaTime;
					SaveDataUIAnime(SaveDataButton2Obj, SaveDataButton2Alpha);
					// セーブファイル3
					SaveDataButton3Alpha += 2.0f * Time.deltaTime;
					SaveDataUIAnime(SaveDataButton3Obj, SaveDataButton3Alpha);
					// コピーボタン
					CopyButtonAlpha += 2.0f * Time.deltaTime;
					SaveDataUIAnime(CopyButtonObj, CopyButtonAlpha);
					// 削除ボタン
					DeleteButtonAlpha += 2.0f * Time.deltaTime;
					SaveDataUIAnime(DeleteButtonObj, DeleteButtonAlpha, true);
				}
			} 
		}

		//--- タイトル画面からセーブ選択画面に遷移しいていたら、以下の処理を行わない ---//
		if (isGoToSaveData) { return; }
		// スタートボタン(“Enter”キー)を押したらセーブデータ選択画面へ
		if (Input.GetButtonDown ("Submit") && PushButtonPanel.isPushButtonAnimationEnd) { 
			isGoToSaveData = true; 
			iTween.ScaleTo (PushButtonPanelObj, iTween.Hash ("x", 0, "y", 0, "time", 0.5f, "easetype", iTween.EaseType.easeInBack));
		}
	}

	void SaveDataUIAnime(GameObject TargetGameObj, float AlphaValue, bool lastAnime = false){
		
		if( AlphaValue > 1.0f && !lastAnime) { return; }
		if( AlphaValue > 1.0f &&  lastAnime) { 
			// デフォルトの選択ファイル(ファイル1)を選択(HighLighted)状態にする
			EventSystemObj.SetActiveRecursively (true);
			DefaltButtonAnime.SetTrigger (DefaltSelectButton.animationTriggers.highlightedTrigger);
			return;
		}

		// 親オブジェクトの不透明化処理処理
		TargetGameObj.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, AlphaValue);

		// 全ての子オブジェクトを取得
		Transform[]	ChildList = TargetGameObj.transform.GetComponentsInChildren< Transform>();
		
		//--- リストから子オブジェクトを取り出し、コンポーネント毎に不透明化処理 ---//
		foreach (Transform transform in ChildList) {
			if( transform.tag == "ImageUI") {
				transform.GetComponent<Image> ().color = new Color (transform.GetComponent<Image> ().color.r, transform.GetComponent<Image> ().color.g, transform.GetComponent<Image> ().color.b, AlphaValue);
			}
			if( transform.tag == "WhitePanelUI") {
				if( !CopyButton.isCopyActive && transform.name == "WhitePanelCopy" ){
					transform.GetComponent<Image> ().color = new Color (transform.GetComponent<Image> ().color.r, transform.GetComponent<Image> ().color.g, transform.GetComponent<Image> ().color.b, AlphaValue-0.5f);
				}
				if( !DeleteButton.isDeleteActive && transform.name == "WhitePanelDelete" ){
					transform.GetComponent<Image> ().color = new Color (transform.GetComponent<Image> ().color.r, transform.GetComponent<Image> ().color.g, transform.GetComponent<Image> ().color.b, AlphaValue-0.5f);
				}
			}
			if( transform.tag == "TextUI") {
				transform.GetComponent<Text> ().color = new Color  (0.43137255f, 0.29411765f, 0.0f, AlphaValue);
			}
		}
	}
}
