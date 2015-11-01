using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DeleteButton : MonoBehaviour {

	//--- オブジェクト ---//
	private GameObject TitleText;
	private GameObject DeleteName;
	private GameObject EventSystemObj;
	private GameObject WhitePanelCopy;
	
	private GameObject SaveDataButton1;
	private GameObject SaveDataButton2;
	private GameObject SaveDataButton3;
	private GameObject WhitePanel1;
	private GameObject WhitePanel2;
	private GameObject WhitePanel3;
	
	//--- デフォルトボタンを格納 ---//
	public Animator DefaltButtonAnime;
	public Button   DefaltSelectButton;

	//--- コピーボタンを格納 ---//
	public Animator CopyButtonAnime;
	public Button   CopySelectButton;

	//--- フラグ管理 ---//
	public static bool isDeleteMode;
	public static bool isDeleteActive;

	void Start () {
		TitleText      = GameObject.Find ("TitleText");
		DeleteName     = GameObject.Find ("DeleteName");
		EventSystemObj = GameObject.Find ("EventSystem");
		WhitePanelCopy = GameObject.Find ("WhitePanelCopy");
		
		SaveDataButton1 = GameObject.Find ("SaveDataButton1");
		SaveDataButton2 = GameObject.Find ("SaveDataButton2");
		SaveDataButton3 = GameObject.Find ("SaveDataButton3");
		WhitePanel1 = GameObject.Find ("WhitePanel1");
		WhitePanel2 = GameObject.Find ("WhitePanel2");
		WhitePanel3 = GameObject.Find ("WhitePanel3");

		isDeleteMode = false;

		// データが全く無い場合は非表示
		if (PlayerPrefs.GetInt ("Save1_WorldNum") == 0 && PlayerPrefs.GetInt ("Save2_WorldNum") == 0 && PlayerPrefs.GetInt ("Save3_WorldNum") == 0) {
			this.gameObject.GetComponent<Button> ().enabled = false;
			isDeleteActive = false;
		} else {
			this.gameObject.GetComponent<Button> ().enabled = true;
			isDeleteActive = true;
		}
	}

	public void OnDeleteButtonClick(){

		if (!isDeleteMode) {
			//--- 通常(ファイル選択)モードなら ---//
			// 削除モードに変更
			isDeleteMode = true;

			// 画面タイトルとコピーボタンのテキスト変更
			TitleText.GetComponent<Text> ().text  = "削除するファイルを選んでください";
			DeleteName.GetComponent<Text> ().text = "キャンセル";

			// EventSystemを一瞬NoActiveにすることでボタンのデフォルト値を再設定できる
			EventSystemObj.SetActiveRecursively (false);
			EventSystemObj.SetActiveRecursively (true);

			// デフォルト(ファイル1)ボタンを選択状態にする
			DefaltButtonAnime.SetTrigger (DefaltSelectButton.animationTriggers.highlightedTrigger);
			// コピーボタンを非活性にする
			CopySelectButton.enabled = false;
			WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);

			// 一度全てのセーブファイルボタンを活性化する
			SaveDataButton1.GetComponent<Button>().enabled = true;
			SaveDataButton2.GetComponent<Button>().enabled = true;
			SaveDataButton3.GetComponent<Button>().enabled = true;
			WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			
			// セーブデータが無いファイルは非活性にする
			if (PlayerPrefs.GetInt ("Save1_WorldNum") == 0) { 
				
				// 対象のボタンを非活性にし、ボタンのデフォルト値を変更
				WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
				SaveDataButton1.GetComponent<Button>().enabled = false;
				EventSystem.current.firstSelectedGameObject = SaveDataButton2;
				SaveDataButton2.GetComponent<Animator>().SetTrigger (SaveDataButton2.GetComponent<Button>().animationTriggers.highlightedTrigger);
				
				if (PlayerPrefs.GetInt ("Save2_WorldNum") == 0) { 
					
					// 対象のボタンを非活性にし、ボタンのデフォルト値を変更
					WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
					SaveDataButton2.GetComponent<Button>().enabled = false;
					EventSystem.current.firstSelectedGameObject = SaveDataButton3;
					SaveDataButton3.GetComponent<Animator>().SetTrigger (SaveDataButton3.GetComponent<Button>().animationTriggers.highlightedTrigger);
					
				} else if(PlayerPrefs.GetInt ("Save3_WorldNum") == 0) { 
					
					// 対象のボタンを非活性にし、ボタンのデフォルト値を変更
					WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
					SaveDataButton3.GetComponent<Button>().enabled = false;
					EventSystem.current.firstSelectedGameObject = SaveDataButton2;
					SaveDataButton2.GetComponent<Animator>().SetTrigger (SaveDataButton2.GetComponent<Button>().animationTriggers.highlightedTrigger);
					
					// コピーボタンの上ボタン押下時のボタンを変更
					Navigation navigation = this.gameObject.GetComponent<Button>().navigation;
					navigation.selectOnUp = SaveDataButton2.GetComponent<Button>();
					this.gameObject.GetComponent<Selectable>().navigation = navigation;
				}
				
				// デフォルト値を再設定するためにEventSystemを再起動
				EventSystemObj.SetActiveRecursively (false);
				EventSystemObj.SetActiveRecursively (true);
			} else {
				
				if (PlayerPrefs.GetInt ("Save3_WorldNum") == 0) { 
					// 対象のボタンを非活性
					WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
					SaveDataButton3.GetComponent<Button>().enabled = false;
					
					// コピーボタンの上ボタン押下時のボタンを変更
					Navigation navigation = this.gameObject.GetComponent<Button>().navigation;
					navigation.selectOnUp = SaveDataButton2.GetComponent<Button>();
					this.gameObject.GetComponent<Selectable>().navigation = navigation;
				}
				if (PlayerPrefs.GetInt ("Save2_WorldNum") == 0) { 
					// 対象のボタンを非活性
					WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
					SaveDataButton2.GetComponent<Button>().enabled = false; 
					
					// コピーボタンの上ボタン押下時のボタンを変更
					Navigation navigation = this.gameObject.GetComponent<Button>().navigation;
					navigation.selectOnUp = SaveDataButton1.GetComponent<Button>();
					this.gameObject.GetComponent<Selectable>().navigation = navigation;
				}
			}
		} else {

			//--- ファイル削除モードなら ---//
			// 通常(ファイル選択)モードに戻す
			isDeleteMode = false;

			// 画面タイトルとコピーボタンのテキスト変更
			TitleText.GetComponent<Text> ().text = "遊びたいファイルを選んでください";
			DeleteName.GetComponent<Text> ().text = "削除";
			
			// EventSystemを一瞬NoActiveにすることでボタンのデフォルト値を再設定できる
			EventSystemObj.SetActiveRecursively (false);
			EventSystemObj.SetActiveRecursively (true);
			
			// ボタンのデフォルト値を変更
			EventSystem.current.firstSelectedGameObject = SaveDataButton1;
			SaveDataButton1.GetComponent<Animator>().SetTrigger (SaveDataButton1.GetComponent<Button>().animationTriggers.highlightedTrigger);
			// セーブデータが全て作成されていない場合は,コピーボタンを活性にする
			if (!(PlayerPrefs.GetInt ("Save1_WorldNum") != 0 && PlayerPrefs.GetInt ("Save2_WorldNum") != 0 && PlayerPrefs.GetInt ("Save3_WorldNum") != 0)) {
				CopySelectButton.enabled = true;
				WhitePanelCopy.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			}

			// 一度全てのセーブファイルボタンを活性化する
			SaveDataButton1.GetComponent<Button>().enabled = true;
			SaveDataButton2.GetComponent<Button>().enabled = true;
			SaveDataButton3.GetComponent<Button>().enabled = true;
			WhitePanel1.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			WhitePanel2.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			WhitePanel3.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

		}
	}
}
