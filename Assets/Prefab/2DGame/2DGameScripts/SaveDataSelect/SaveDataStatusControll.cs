using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// ワールドマップイメージは”Resource”フォルダ内に【WorldImageX】の名前で入れる
// ex.) WorldImage1

public class SaveDataStatusControll : MonoBehaviour {

	//--- SaveDataCanvasオブジェクト ---//
	// ワールドイメージ格納
	public static Sprite[] WorldImageSprites;

	// SaveData1
	public static GameObject SaveDataButton1Obj;
	public static GameObject WorldImage1;

	public static GameObject AbilityArea1;
	public static GameObject AbilitiesArea1;

	public static GameObject StrengthImage1;
	public static GameObject GravityImage1;
	public static GameObject AvatarImage1;
	public static GameObject FlyImage1;
	public static GameObject ChronoImage1;
	public static GameObject PsychoImage1;
	public static GameObject MagicImage1;

	public static GameObject StrengthImageS1;
	public static GameObject GravityImageS1;
	public static GameObject AvatarImageS1;
	public static GameObject FlyImageS1;
	public static GameObject ChronoImageS1;
	public static GameObject PsychoImageS1;
	public static GameObject MagicImageS1;

	// SaveData2
	public static GameObject SaveDataButton2Obj;
	public static GameObject WorldImage2;

	public static GameObject AbilityArea2;
	public static GameObject AbilitiesArea2;
	
	public static GameObject StrengthImage2;
	public static GameObject GravityImage2;
	public static GameObject AvatarImage2;
	public static GameObject FlyImage2;
	public static GameObject ChronoImage2;
	public static GameObject PsychoImage2;
	public static GameObject MagicImage2;
	
	public static GameObject StrengthImageS2;
	public static GameObject GravityImageS2;
	public static GameObject AvatarImageS2;
	public static GameObject FlyImageS2;
	public static GameObject ChronoImageS2;
	public static GameObject PsychoImageS2;
	public static GameObject MagicImageS2;

	// SaveData3
	public static GameObject SaveDataButton3Obj;
	public static GameObject WorldImage3;

	public static GameObject AbilityArea3;
	public static GameObject AbilitiesArea3;
	
	public static GameObject StrengthImage3;
	public static GameObject GravityImage3;
	public static GameObject AvatarImage3;
	public static GameObject FlyImage3;
	public static GameObject ChronoImage3;
	public static GameObject PsychoImage3;
	public static GameObject MagicImage3;
	
	public static GameObject StrengthImageS3;
	public static GameObject GravityImageS3;
	public static GameObject AvatarImageS3;
	public static GameObject FlyImageS3;
	public static GameObject ChronoImageS3;
	public static GameObject PsychoImageS3;
	public static GameObject MagicImageS3;

