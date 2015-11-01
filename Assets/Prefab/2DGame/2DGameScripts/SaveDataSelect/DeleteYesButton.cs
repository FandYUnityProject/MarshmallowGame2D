using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DeleteYesButton : MonoBehaviour {

	//--- オブジェクト ---//
	private GameObject TitleText;
	private GameObject CopyName;
	private GameObject EventSystemObj;
	
	private GameObject SaveDataButton1;
	private GameObject SaveDataButton2;
	private GameObject SaveDataButton3;
	private GameObject CopyButtonObj;
	private GameObject DeleteCanvas;
	private GameObject DeleteName;
	private GameObject DeleteButtonObj;
	private GameObject DeleteBackgroundPanel;
	private GameObject DeleteConfirmationMessage;
	private GameObject DeleteYesButtonObj;
	private GameObject DeleteYesName;
	private GameObject DeleteNoButtonObj;

	private GameObject WhitePanel1;
	private GameObject WhitePanel2;
	private GameObject WhitePanel3;
	private GameObject WhitePanelCopy;
	private GameObject WhitePanelDelete;

	void Start () {
		TitleText   = GameObject.Find ("TitleText");
		CopyName    = GameObject.Find ("CopyName");
		EventSystemObj = GameObject.Find ("EventSystem");
		
		SaveDataButton1  = GameObject.Find ("SaveDataButton1");
		SaveDataButton2  = GameObject.Find ("SaveDataButton2");
		SaveDataButton3  = GameObject.Find ("SaveDataButton3");
		CopyButtonObj    = GameObject.Find ("CopyButton");
		DeleteCanvas     = GameObject.Find ("DeleteCanvas");
		DeleteName		 = GameObject.Find ("DeleteName");
		DeleteButtonObj  = GameObject.Find ("DeleteButton");
		DeleteBackgroundPanel = GameObject.Find ("DeleteBackgroundPanel");
		DeleteConfirmationMessage = GameObject.Find ("DeleteConfirmationMessage");
		DeleteYesButtonObj = GameObject.Find ("DeleteYesButton");
		DeleteYesName = GameObject.Find ("DeleteYesName");
		DeleteNoButtonObj = GameObject.Find ("DeleteNoButton");
		
		WhitePanel1 = GameObject.Find ("WhitePanel1");
		WhitePanel2 = GameObject.Find ("WhitePanel2");
		WhitePanel3 = GameObject.Find ("WhitePanel3");
		WhitePanelCopy   = GameObject.Find ("WhitePanelCopy");
		WhitePanelDelete = GameObject.Find ("WhitePanelDelete");
	}

	public void OnDeleteYesButtonClick(){

		iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 3.0f, "time", 1.0f, "onUpdate", "UpdateWhitePanelAlphaUp","onComplete", "CompleteWhitePanelAlphaUp", "oncompletetarget", gameObject));
	}

	void UpdateWhitePanelAlphaUp( float WhitePanelAlpha ){
		EventSystemObj.SetActiveRecursively (false);
		
		// ファイル削除確認画面の非表示
		DeleteBackgroundPanel.GetComponent<RectTransform>().localScale = new Vector2(0,0);
		DeleteConfirmationMessage.SetActiveRecursively (false);
		DeleteYesButtonObj.GetComponent<Image> ().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		DeleteYesName.GetComponent<Text> ().color = new Color (0.43137255f, 0.29411765f, 0.0f, 0.0f);
		DeleteNoButtonObj.SetActiveRecursively (false);

		if (SaveDataButton.TargetDeleteObj.name == "SaveDataButton1") { 
			WhitePanel1.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, WhitePanelAlpha); 
			PlayerPrefs.SetInt ("Save1_WorldNum", 0);
			PlayerPrefs.SetInt ("Save1_FloorNum", 0);
			PlayerPrefs.SetInt ("Save1_Ability0", 0);
			PlayerPrefs.SetInt ("Save1_Ability1", 0);
			PlayerPrefs.SetInt ("Save1_Ability2", 0);
			PlayerPrefs.SetInt ("Save1_Ability3", 0);
			PlayerPrefs.SetInt ("Save1_Ability4", 0);
			PlayerPrefs.SetInt ("Save1_Ability5", 0);
			PlayerPrefs.SetInt ("Save1_Ability6", 0);
			PlayerPrefs.SetInt ("Save1_GetAbility", 0);
		}
		if (SaveDataButton.TargetDeleteObj.name == "SaveDataButton2") { 
			WhitePanel2.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, WhitePanelAlpha);
			PlayerPrefs.SetInt ("Save2_WorldNum", 0);
			PlayerPrefs.SetInt ("Save2_FloorNum", 0);
			PlayerPrefs.SetInt ("Save2_Ability0", 0);
			PlayerPrefs.SetInt ("Save2_Ability1", 0);
			PlayerPrefs.SetInt ("Save2_Ability2", 0);
			PlayerPrefs.SetInt ("Save2_Ability3", 0);
			PlayerPrefs.SetInt ("Save2_Ability4", 0);
			PlayerPrefs.SetInt ("Save2_Ability5", 0);
			PlayerPrefs.SetInt ("Save2_Ability6", 0);
			PlayerPrefs.SetInt ("Save2_GetAbility", 0);
		}
		if (SaveDataButton.TargetDeleteObj.name == "SaveDataButton3") { 
			WhitePanel3.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, WhitePanelAlpha);
			PlayerPrefs.SetInt ("Save3_WorldNum", 0);
			PlayerPrefs.SetInt ("Save3_FloorNum", 0);
			PlayerPrefs.SetInt ("Save3_Ability0", 0);
			PlayerPrefs.SetInt ("Save3_Ability1", 0);
			PlayerPrefs.SetInt ("Save3_Ability2", 0);
			PlayerPrefs.SetInt ("Save3_Ability3", 0);
			PlayerPrefs.SetInt ("Save3_Ability4", 0);
			PlayerPrefs.SetInt ("Save3_Ability5", 0);
			PlayerPrefs.SetInt ("Save3_Ability6", 0);
			PlayerPrefs.SetInt ("Save3_GetAbility", 0);
		}
		
		// 画面タイトルのテキスト変更
		TitleText.GetComponent<Text> ().text = "削除中・・・";
	}
	
	void CompleteWhitePanelAlphaUp (){
		// セーブデータ情報を画面に反映
		SaveDataStatusControll.SaveDataStatusSetting();
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 2.5f, "onUpdate", "UpdateWhitePanelAlphaDown","onComplete", "CompleteWhitePanelAlphaDown", "oncompletetarget", gameObject));
	}
	
	void UpdateWhitePanelAlphaDown( float WhitePanelAlpha ){
		if (SaveDataButton.TargetDeleteObj.name == "SaveDataButton1") { WhitePanel1.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, WhitePanelAlpha);  }
		if (SaveDataButton.TargetDeleteObj.name == "SaveDataButton2") { WhitePanel2.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, WhitePanelAlpha);  }
		if (SaveDataButton.TargetDeleteObj.name == "SaveDataButton3") { WhitePanel3.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, WhitePanelAlpha);  }

		// 画面タイトルのテキスト変更
		TitleText.GetComponent<Text> ().text = "削除が完了しました！";
	}
	
	void CompleteWhitePanelAlphaDown (){

		// 全てのセーブファイルボタンを活性化する
		SaveDataButton1.GetComponent<Button>().enabled = true;
		SaveDataButton2.GetComponent<Button>().enabled = true;
		SaveDataButton3.GetComponent<Button>().enabled = true;
		WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		
		// 削除ボタンの上ボタン押下時のボタンを変更
		Navigation navigation = DeleteButtonObj.GetComponent<Button>().navigation;
		navigation.selectOnUp = SaveDataButton3.GetComponent<Button>();
		DeleteButtonObj.GetComponent<Selectable>().navigation = navigation;
		
		// データが１つも無い場合は表示
		if (PlayerPrefs.GetInt ("Save1_WorldNum") == 0 && PlayerPrefs.GetInt ("Save2_WorldNum") == 0 && PlayerPrefs.GetInt ("Save3_WorldNum") == 0) {
			DeleteButtonObj.GetComponent<Button> ().enabled = false;
			WhitePanelDelete.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
			CopyButtonObj.GetComponent<Button> ().enabled = false;
			WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
		} else {
			DeleteButtonObj.GetComponent<Button> ().enabled = true;
			WhitePanelDelete.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			CopyButtonObj.GetComponent<Button> ().enabled = true;
			WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}
		
		// 画面タイトルとコピーボタンのテキスト変更
		TitleText.GetComponent<Text> ().text = "遊びたいファイルを選んでください";
		DeleteName.GetComponent<Text> ().text  = "削除";
		DeleteButton.isDeleteMode = false;
		EventSystemObj.SetActiveRecursively (true);
		
		// ボタンのデフォルト値を変更
		EventSystem.current.firstSelectedGameObject = SaveDataButton1;
		SaveDataButton1.GetComponent<Animator>().SetTrigger (SaveDataButton1.GetComponent<Button>().animationTriggers.highlightedTrigger);
		
	}
}
