using UnityEngine;
using System.Collections;

public class GetAbility : MonoBehaviour {

	void Start () {

		// 既に獲得している能力は削除する
		if( this.gameObject.name == "StrengthIcon" && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability0") == 1){ Destroy( this.gameObject ); }
		if( this.gameObject.name == "GravityIcon"  && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability1") == 1){ Destroy( this.gameObject ); }
		if( this.gameObject.name == "AvatarIcon"   && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability2") == 1){ Destroy( this.gameObject ); }
		if( this.gameObject.name == "FlyIcon"      && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability3") == 1){ Destroy( this.gameObject ); }
		if( this.gameObject.name == "ChronoIcon"   && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability4") == 1){ Destroy( this.gameObject ); }
		if( this.gameObject.name == "PsychoIcon"   && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability5") == 1){ Destroy( this.gameObject ); }
		if( this.gameObject.name == "MagicIcon"    && PlayerPrefs.GetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_Ability6") == 1){ Destroy( this.gameObject ); }

	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.name == "Player") {
			//--- 獲得能力に応じてアビリティスロット画面に反映、ソート ---//
			if( this.gameObject.name == "StrengthIcon" ){ SortSettingAbility(0, AbilityChange.StrengthImage); Debug.Log("Get StrengthIcon"); }
			if( this.gameObject.name == "GravityIcon" ) { SortSettingAbility(1, AbilityChange.GravityImage);  Debug.Log("Get GravityIcon");  }
			if( this.gameObject.name == "AvatarIcon" )  { SortSettingAbility(2, AbilityChange.AvatarImage);   Debug.Log("Get AvatarIcon");   }
			if( this.gameObject.name == "FlyIcon" )     { SortSettingAbility(3, AbilityChange.FlyImage);      Debug.Log("Get FlyIcon");      }
			if( this.gameObject.name == "ChronoIcon" )  { SortSettingAbility(4, AbilityChange.ChronoImage);   Debug.Log("Get ChronoIcon");   }
			if( this.gameObject.name == "PsychoIcon" )  { SortSettingAbility(5, AbilityChange.PsychoImage);   Debug.Log("Get PsychoIcon");   }
			if( this.gameObject.name == "MagicIcon" )   { SortSettingAbility(6, AbilityChange.MagicImage);    Debug.Log("Get MagicIcon");    }
		}
	}

	void SortSettingAbility(int AbilityNumber, GameObject AbilityImage){

		// TODO 能力獲得時にアニメーションが入ると思うので、コルーチンか何かで処理追加

		// アビリティスロット画面に反映
		AbilityChange.isGetAbility[AbilityNumber] = true;
		AbilityImage.SetActiveRecursively (true);
		AbilityChange.SetSlotCount++;
		AbilityChange.GetAbility++;
		AbilityChange.NowSlotAbilities.Add(AbilityImage);
		AbilityChange.NowSlotAbilitiesNumList.Add(AbilityNumber);
		
		if( AbilityChange.GetAbility == 1 ){
			AbilityChange.NormalImage.SetActiveRecursively (false);
			AbilityChange.MagicImage.transform.localPosition = new Vector2(0, 0);
		} else {
			
			// 獲得したアビリティのソート
			int x=0;	// アイテムイメージの位置
			
			// 現在取得しているアビリティを画面に反映
			for (int i=0; i<AbilityChange.isGetAbility.Length; i++){
				if( AbilityChange.isGetAbility[i] ){
					AbilityChange.NowSlotAbilities[x] = AbilityChange.GetAbilities[i];
					AbilityChange.NowSlotAbilitiesNumList[x] = i;
					AbilityChange.GetAbilities[i].transform.localPosition = new Vector2(x * AbilityChange.SlotImageWidth, 0);
					x++;
				}
			}
			
			// 現在獲得しているアビリティに応じて、獲得したアビリティをスロットに表示させる
			int NowGetAbilityCount = 0;
			for(int i=0; i<=(AbilityNumber-1); i++){
				if(AbilityChange.isGetAbility[i]){
					NowGetAbilityCount++;
				}
			}
			AbilityChange.Abilities.transform.localPosition = new Vector2(NowGetAbilityCount * -AbilityChange.SlotImageWidth, 0 );
			AbilityChange.SelectAbilityNumber = NowGetAbilityCount;

			for (int i=0; i<AbilityChange.NowSlotAbilitiesNumList.Count; i++ ){
				if( AbilityChange.NowSlotAbilitiesNumList[i] == AbilityNumber ){
					AbilityChange.NowSlotAbilitiesNum = i;
				}
			}

		}
		PlayerPrefs.SetInt ("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility", PlayerPrefs.GetInt("Save"+ PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility") + 1);

		Destroy( this.gameObject );
	}
}