	// Use this for initialization
	void Start () {
		// SaveData1
		SaveDataButton1Obj 	= GameObject.Find("SaveDataButton1");
		WorldImage1		 	= GameObject.Find("WorldImage1");
		AbilityArea1 		= GameObject.Find("AbilityArea1");
		AbilitiesArea1		= GameObject.Find("AbilitiesArea1");
		StrengthImage1		= GameObject.Find("StrengthImage1");
		GravityImage1		= GameObject.Find("GravityImage1");
		AvatarImage1		= GameObject.Find("AvatarImage1");
		FlyImage1			= GameObject.Find("FlyImage1");
		ChronoImage1		= GameObject.Find("ChronoImage1");
		PsychoImage1		= GameObject.Find("PsychoImage1");
		MagicImage1			= GameObject.Find("MagicImage1");
		StrengthImageS1		= GameObject.Find("StrengthImageS1");
		GravityImageS1		= GameObject.Find("GravityImageS1");
		AvatarImageS1		= GameObject.Find("AvatarImageS1");
		FlyImageS1			= GameObject.Find("FlyImageS1");
		ChronoImageS1		= GameObject.Find("ChronoImageS1");
		PsychoImageS1		= GameObject.Find("PsychoImageS1");
		MagicImageS1		= GameObject.Find("MagicImageS1");
		
		// SaveData2
		SaveDataButton2Obj 	= GameObject.Find("SaveDataButton2");
		WorldImage2		 	= GameObject.Find("WorldImage2");
		AbilityArea2 		= GameObject.Find("AbilityArea2");
		AbilitiesArea2		= GameObject.Find("AbilitiesArea2");
		StrengthImage2		= GameObject.Find("StrengthImage2");
		GravityImage2		= GameObject.Find("GravityImage2");
		AvatarImage2		= GameObject.Find("AvatarImage2");
		FlyImage2			= GameObject.Find("FlyImage2");
		ChronoImage2		= GameObject.Find("ChronoImage2");
		PsychoImage2		= GameObject.Find("PsychoImage2");
		MagicImage2			= GameObject.Find("MagicImage2");
		StrengthImageS2		= GameObject.Find("StrengthImageS2");
		GravityImageS2		= GameObject.Find("GravityImageS2");
		AvatarImageS2		= GameObject.Find("AvatarImageS2");
		FlyImageS2			= GameObject.Find("FlyImageS2");
		ChronoImageS2		= GameObject.Find("ChronoImageS2");
		PsychoImageS2		= GameObject.Find("PsychoImageS2");
		MagicImageS2		= GameObject.Find("MagicImageS2");
		
		// SaveData3
		SaveDataButton3Obj 	= GameObject.Find("SaveDataButton3");
		WorldImage3		 	= GameObject.Find("WorldImage3");
		AbilityArea3		= GameObject.Find("AbilityArea3");
		AbilitiesArea3		= GameObject.Find("AbilitiesArea3");
		StrengthImage3		= GameObject.Find("StrengthImage3");
		GravityImage3		= GameObject.Find("GravityImage3");
		AvatarImage3		= GameObject.Find("AvatarImage3");
		FlyImage3			= GameObject.Find("FlyImage3");
		ChronoImage3		= GameObject.Find("ChronoImage3");
		PsychoImage3		= GameObject.Find("PsychoImage3");
		MagicImage3			= GameObject.Find("MagicImage3");
		StrengthImageS3		= GameObject.Find("StrengthImageS3");
		GravityImageS3		= GameObject.Find("GravityImageS3");
		AvatarImageS3		= GameObject.Find("AvatarImageS3");
		FlyImageS3			= GameObject.Find("FlyImageS3");
		ChronoImageS3		= GameObject.Find("ChronoImageS3");
		PsychoImageS3		= GameObject.Find("PsychoImageS3");
		MagicImageS3		= GameObject.Find("MagicImageS3");
	}

