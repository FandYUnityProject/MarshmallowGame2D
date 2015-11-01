using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DeleteNoButton : MonoBehaviour {

	//--- オブジェクト ---//
	private GameObject TitleText;
	private GameObject WhitePanelCopy;
	private GameObject EventSystemObj;
	private GameObject DeleteName;
	private GameObject DeleteCanvas;
	private GameObject DeleteBackgroundPanel;
	private GameObject SaveDataButton1;
	private GameObject SaveDataButton2;
	private GameObject SaveDataButton3;
	private GameObject CopyButtonObj;
	private GameObject DeleteButtonObj;

	void Start () {
		TitleText 				= GameObject.Find ("TitleText");
		WhitePanelCopy			= GameObject.Find ("WhitePanelCopy");
		EventSystemObj 			= GameObject.Find ("EventSystem");
		DeleteName				= GameObject.Find ("DeleteName");
		DeleteCanvas			= GameObject.Find ("DeleteCanvas");
		DeleteBackgroundPanel 	= GameObject.Find ("DeleteBackgroundPanel");
		SaveDataButton1  		= GameObject.Find ("SaveDataButton1");
		SaveDataButton2  		= GameObject.Find ("SaveDataButton2");
		SaveDataButton3  		= GameObject.Find ("SaveDataButton3");
		CopyButtonObj    		= GameObject.Find ("CopyButton");
		DeleteButtonObj  		= GameObject.Find ("DeleteButton");
	}

	public void OnDeleteNoButtonClick(){

		DeleteButton.isDeleteMode = false;

		// 画面タイトルのテキスト変更
		TitleText.GetComponent<Text> ().text = "遊びたいファイルを選んでください";
		DeleteName.GetComponent<Text> ().text = "削除";
		
		// ファイル削除確認画面の非表示
		DeleteBackgroundPanel.GetComponent<RectTransform>().localScale = new Vector2(0,0);
		DeleteCanvas.SetActiveRecursively (false);
			
		// 全てのセーブファイルボタンを活性にする
		SaveDataButton1.GetComponent<Button>().enabled = true;
		SaveDataButton2.GetComponent<Button>().enabled = true;
		SaveDataButton3.GetComponent<Button>().enabled = true;
		DeleteButtonObj.GetComponent<Button>().enabled = true;
		
		// デフォルト値を再設定するためにEventSystemを再起動
		EventSystemObj.SetActiveRecursively (false);
		EventSystemObj.SetActiveRecursively (true);
		
		// ボタンのデフォルト値を変更
		EventSystem.current.firstSelectedGameObject = SaveDataButton1;
		SaveDataButton1.GetComponent<Animator>().SetTrigger (SaveDataButton1.GetComponent<Button>().animationTriggers.highlightedTrigger);

		// セーブデータが全て作成されていない場合は,コピーボタンを活性にする
		if (!(PlayerPrefs.GetInt ("Save1_WorldNum") != 0 && PlayerPrefs.GetInt ("Save2_WorldNum") != 0 && PlayerPrefs.GetInt ("Save3_WorldNum") != 0)) {
			CopyButtonObj.GetComponent<Button>().enabled = true;
			WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}
	}
}
