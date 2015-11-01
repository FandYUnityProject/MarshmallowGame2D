using UnityEngine;
using System.Collections;

public class SavePointPositionControll : MonoBehaviour {

	private GameObject PlayerObj;

	// ゲーム再開フラグ
	public static bool isResumption = true;

	void Start () {

		PlayerObj = GameObject.Find ("Player");

		if (PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_WorldNum") == 0) {

			// “はじめから”のスタート地点
			PlayerObj.transform.position = new Vector3 (NewGamePositionControll.NewGamePositionX, NewGamePositionControll.NewGamePositionY, 0);

			// "はじめから"のみ、ゲーム再開フラグを下ろす
			isResumption = false;
		} else {
			// “はじめから”以外は保存先からセーブポイントの座標を取得して、再開
			PlayerObj.transform.localPosition = new Vector3 (  PlayerPrefs.GetFloat ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_SavePoint_X")
			                                                 , PlayerPrefs.GetFloat ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_SavePoint_Y")
			                                                 , 0);
		}
	
		/*
		if (PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_WorldNum") == 1) {
			if (PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_FloorNum") == 1) {
				PlayerObj.transform.localPosition = new Vector3 (SavePoint.SavePointX, SavePoint.SavePointY, 0);
			}
		}
		*/
	}
}
