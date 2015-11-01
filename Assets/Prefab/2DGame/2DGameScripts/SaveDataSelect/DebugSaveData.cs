using UnityEngine;
using System.Collections;

/* --- 能力番号:オブジェクト名 ---
 * 0: StrengthImage -> 肉体強化
 * 1: GravityImage  -> 重力操作
 * 2: AvatarImage   -> 分身
 * 3: FlyImage      -> 飛行
 * 4: ChronoImage   -> 時間操作
 * 5: PsychoImage   -> 念力
 * 6: MagicImage    -> 魔法
 */

public class DebugSaveData : MonoBehaviour {
	
	public bool デバッグモード;

	public SaveData1 saveData1;
	public SaveData2 saveData2;
	public SaveData3 saveData3;
	
	void Awake(){
		if (デバッグモード) {
			saveData1.Awake ();
			saveData2.Awake ();
			saveData3.Awake ();
			Debug.Log("Debug Start!");
		}
	}


	[System.Serializable]
	public class SaveData1{
		public bool  セーブ1全取得					= false;
		public bool  セーブ1全取削除				= false;
		public int   セーブ1現在のワールドマップ番号	= 0;
		public int   セーブ1現在のセーブフロア番号	= 0;
		public float セーブ1現在の再開地点のX座標	= 0;
		public float セーブ1現在の再開地点のY座標	= 0;
		public bool  セーブ1肉体強化取得			= false;
		public bool  セーブ1重力操作能力取得		= false;
		public bool  セーブ1分身能力取得			= false;
		public bool  セーブ1飛行能力取得			= false;
		public bool  セーブ1時間操作能力取得		= false;
		public bool  セーブ1念力能力取得			= false;
		public bool  セーブ1魔法能力取得			= false;

		private int Save1AbilityCount = 0;

		public void Awake(){
			//--- ワールドマップ番号が0(新しく物語を始める)のデータは初期化扱い ---//
			// セーブデータ1
			if (PlayerPrefs.GetInt ("Save1_WorldNum") == 0) {
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
			// セーブデータ2
			if (PlayerPrefs.GetInt ("Save2_WorldNum") == 0) {
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
			// セーブデータ3
			if (PlayerPrefs.GetInt ("Save3_WorldNum") == 0) {
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
			
			//--- セーブデータ1 ---//
			if (セーブ1全取得) {
				セーブ1現在のワールドマップ番号  = 8;
				セーブ1現在のセーブフロア番号	= 64;
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability0", 1);
				PlayerPrefs.SetInt ("Save1_Ability1", 1);
				PlayerPrefs.SetInt ("Save1_Ability2", 1);
				PlayerPrefs.SetInt ("Save1_Ability3", 1);
				PlayerPrefs.SetInt ("Save1_Ability4", 1);
				PlayerPrefs.SetInt ("Save1_Ability5", 1);
				PlayerPrefs.SetInt ("Save1_Ability6", 1);
				
				PlayerPrefs.SetFloat("Save1_SavePoint_X", 2.40f);
				PlayerPrefs.SetFloat("Save1_SavePoint_Y", 9.35f);

				Save1AbilityCount = 7;
				PlayerPrefs.SetInt ("Save1_GetAbility", Save1AbilityCount);
				return;
			}
			if (セーブ1全取削除) {
				セーブ1現在のワールドマップ番号 	= 0;
				セーブ1現在のセーブフロア番号	= 0;
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability0", 0);
				PlayerPrefs.SetInt ("Save1_Ability1", 0);
				PlayerPrefs.SetInt ("Save1_Ability2", 0);
				PlayerPrefs.SetInt ("Save1_Ability3", 0);
				PlayerPrefs.SetInt ("Save1_Ability4", 0);
				PlayerPrefs.SetInt ("Save1_Ability5", 0);
				PlayerPrefs.SetInt ("Save1_Ability6", 0);
				
				PlayerPrefs.SetFloat("Save1_SavePoint_X",  1.50f);
				PlayerPrefs.SetFloat("Save1_SavePoint_Y", -5.76f);
				
				Save1AbilityCount = 0;
				PlayerPrefs.SetInt ("Save1_GetAbility", Save1AbilityCount);
				return;
			}

			if(PlayerPrefs.GetInt ("Save1_WorldNum") == 1 && PlayerPrefs.GetInt ("Save1_FloorNum") == 0 ){	
				PlayerPrefs.SetFloat("Save1_SavePoint_X", 2.40f);
				PlayerPrefs.SetFloat("Save1_SavePoint_Y", 9.35f);
			} else {
				PlayerPrefs.SetFloat("Save1_SavePoint_X", セーブ1現在の再開地点のX座標);
				PlayerPrefs.SetFloat("Save1_SavePoint_Y", セーブ1現在の再開地点のY座標);
			}

			if (セーブ1肉体強化取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetFloat("Save1_SavePoint_X", 2.40f);
				PlayerPrefs.SetInt ("Save1_Ability0", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability0", 0);
			}
			if (セーブ1重力操作能力取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability1", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability1", 0);
			}
			if (セーブ1分身能力取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability2", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability2", 0);
			}
			if (セーブ1飛行能力取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability3", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability3", 0);
			}
			if (セーブ1時間操作能力取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability4", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability4", 0);
			}
			if (セーブ1念力能力取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability5", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability5", 0);
			}
			if (セーブ1魔法能力取得) {
				PlayerPrefs.SetInt ("Save1_WorldNum", セーブ1現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save1_FloorNum", セーブ1現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save1_Ability6", 1);
				Save1AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save1_Ability6", 0);
			}

