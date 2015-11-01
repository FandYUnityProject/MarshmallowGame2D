using UnityEngine;
using System.Collections;

public class WorldChanger : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name == "Player") {
			for( int WorldNumber=0; WorldNumber<=9; WorldNumber++ ){
				if( this.gameObject.name.LastIndexOf("_" + WorldNumber) != -1 ){
					// WorldChanger[WorldNumber]に触れたら、WorldImageをWorldNumberの画像に変更
					WorldNumberControll.WorldNumber = WorldNumber;

					// Application.LoadLevel("SaveData");
				}
			}
		}
	}
}
