using UnityEngine;
using System.Collections;

public class DebugModeControll : MonoBehaviour {

	public bool  デバッグセーブ0全取得					= false;
	public bool  デバッグセーブ0全取削除				= false;
	public int   デバッグセーブ0現在のワールドマップ番号	= 0;
	public int   デバッグセーブ0現在のセーブフロア番号		= 0;
	public float デバッグセーブ0現在の再開地点のX座標		= 0;
	public float デバッグセーブ0現在の再開地点のY座標		= 0;
	public bool  デバッグセーブ0肉体強化取得				= false;
	public bool  デバッグセーブ0重力操作能力取得			= false;
	public bool  デバッグセーブ0分身能力取得				= false;
	public bool  デバッグセーブ0飛行能力取得				= false;
	public bool  デバッグセーブ0時間操作能力取得			= false;
	public bool  デバッグセーブ0念力能力取得				= false;
	public bool  デバッグセーブ0魔法能力取得				= false;
	
	private int  Save0AbilityCount = 0;

	void Start(){
	
	}

	void Awake () {
		if (デバッグセーブ0全取得) {
			デバッグセーブ0現在のワールドマップ番号  = 8;
			デバッグセーブ0現在のセーブフロア番号	= 64;
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability0", 1);
			PlayerPrefs.SetInt ("Save0_Ability1", 1);
			PlayerPrefs.SetInt ("Save0_Ability2", 1);
			PlayerPrefs.SetInt ("Save0_Ability3", 1);
			PlayerPrefs.SetInt ("Save0_Ability4", 1);
			PlayerPrefs.SetInt ("Save0_Ability5", 1);
			PlayerPrefs.SetInt ("Save0_Ability6", 1);
			
			PlayerPrefs.SetFloat("Save0_SavePoint_X", 2.40f);
			PlayerPrefs.SetFloat("Save0_SavePoint_Y", 9.35f);

			PlayerPrefs.SetInt ("Save0_GetAbility", 7);
			return;
		}
		if (デバッグセーブ0全取削除) {
			デバッグセーブ0現在のワールドマップ番号 	= 0;
			デバッグセーブ0現在のセーブフロア番号	= 0;
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability0", 0);
			PlayerPrefs.SetInt ("Save0_Ability1", 0);
			PlayerPrefs.SetInt ("Save0_Ability2", 0);
			PlayerPrefs.SetInt ("Save0_Ability3", 0);
			PlayerPrefs.SetInt ("Save0_Ability4", 0);
			PlayerPrefs.SetInt ("Save0_Ability5", 0);
			PlayerPrefs.SetInt ("Save0_Ability6", 0);
			
			PlayerPrefs.SetFloat("Save0_SavePoint_X",  1.50f);
			PlayerPrefs.SetFloat("Save0_SavePoint_Y", -5.76f);

			PlayerPrefs.SetInt ("Save0_GetAbility", 0);
			return;
		}

		if(PlayerPrefs.GetInt ("Save0_WorldNum") == 1 && PlayerPrefs.GetInt ("Save0_FloorNum") == 0 ){	
			PlayerPrefs.SetFloat("Save0_SavePoint_X", 2.40f);
			PlayerPrefs.SetFloat("Save0_SavePoint_Y", 9.35f);
		} else {
			PlayerPrefs.SetFloat("Save0_SavePoint_X", デバッグセーブ0現在の再開地点のX座標);
			PlayerPrefs.SetFloat("Save0_SavePoint_Y", デバッグセーブ0現在の再開地点のY座標);
		}
		
		if (デバッグセーブ0肉体強化取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability0", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability0", 0);
		}
		if (デバッグセーブ0重力操作能力取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability1", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability1", 0);
		}
		if (デバッグセーブ0分身能力取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability2", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability2", 0);
		}
		if (デバッグセーブ0飛行能力取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability3", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability3", 0);
		}
		if (デバッグセーブ0時間操作能力取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability4", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability4", 0);
		}
		if (デバッグセーブ0念力能力取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability5", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability5", 0);
		}
		if (デバッグセーブ0魔法能力取得) {
			PlayerPrefs.SetInt ("Save0_WorldNum", デバッグセーブ0現在のワールドマップ番号);
			PlayerPrefs.SetInt ("Save0_FloorNum", デバッグセーブ0現在のセーブフロア番号);
			PlayerPrefs.SetInt ("Save0_Ability6", 1);
			Save0AbilityCount++;
		} else {
			PlayerPrefs.SetInt ("Save0_Ability6", 0);
		}
		
		PlayerPrefs.SetInt ("Save0_GetAbility", Save0AbilityCount);
	}
}
