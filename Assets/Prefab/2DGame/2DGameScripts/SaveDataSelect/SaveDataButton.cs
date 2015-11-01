using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SaveDataButton : MonoBehaviour {

	//--- オブジェクト ---//
	private GameObject TitleText;
	private GameObject CopyName;
	private GameObject EventSystemObj;
	
	private GameObject SaveDataButton1;
	private GameObject SaveDataButton2;
	private GameObject SaveDataButton3;
	private GameObject CopyButtonObj;
	public static GameObject TargetDeleteObj;
	private GameObject DeleteButtonObj;
	private GameObject DeleteCanvas;
	private GameObject DeleteBackgroundPanel;
	private GameObject DeleteConfirmationMessage;
	private GameObject DeleteYesButtonObj;
	private GameObject DeleteYesName;
	private GameObject DeleteName;
	private GameObject DeleteNoButtonObj;
	
	private Image      BlackOutImage;
	
	private GameObject WhitePanelCopy;
	private GameObject WhitePanelDelete;

	private GameObject WhitePanel1;
	private GameObject WhitePanel2;
	private GameObject WhitePanel3;
	private GameObject TargetWhitePanel;

	
	//--- フラグ管理 ---//
	public static bool isOriginalCopySelect   = true;
	
	//--- コピー元セーブデータ保管変数 ---//
	public static int WorldNum;
	public static int FloorNum;
	public static int Ability0;
	public static int Ability1;
	public static int Ability2;
	public static int Ability3;
	public static int Ability4;
	public static int Ability5;
	public static int Ability6;
	public static int GetAbility;

	void Start () {
		TitleText   = GameObject.Find ("TitleText");
		CopyName    = GameObject.Find ("CopyName");
		EventSystemObj = GameObject.Find ("EventSystem");
		
		SaveDataButton1  = GameObject.Find ("SaveDataButton1");
		SaveDataButton2  = GameObject.Find ("SaveDataButton2");
		SaveDataButton3  = GameObject.Find ("SaveDataButton3");
		CopyButtonObj    = GameObject.Find ("CopyButton");
		DeleteButtonObj  = GameObject.Find ("DeleteButton");
		DeleteCanvas     = GameObject.Find ("DeleteCanvas");
		DeleteBackgroundPanel = GameObject.Find ("DeleteBackgroundPanel");
		DeleteConfirmationMessage = GameObject.Find ("DeleteConfirmationMessage");
		DeleteYesButtonObj = GameObject.Find ("DeleteYesButton");
		DeleteYesName = GameObject.Find ("DeleteYesName");
		DeleteNoButtonObj = GameObject.Find ("DeleteNoButton");
		
		BlackOutImage   = GameObject.Find ("BlackOut").GetComponent<Image> ();

		WhitePanelCopy   = GameObject.Find ("WhitePanelCopy");
		WhitePanelDelete = GameObject.Find ("WhitePanelDelete"); 

		WhitePanel1 = GameObject.Find ("WhitePanel1");
		WhitePanel2 = GameObject.Find ("WhitePanel2");
		WhitePanel3 = GameObject.Find ("WhitePanel3");

	}

	public void OnSaveDataButtonClick(){

		if (CopyButton.isCopyMode) {
			if( isOriginalCopySelect ){

				isOriginalCopySelect = false;

				// 一度全てのセーブファイルボタンを活性化する
				SaveDataButton1.GetComponent<Button>().enabled = true;
				SaveDataButton2.GetComponent<Button>().enabled = true;
				SaveDataButton3.GetComponent<Button>().enabled = true;
				WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
				WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
				WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

				// セーブデータがあるファイルは非活性にする
				if (PlayerPrefs.GetInt ("Save1_WorldNum") != 0) {

					// デフォルト値を再設定するためにEventSystemを再起動
					EventSystemObj.SetActiveRecursively (false);
					EventSystemObj.SetActiveRecursively (true);
					
					// 対象のボタンを非活性にし、ボタンのデフォルト値を変更
					WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
					SaveDataButton1.GetComponent<Button>().enabled = false;
					EventSystem.current.firstSelectedGameObject = SaveDataButton2;
					SaveDataButton2.GetComponent<Animator>().SetTrigger (SaveDataButton2.GetComponent<Button>().animationTriggers.highlightedTrigger);
					
					if (PlayerPrefs.GetInt ("Save2_WorldNum") != 0) { 
						
						// 対象のボタンを非活性にし、ボタンのデフォルト値を変更
						WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
						SaveDataButton2.GetComponent<Button>().enabled = false;
						EventSystem.current.firstSelectedGameObject = SaveDataButton3;
						SaveDataButton3.GetComponent<Animator>().SetTrigger (SaveDataButton3.GetComponent<Button>().animationTriggers.highlightedTrigger);
						
					} else if(PlayerPrefs.GetInt ("Save3_WorldNum") != 0) { 
						
						// 対象のボタンを非活性にし、ボタンのデフォルト値を変更
						WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
						SaveDataButton3.GetComponent<Button>().enabled = false;
						EventSystem.current.firstSelectedGameObject = SaveDataButton2;
						SaveDataButton2.GetComponent<Animator>().SetTrigger (SaveDataButton2.GetComponent<Button>().animationTriggers.highlightedTrigger);
						
						// コピーボタンの上ボタン押下時のボタンを変更
						Navigation navigation = CopyButtonObj.GetComponent<Button>().navigation;
						navigation.selectOnUp = SaveDataButton2.GetComponent<Button>();
						CopyButtonObj.GetComponent<Selectable>().navigation = navigation;
					}
				} else {
					
					// デフォルト値を再設定するためにEventSystemを再起動
					EventSystemObj.SetActiveRecursively (false);
					EventSystemObj.SetActiveRecursively (true);

					// 対象のボタンを活性にし、ボタンのデフォルト値を変更
					SaveDataButton1.GetComponent<Button>().enabled = true;
					EventSystem.current.firstSelectedGameObject = SaveDataButton1;
					SaveDataButton1.GetComponent<Animator>().SetTrigger (SaveDataButton1.GetComponent<Button>().animationTriggers.highlightedTrigger);
					// コピーボタンの上ボタン押下時のボタンを変更
					Navigation navigation = CopyButtonObj.GetComponent<Button>().navigation;
					navigation.selectOnUp = SaveDataButton1.GetComponent<Button>();
					CopyButtonObj.GetComponent<Selectable>().navigation = navigation;

					if (PlayerPrefs.GetInt ("Save3_WorldNum") != 0) { 
						// 対象のボタンを非活性
						WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
						SaveDataButton3.GetComponent<Button>().enabled = false;
						
						// コピーボタンの上ボタン押下時のボタンを変更
						navigation = CopyButtonObj.GetComponent<Button>().navigation;
						navigation.selectOnUp = SaveDataButton2.GetComponent<Button>();
						CopyButtonObj.GetComponent<Selectable>().navigation = navigation;
					}
					if (PlayerPrefs.GetInt ("Save2_WorldNum") != 0) { 
						// 対象のボタンを非活性
						WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
						SaveDataButton2.GetComponent<Button>().enabled = false; 
						
						// コピーボタンの上ボタン押下時のボタンを変更
						navigation = CopyButtonObj.GetComponent<Button>().navigation;
						navigation.selectOnUp = SaveDataButton1.GetComponent<Button>();
						CopyButtonObj.GetComponent<Selectable>().navigation = navigation;
					}
				}
				if(PlayerPrefs.GetInt ("Save3_WorldNum") == 0) {
					// コピーボタンの上ボタン押下時のボタンを変更
					Navigation navigation = CopyButtonObj.GetComponent<Button>().navigation;
					navigation.selectOnUp = SaveDataButton3.GetComponent<Button>();
					CopyButtonObj.GetComponent<Selectable>().navigation = navigation;
				}

				// 画面タイトルのテキスト変更
				TitleText.GetComponent<Text> ().text = "コピー先のファイルを選んでください";

				if( this.gameObject.name == "SaveDataButton1" ){
					Debug.Log("Copy1");
					WorldNum   = PlayerPrefs.GetInt ("Save1_WorldNum");
					FloorNum   = PlayerPrefs.GetInt ("Save1_FloorNum");
					Ability0   = PlayerPrefs.GetInt ("Save1_Ability0");
					Ability1   = PlayerPrefs.GetInt ("Save1_Ability1");
					Ability2   = PlayerPrefs.GetInt ("Save1_Ability2");
					Ability3   = PlayerPrefs.GetInt ("Save1_Ability3");
					Ability4   = PlayerPrefs.GetInt ("Save1_Ability4");
					Ability5   = PlayerPrefs.GetInt ("Save1_Ability5");
					Ability6   = PlayerPrefs.GetInt ("Save1_Ability6");
					GetAbility = PlayerPrefs.GetInt ("Save1_GetAbility");
				} else if( this.gameObject.name == "SaveDataButton2" ){
					Debug.Log("Copy2");
					WorldNum   = PlayerPrefs.GetInt ("Save2_WorldNum");
					FloorNum   = PlayerPrefs.GetInt ("Save2_FloorNum");
					Ability0   = PlayerPrefs.GetInt ("Save2_Ability0");
					Ability1   = PlayerPrefs.GetInt ("Save2_Ability1");
					Ability2   = PlayerPrefs.GetInt ("Save2_Ability2");
					Ability3   = PlayerPrefs.GetInt ("Save2_Ability3");
					Ability4   = PlayerPrefs.GetInt ("Save2_Ability4");
					Ability5   = PlayerPrefs.GetInt ("Save2_Ability5");
					Ability6   = PlayerPrefs.GetInt ("Save2_Ability6");
					GetAbility = PlayerPrefs.GetInt ("Save2_GetAbility");
				} else {
					Debug.Log("Copy3");
					WorldNum   = PlayerPrefs.GetInt ("Save3_WorldNum");
					FloorNum   = PlayerPrefs.GetInt ("Save3_FloorNum");
					Ability0   = PlayerPrefs.GetInt ("Save3_Ability0");
					Ability1   = PlayerPrefs.GetInt ("Save3_Ability1");
					Ability2   = PlayerPrefs.GetInt ("Save3_Ability2");
					Ability3   = PlayerPrefs.GetInt ("Save3_Ability3");
					Ability4   = PlayerPrefs.GetInt ("Save3_Ability4");
					Ability5   = PlayerPrefs.GetInt ("Save3_Ability5");
					Ability6   = PlayerPrefs.GetInt ("Save3_Ability6");
					GetAbility = PlayerPrefs.GetInt ("Save3_GetAbility");
				}
			} else {

				isOriginalCopySelect = true;
				
				// 画面タイトルのテキスト変更
				TitleText.GetComponent<Text> ().text = "コピーするファイルを選んでください";

				if( this.gameObject.name == "SaveDataButton1" ){
					Debug.Log("Paste1");
					PlayerPrefs.SetInt ("Save1_WorldNum", WorldNum);
					PlayerPrefs.SetInt ("Save1_FloorNum", FloorNum);
					PlayerPrefs.SetInt ("Save1_Ability0", Ability0);
					PlayerPrefs.SetInt ("Save1_Ability1", Ability1);
					PlayerPrefs.SetInt ("Save1_Ability2", Ability2);
					PlayerPrefs.SetInt ("Save1_Ability3", Ability3);
					PlayerPrefs.SetInt ("Save1_Ability4", Ability4);
					PlayerPrefs.SetInt ("Save1_Ability5", Ability5);
					PlayerPrefs.SetInt ("Save1_Ability6", Ability6);
					PlayerPrefs.SetInt ("Save1_GetAbility", GetAbility);

					TargetWhitePanel = WhitePanel1;
					iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 2.0f, "time", 1.0f, "onUpdate", "UpdateWhitePanelAlphaUp","onComplete", "CompleteWhitePanelAlphaUp", "oncompletetarget", gameObject));
				} else if( this.gameObject.name == "SaveDataButton2" ){
					Debug.Log("Paste2");
					PlayerPrefs.SetInt ("Save2_WorldNum", WorldNum);
					PlayerPrefs.SetInt ("Save2_FloorNum", FloorNum);
					PlayerPrefs.SetInt ("Save2_Ability0", Ability0);
					PlayerPrefs.SetInt ("Save2_Ability1", Ability1);
					PlayerPrefs.SetInt ("Save2_Ability2", Ability2);
					PlayerPrefs.SetInt ("Save2_Ability3", Ability3);
					PlayerPrefs.SetInt ("Save2_Ability4", Ability4);
					PlayerPrefs.SetInt ("Save2_Ability5", Ability5);
					PlayerPrefs.SetInt ("Save2_Ability6", Ability6);
					PlayerPrefs.SetInt ("Save2_GetAbility", GetAbility);

					TargetWhitePanel = WhitePanel2;
					iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 2.0f, "time", 1.0f, "onUpdate", "UpdateWhitePanelAlphaUp","onComplete", "CompleteWhitePanelAlphaUp", "oncompletetarget", gameObject));
				} else {
					Debug.Log("Paste3");
					PlayerPrefs.SetInt ("Save3_WorldNum", WorldNum);
					PlayerPrefs.SetInt ("Save3_FloorNum", FloorNum);
					PlayerPrefs.SetInt ("Save3_Ability0", Ability0);
					PlayerPrefs.SetInt ("Save3_Ability1", Ability1);
					PlayerPrefs.SetInt ("Save3_Ability2", Ability2);
					PlayerPrefs.SetInt ("Save3_Ability3", Ability3);
					PlayerPrefs.SetInt ("Save3_Ability4", Ability4);
					PlayerPrefs.SetInt ("Save3_Ability5", Ability5);
					PlayerPrefs.SetInt ("Save3_Ability6", Ability6);
					PlayerPrefs.SetInt ("Save3_GetAbility", GetAbility);

					TargetWhitePanel = WhitePanel3;
					iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 2.0f, "time", 1.0f, "onUpdate", "UpdateWhitePanelAlphaUp","onComplete", "CompleteWhitePanelAlphaUp", "oncompletetarget", gameObject));
				}

			}
		} else if (DeleteButton.isDeleteMode) {
			// 画面タイトルのテキスト変更
			TitleText.GetComponent<Text> ().text = "削除するファイルを選んでください";
			
			// 全てのセーブファイルボタンを非活性にする
			SaveDataButton1.GetComponent<Button>().enabled = false;
			SaveDataButton2.GetComponent<Button>().enabled = false;
			SaveDataButton3.GetComponent<Button>().enabled = false;
			DeleteButtonObj.GetComponent<Button>().enabled = false;

			// ファイル削除確認画面の表示
			DeleteCanvas.SetActiveRecursively (true);
			DeleteConfirmationMessage.SetActiveRecursively (true);
			DeleteYesButtonObj.SetActiveRecursively (true);
			DeleteYesButtonObj.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			DeleteYesName.GetComponent<Text> ().color = new Color (0.43137255f, 0.29411765f, 0.0f, 1.0f);
			DeleteNoButtonObj.SetActiveRecursively (true);
			DeleteBackgroundPanel.GetComponent<RectTransform>().localScale = new Vector2(1,1);

			// 削除対象のファイルを格納
			TargetDeleteObj = this.gameObject;
			if (this.gameObject.name == "SaveDataButton1" ){ DeleteConfirmationMessage.GetComponent<Text>().text = "ファイル1を削除しますか？"; }
			if (this.gameObject.name == "SaveDataButton2" ){ DeleteConfirmationMessage.GetComponent<Text>().text = "ファイル2を削除しますか？"; }
			if (this.gameObject.name == "SaveDataButton3" ){ DeleteConfirmationMessage.GetComponent<Text>().text = "ファイル3を削除しますか？"; }

			// デフォルト値を再設定するためにEventSystemを再起動
			EventSystemObj.SetActiveRecursively (false);
			EventSystemObj.SetActiveRecursively (true);
			
			// 一度全てのセーブファイルボタンを活性化する
			SaveDataButton1.GetComponent<Button>().enabled = true;
			SaveDataButton2.GetComponent<Button>().enabled = true;
			SaveDataButton3.GetComponent<Button>().enabled = true;
			WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			
			// ボタンのデフォルト値を変更
			EventSystem.current.firstSelectedGameObject = DeleteNoButtonObj;
			DeleteNoButtonObj.GetComponent<Animator>().SetTrigger (DeleteNoButtonObj.GetComponent<Button>().animationTriggers.highlightedTrigger);

		} else {

			// 選択したセーブファイルの番号を格納し、シーン遷移
			if (this.gameObject.name == "SaveDataButton1" ){ PlaySaveDataNumber.NowPlaySaveDataNumber = 1; }
			if (this.gameObject.name == "SaveDataButton2" ){ PlaySaveDataNumber.NowPlaySaveDataNumber = 2; }
			if (this.gameObject.name == "SaveDataButton3" ){ PlaySaveDataNumber.NowPlaySaveDataNumber = 3; }

			// 画面暗転アニメーション
			iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 1.0f, "time", 0.4f, "delay", 0.05f, "onUpdate", "UpdateBlackOutAnimation", "onComplete", "CompleteBlackOutAnimation"));
		}
	}

	void UpdateBlackOutAnimation(float ImageAlpha){
		// 画面暗転
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}

	void CompleteBlackOutAnimation(){
		// シーン移動
		if (this.gameObject.name == "SaveDataButton1") {
			if(PlayerPrefs.GetInt ("Save1_WorldNum") == 0){
				Application.LoadLevel("Event01");
			} else {
				Application.LoadLevel("GameMap2D");
			}
		}
		if (this.gameObject.name == "SaveDataButton2") {
			if(PlayerPrefs.GetInt ("Save2_WorldNum") == 0){
				Application.LoadLevel("Event01");
			} else {
				Application.LoadLevel("GameMap2D");
			}
		}
		if (this.gameObject.name == "SaveDataButton3") {
			if(PlayerPrefs.GetInt ("Save3_WorldNum") == 0){
				Application.LoadLevel("Event01");
			} else {
				Application.LoadLevel("GameMap2D");
			}
		}
	}

	void UpdateWhitePanelAlphaUp( float WhitePanelAlpha ){
		EventSystemObj.SetActiveRecursively (false);
		TargetWhitePanel.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, WhitePanelAlpha);

		// 画面タイトルのテキスト変更
		TitleText.GetComponent<Text> ().text = "コピー中・・・";
	}

	void CompleteWhitePanelAlphaUp (){
		// セーブデータ情報を画面に反映
		SaveDataStatusControll.SaveDataStatusSetting();
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 2.5f, "onUpdate", "UpdateWhitePanelAlphaDown","onComplete", "CompleteWhitePanelAlphaDown", "oncompletetarget", gameObject));
	}

	void UpdateWhitePanelAlphaDown( float WhitePanelAlpha ){
		TargetWhitePanel.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, WhitePanelAlpha);
		
		// 画面タイトルのテキスト変更
		TitleText.GetComponent<Text> ().text = "コピーが完了しました！";
	}

	void CompleteWhitePanelAlphaDown (){
		// 全てのセーブファイルボタンを活性化する
		SaveDataButton1.GetComponent<Button>().enabled = true;
		SaveDataButton2.GetComponent<Button>().enabled = true;
		SaveDataButton3.GetComponent<Button>().enabled = true;
		WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

		// コピーボタンの上ボタン押下時のボタンを変更
		Navigation navigation = CopyButtonObj.GetComponent<Button>().navigation;
		navigation.selectOnUp = SaveDataButton3.GetComponent<Button>();
		CopyButtonObj.GetComponent<Selectable>().navigation = navigation;

		// 全てのデータが作成されている場合は非表示
		if (PlayerPrefs.GetInt ("Save1_WorldNum") != 0 && PlayerPrefs.GetInt ("Save2_WorldNum") != 0 && PlayerPrefs.GetInt ("Save3_WorldNum") != 0) {
			CopyButtonObj.GetComponent<Button> ().enabled = false;
			WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
		} else {
			CopyButtonObj.GetComponent<Button> ().enabled = true;
			WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}

		// 削除ボタンを活性にする
		DeleteButtonObj.GetComponent<Button>().enabled = true;
		WhitePanelDelete.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

		// 画面タイトルとコピーボタンのテキスト変更
		TitleText.GetComponent<Text> ().text = "遊びたいファイルを選んでください";
		CopyName.GetComponent<Text> ().text  = "コピー";
		CopyButton.isCopyMode = false;
		EventSystemObj.SetActiveRecursively (true);

		// ボタンのデフォルト値を変更
		EventSystem.current.firstSelectedGameObject = SaveDataButton1;
		SaveDataButton1.GetComponent<Animator>().SetTrigger (SaveDataButton1.GetComponent<Button>().animationTriggers.highlightedTrigger);

	}
}