			PlayerPrefs.SetInt ("Save1_GetAbility", Save1AbilityCount);
		}
	}

	[System.Serializable]
	public class SaveData2{
		public bool  セーブ2全取得					= false;
		public bool  セーブ2全取削除				= false;
		public int   セーブ2現在のワールドマップ番号	= 0;
		public int   セーブ2現在のセーブフロア番号	= 0;
		public float セーブ2現在の再開地点のX座標	= 0;
		public float セーブ2現在の再開地点のY座標	= 0;
		public bool  セーブ2肉体強化取得			= false;
		public bool  セーブ2重力操作能力取得		= false;
		public bool  セーブ2分身能力取得			= false;
		public bool  セーブ2飛行能力取得			= false;
		public bool  セーブ2時間操作能力取得		= false;
		public bool  セーブ2念力能力取得			= false;
		public bool  セーブ2魔法能力取得			= false;
		
		private int Save2AbilityCount = 0;
		
		public void Awake(){
			
			//--- セーブデータ2 ---//
			if (セーブ2全取得) {
				セーブ2現在のワールドマップ番号 	= 8;
				セーブ2現在のセーブフロア番号	= 64;
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability0", 1);
				PlayerPrefs.SetInt ("Save2_Ability1", 1);
				PlayerPrefs.SetInt ("Save2_Ability2", 1);
				PlayerPrefs.SetInt ("Save2_Ability3", 1);
				PlayerPrefs.SetInt ("Save2_Ability4", 1);
				PlayerPrefs.SetInt ("Save2_Ability5", 1);
				PlayerPrefs.SetInt ("Save2_Ability6", 1);
				
				PlayerPrefs.SetFloat("Save2_SavePoint_X", 2.40f);
				PlayerPrefs.SetFloat("Save2_SavePoint_Y", 9.35f);
				
				Save2AbilityCount = 7;
				PlayerPrefs.SetInt ("Save2_GetAbility", Save2AbilityCount);
				return;
			}
			if (セーブ2全取削除) {
				セーブ2現在のワールドマップ番号 	= 0;
				セーブ2現在のセーブフロア番号	= 0;
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability0", 0);
				PlayerPrefs.SetInt ("Save2_Ability1", 0);
				PlayerPrefs.SetInt ("Save2_Ability2", 0);
				PlayerPrefs.SetInt ("Save2_Ability3", 0);
				PlayerPrefs.SetInt ("Save2_Ability4", 0);
				PlayerPrefs.SetInt ("Save2_Ability5", 0);
				PlayerPrefs.SetInt ("Save2_Ability6", 0);
				
				PlayerPrefs.SetFloat("Save2_SavePoint_X",  1.50f);
				PlayerPrefs.SetFloat("Save2_SavePoint_Y", -5.76f);
				
				Save2AbilityCount = 0;
				PlayerPrefs.SetInt ("Save2_GetAbility", Save2AbilityCount);
				return;
			}

			if(PlayerPrefs.GetInt ("Save2_WorldNum") == 1 && PlayerPrefs.GetInt ("Save2_FloorNum") == 0 ){	
				PlayerPrefs.SetFloat("Save2_SavePoint_X", 2.40f);
				PlayerPrefs.SetFloat("Save2_SavePoint_Y", 9.35f);
			} else {
				PlayerPrefs.SetFloat("Save2_SavePoint_X", セーブ2現在の再開地点のX座標);
				PlayerPrefs.SetFloat("Save2_SavePoint_Y", セーブ2現在の再開地点のY座標);
			}
			
			if (セーブ2肉体強化取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability0", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability0", 0);
			}
			if (セーブ2重力操作能力取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability1", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability1", 0);
			}
			if (セーブ2分身能力取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability2", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability2", 0);
			}
			if (セーブ2飛行能力取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability3", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability3", 0);
			}
			if (セーブ2時間操作能力取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability4", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability4", 0);
			}
			if (セーブ2念力能力取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability5", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability5", 0);
			}
			if (セーブ2魔法能力取得) {
				PlayerPrefs.SetInt ("Save2_WorldNum", セーブ2現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save2_FloorNum", セーブ2現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save2_Ability6", 1);
				Save2AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save2_Ability6", 0);
			}
			
			PlayerPrefs.SetInt ("Save2_GetAbility", Save2AbilityCount);
		}
	}

	[System.Serializable]
	public class SaveData3{
		public bool  セーブ3全取得					= false;
		public bool  セーブ3全取削除				= false;
		public int   セーブ3現在のワールドマップ番号	= 0;
		public int   セーブ3現在のセーブフロア番号	= 0;
		public float セーブ3現在の再開地点のX座標	= 0;
		public float セーブ3現在の再開地点のY座標	= 0;
		public bool  セーブ3肉体強化取得			= false;
		public bool  セーブ3重力操作能力取得		= false;
		public bool  セーブ3分身能力取得			= false;
		public bool  セーブ3飛行能力取得			= false;
		public bool  セーブ3時間操作能力取得		= false;
		public bool  セーブ3念力能力取得			= false;
		public bool  セーブ3魔法能力取得			= false;
		
		private int Save3AbilityCount = 0;
		
		public void Awake(){
			
			//--- セーブデータ3 ---//
			if (セーブ3全取得) {
				セーブ3現在のワールドマップ番号 		= 8;
				セーブ3現在のセーブフロア番号	= 64;
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability0", 1);
				PlayerPrefs.SetInt ("Save3_Ability1", 1);
				PlayerPrefs.SetInt ("Save3_Ability2", 1);
				PlayerPrefs.SetInt ("Save3_Ability3", 1);
				PlayerPrefs.SetInt ("Save3_Ability4", 1);
				PlayerPrefs.SetInt ("Save3_Ability5", 1);
				PlayerPrefs.SetInt ("Save3_Ability6", 1);

				PlayerPrefs.SetFloat("Save3_SavePoint_X", 2.40f);
				PlayerPrefs.SetFloat("Save3_SavePoint_Y", 9.35f);
				
				Save3AbilityCount = 7;
				PlayerPrefs.SetInt ("Save3_GetAbility", Save3AbilityCount);
				return;
			}
			if (セーブ3全取削除) {
				セーブ3現在のワールドマップ番号 		= 0;
				セーブ3現在のセーブフロア番号	= 0;
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability0", 0);
				PlayerPrefs.SetInt ("Save3_Ability1", 0);
				PlayerPrefs.SetInt ("Save3_Ability2", 0);
				PlayerPrefs.SetInt ("Save3_Ability3", 0);
				PlayerPrefs.SetInt ("Save3_Ability4", 0);
				PlayerPrefs.SetInt ("Save3_Ability5", 0);
				PlayerPrefs.SetInt ("Save3_Ability6", 0);
				
				PlayerPrefs.SetFloat("Save3_SavePoint_X",  1.50f);
				PlayerPrefs.SetFloat("Save3_SavePoint_Y", -5.76f);
				
				Save3AbilityCount = 0;
				PlayerPrefs.SetInt ("Save3_GetAbility", Save3AbilityCount);
				return;
			}

			if(PlayerPrefs.GetInt ("Save3_WorldNum") == 1 && PlayerPrefs.GetInt ("Save3_FloorNum") == 0 ){	
				PlayerPrefs.SetFloat("Save3_SavePoint_X", 2.40f);
				PlayerPrefs.SetFloat("Save3_SavePoint_Y", 9.35f);
			} else {
				PlayerPrefs.SetFloat("Save3_SavePoint_X", セーブ3現在の再開地点のX座標);
				PlayerPrefs.SetFloat("Save3_SavePoint_Y", セーブ3現在の再開地点のY座標);
			}
			
			if (セーブ3肉体強化取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability0", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability0", 0);
			}
			if (セーブ3重力操作能力取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability1", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability1", 0);
			}
			if (セーブ3分身能力取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability2", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability2", 0);
			}
			if (セーブ3飛行能力取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability3", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability3", 0);
			}
			if (セーブ3時間操作能力取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability4", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability4", 0);
			}
			if (セーブ3念力能力取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability5", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability5", 0);
			}
			if (セーブ3魔法能力取得) {
				PlayerPrefs.SetInt ("Save3_WorldNum", セーブ3現在のワールドマップ番号);
				PlayerPrefs.SetInt ("Save3_FloorNum", セーブ3現在のセーブフロア番号);
				PlayerPrefs.SetInt ("Save3_Ability6", 1);
				Save3AbilityCount++;
			} else {
				PlayerPrefs.SetInt ("Save3_Ability6", 0);
			}
			
			PlayerPrefs.SetInt ("Save3_GetAbility", Save3AbilityCount);
		}
	}
}