	// セーブデータ情報を画面に反映
	public static void SaveDataStatusSetting () {

		//--- セーブフロア情報(セーブ選択画面には反映しない) ---//
		PlayerPrefs.GetInt ("Save1_FloorNum");
		PlayerPrefs.GetInt ("Save2_FloorNum");
		PlayerPrefs.GetInt ("Save3_FloorNum");

		//--- ワールドイメージを取得、反映 ---//
		// SaveData1 - Map
		WorldImage1.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Sprite/WorldImage" + PlayerPrefs.GetInt ("Save1_WorldNum"));
		// SaveData2 - Map
		WorldImage2.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Sprite/WorldImage" + PlayerPrefs.GetInt ("Save2_WorldNum"));
		// SaveData3 - Map
		WorldImage3.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>("Sprite/WorldImage" + PlayerPrefs.GetInt ("Save3_WorldNum"));

		//*** 各能力取得毎に反映 ***//
		// SaveData1 - Ability
		if (PlayerPrefs.GetInt ("Save1_GetAbility") == 0) {
			//--- アビリティを1つも獲得していない場合 ---//
			AbilityArea1.SetActiveRecursively (false);
			AbilitiesArea1.SetActiveRecursively (false);
		} else if (PlayerPrefs.GetInt ("Save1_GetAbility") == 1) {
			//--- アビリティを1つ獲得している場合 ---//

			// 複数アビリティウィンドウを非表示にする
			AbilityArea1.SetActiveRecursively (true);
			AbilitiesArea1.SetActiveRecursively (false);
			
			// 各アビリティ毎に処理
			if (PlayerPrefs.GetInt ("Save1_Ability0") == 1) {
				SaveData1AbilityNotActive ();
				StrengthImage1.SetActiveRecursively (true);
			}
			if (PlayerPrefs.GetInt ("Save1_Ability1") == 1) {
				SaveData1AbilityNotActive ();
				GravityImage1.SetActiveRecursively (true);
			}
			if (PlayerPrefs.GetInt ("Save1_Ability2") == 1) {
				SaveData1AbilityNotActive ();
				AvatarImage1.SetActiveRecursively (true);
			}
			if (PlayerPrefs.GetInt ("Save1_Ability3") == 1) {
				SaveData1AbilityNotActive ();
				FlyImage1.SetActiveRecursively (true);
			}
			if (PlayerPrefs.GetInt ("Save1_Ability4") == 1) {
				SaveData1AbilityNotActive ();
				ChronoImage1.SetActiveRecursively (true);
			}
			if (PlayerPrefs.GetInt ("Save1_Ability5") == 1) {
				SaveData1AbilityNotActive ();
				PsychoImage1.SetActiveRecursively (true);
			}
			if (PlayerPrefs.GetInt ("Save1_Ability6") == 1) {
				SaveData1AbilityNotActive ();
				MagicImage1.SetActiveRecursively (true);
			}
		} else {
			//--- アビリティを2つ以上獲得している場合 ---//

			// 単数アビリティウィンドウを非表示にする
			AbilityArea1.SetActiveRecursively (false);
			AbilitiesArea1.SetActiveRecursively (true);

			// 各アビリティ毎に処理
			if (PlayerPrefs.GetInt ("Save1_Ability0") == 0) { StrengthImageS1.transform.GetComponent<Image> ().color = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save1_Ability1") == 0) { GravityImageS1.transform.GetComponent<Image> ().color  = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save1_Ability2") == 0) { AvatarImageS1.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save1_Ability3") == 0) { FlyImageS1.transform.GetComponent<Image> ().color      = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save1_Ability4") == 0) { ChronoImageS1.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save1_Ability5") == 0) { PsychoImageS1.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save1_Ability6") == 0) { MagicImageS1.transform.GetComponent<Image> ().color    = new Color  (0, 0, 0, 1.0f); }
		}

		// SaveData2 - Ability
		if (PlayerPrefs.GetInt ("Save2_GetAbility") == 0) {
			AbilityArea2.SetActiveRecursively (false);
			AbilitiesArea2.SetActiveRecursively (false);
		} else if (PlayerPrefs.GetInt ("Save2_GetAbility") == 1) {
			
			// 複数アビリティウィンドウを非表示にする
			AbilityArea2.SetActiveRecursively (true);
			AbilitiesArea2.SetActiveRecursively (false);
			
			// 各アビリティ毎に処理
			if( PlayerPrefs.GetInt ("Save2_Ability0") == 1 ){
				SaveData2AbilityNotActive();
				StrengthImage2.SetActiveRecursively (true);	
			}
			if( PlayerPrefs.GetInt ("Save2_Ability1") == 1 ){
				SaveData2AbilityNotActive();
				GravityImage2.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save2_Ability2") == 1 ){
				SaveData2AbilityNotActive();
				AvatarImage2.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save2_Ability3") == 1 ){
				SaveData2AbilityNotActive();
				FlyImage2.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save2_Ability4") == 1 ){
				SaveData2AbilityNotActive();
				ChronoImage2.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save2_Ability5") == 1 ){
				SaveData2AbilityNotActive();
				PsychoImage2.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save2_Ability6") == 1 ){
				SaveData2AbilityNotActive();
				MagicImage2.SetActiveRecursively (true);
			}
		} else {
			//--- アビリティを2つ以上獲得している場合 ---//

			// 単数アビリティウィンドウを非表示にする
			AbilityArea2.SetActiveRecursively (false);
			AbilitiesArea2.SetActiveRecursively (true);

			// 各アビリティ毎に処理
			if (PlayerPrefs.GetInt ("Save2_Ability0") == 0) { StrengthImageS2.transform.GetComponent<Image> ().color = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save2_Ability1") == 0) { GravityImageS2.transform.GetComponent<Image> ().color  = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save2_Ability2") == 0) { AvatarImageS2.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save2_Ability3") == 0) { FlyImageS2.transform.GetComponent<Image> ().color      = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save2_Ability4") == 0) { ChronoImageS2.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save2_Ability5") == 0) { PsychoImageS2.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save2_Ability6") == 0) { MagicImageS2.transform.GetComponent<Image> ().color    = new Color  (0, 0, 0, 1.0f); }
		}

		// SaveData3 - Ability
		if (PlayerPrefs.GetInt ("Save3_GetAbility") == 0) {
			AbilityArea3.SetActiveRecursively (false);
			AbilitiesArea3.SetActiveRecursively (false);
		} else if (PlayerPrefs.GetInt ("Save3_GetAbility") == 1) {
			
			// 複数アビリティウィンドウを非表示にする
			AbilityArea3.SetActiveRecursively (true);
			AbilitiesArea3.SetActiveRecursively (false);
			
			// 各アビリティ毎に処理
			if( PlayerPrefs.GetInt ("Save3_Ability0") == 1 ){
				SaveData3AbilityNotActive();
				StrengthImage3.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save3_Ability1") == 1 ){
				SaveData3AbilityNotActive();
				GravityImage3.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save3_Ability2") == 1 ){
				SaveData3AbilityNotActive();
				AvatarImage3.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save3_Ability3") == 1 ){
				SaveData3AbilityNotActive();
				FlyImage3.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save3_Ability4") == 1 ){
				SaveData3AbilityNotActive();
				ChronoImage3.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save3_Ability5") == 1 ){
				SaveData3AbilityNotActive();
				PsychoImage3.SetActiveRecursively (true);
			}
			if( PlayerPrefs.GetInt ("Save3_Ability6") == 1 ){
				SaveData3AbilityNotActive();
				MagicImage3.SetActiveRecursively (true);
			}
		} else {
			//--- アビリティを2つ以上獲得している場合 ---//
			
			// 単数アビリティウィンドウを非表示にする
			AbilityArea3.SetActiveRecursively (false);
			AbilitiesArea3.SetActiveRecursively (true);

			// 各アビリティ毎に処理
			if (PlayerPrefs.GetInt ("Save3_Ability0") == 0) { StrengthImageS3.transform.GetComponent<Image> ().color = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save3_Ability1") == 0) { GravityImageS3.transform.GetComponent<Image> ().color  = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save3_Ability2") == 0) { AvatarImageS3.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save3_Ability3") == 0) { FlyImageS3.transform.GetComponent<Image> ().color      = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save3_Ability4") == 0) { ChronoImageS3.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save3_Ability5") == 0) { PsychoImageS3.transform.GetComponent<Image> ().color   = new Color  (0, 0, 0, 1.0f); }
			if (PlayerPrefs.GetInt ("Save3_Ability6") == 0) { MagicImageS3.transform.GetComponent<Image> ().color    = new Color  (0, 0, 0, 1.0f); }
		}

	}

	//--- アビリティを一時全て非表示にする ---//
	// SaveData1 - Ability
	public static void SaveData1AbilityNotActive(){
		StrengthImage1.SetActiveRecursively (false);
		GravityImage1.SetActiveRecursively (false);
		AvatarImage1.SetActiveRecursively (false);
		FlyImage1.SetActiveRecursively (false);
		ChronoImage1.SetActiveRecursively (false);
		PsychoImage1.SetActiveRecursively (false);
		MagicImage1.SetActiveRecursively (false);
	}
	// SaveData2 - Ability
	public static void SaveData2AbilityNotActive(){
		StrengthImage2.SetActiveRecursively (false);
		GravityImage2.SetActiveRecursively (false);
		AvatarImage2.SetActiveRecursively (false);
		FlyImage2.SetActiveRecursively (false);
		ChronoImage2.SetActiveRecursively (false);
		PsychoImage2.SetActiveRecursively (false);
		MagicImage2.SetActiveRecursively (false);
	}
	// SaveData3 - Ability
	public static void SaveData3AbilityNotActive(){
		StrengthImage3.SetActiveRecursively (false);
		GravityImage3.SetActiveRecursively (false);
		AvatarImage3.SetActiveRecursively (false);
		FlyImage3.SetActiveRecursively (false);
		ChronoImage3.SetActiveRecursively (false);
		PsychoImage3.SetActiveRecursively (false);
		MagicImage3.SetActiveRecursively (false);
	}
}
