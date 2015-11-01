using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SavePoint : MonoBehaviour {

	//--- ゲームオブジェクト ---//
	private GameObject SaveWindow;
	private GameObject SaveYesButton;
	private GameObject CameraObj;
	private GameObject PlayerObj;

	//--- フラグ管理 ---//
	public static bool isSaving;

	//--- 各セーブポイントのワールド番号、フロア番号格納 ---//
	public int WorldNum;
	public int FloorNum;

	//--- セーブデータに反映するワールド番号、フロア番号格納 ---//
	public static int SaveWorldNum;
	public static int SaveFloorNum;

	//--- セーブポイントの座標格納 ---//
	public static float SavePointX = 0.0f;
	public static float SavePointY = 0.0f;

	void Start () {
		
		SaveWindow    = GameObject.Find ("SaveWindow");
		SaveYesButton = GameObject.Find ("SaveYes");

		CameraObj = GameObject.Find ("Main Camera");
		PlayerObj = GameObject.Find ("Player");

		if (SaveWindow != null) {
			SaveWindow.SetActiveRecursively (false);
		}

		// 初期化
		isSaving = false;
	}

	void OnTriggerEnter2D(Collider2D col){

		// ゲーム再開時でない場合
		if (col.gameObject.name == "Player" && !SavePointPositionControll.isResumption) {

			// セーブウィンドウを表示
			SaveWindow.SetActiveRecursively (true);

			// セーブ中のフラグを立て、セーブ用のワールド番号、フロア番号、セーブポイントの位置格納
			isSaving = true;
			SaveWorldNum = WorldNum;
			SaveFloorNum = FloorNum;
			SavePointX = this.transform.position.x;
			SavePointY = this.transform.position.y;

			// ボタンのデフォルト値を変更
			EventSystem.current.firstSelectedGameObject = SaveYesButton;
			SaveYesButton.GetComponent<Animator> ().SetTrigger (SaveYesButton.GetComponent<Button> ().animationTriggers.highlightedTrigger);
		} else {
			// ゲーム再開時なら、カメラ固定
			CameraObj.transform.position = new Vector3(PlayerObj.transform.position.x + 2.35f, PlayerObj.transform.position.y + 1.8395f, -10);
			PlayerTouchObject.isSaveMap = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		// ゲーム再開フラグを下ろす
		SavePointPositionControll.isResumption = false;
	}
}
