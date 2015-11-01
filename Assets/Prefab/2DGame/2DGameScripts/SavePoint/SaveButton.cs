using UnityEngine;
using System.Collections;

public class SaveButton : MonoBehaviour {
	
	private GameObject SaveWindow;
	
	void Start(){
		SaveWindow = GameObject.Find ("SaveWindow");
	}

	public void OnClickSaveButton(){

		if (this.gameObject.name == "SaveYes") {
			//--- セーブウィンドウを閉じる前にセーブ ---//

			// ワールド番号とフロア番号を保存
			PlayerPrefs.SetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_WorldNum", SavePoint.SaveWorldNum);
			PlayerPrefs.SetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_FloorNum", SavePoint.SaveFloorNum);

			// アビリティ毎にセーブ
			for( int AbilityNumber=0; AbilityNumber<=6; AbilityNumber++ ){
				if( AbilityChange.isGetAbility[AbilityNumber] ){
					PlayerPrefs.SetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability" + AbilityNumber, 1);
				} else {
					PlayerPrefs.SetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability" + AbilityNumber, 0);
				}
			}

			// 獲得したアビリティ数を保存
			PlayerPrefs.SetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility", AbilityChange.GetAbility);

			// セーブポイントの座標を保存
			PlayerPrefs.SetFloat ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_SavePoint_X", SavePoint.SavePointX);
			PlayerPrefs.SetFloat ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_SavePoint_Y", SavePoint.SavePointY);

			Debug.Log("SaveComplete!! - "
			          + "  WorldNum:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_WorldNum" )
			          + "  FloorNum:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_FloorNum" )
			          + "  Ability0:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability0" )
			          + "  Ability1:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability1" )
			          + "  Ability2:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability2" )
			          + "  Ability3:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability3" )
			          + "  Ability4:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability4" )
			          + "  Ability5:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability5" )
			          + "  Ability6:"    + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability6" )
			          + "  GetAbility:"  + PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility" ));
		}

		// セーブウィンドウを閉じる
		SaveWindow.SetActiveRecursively (false);
		SavePoint.isSaving = false;
	}
}
