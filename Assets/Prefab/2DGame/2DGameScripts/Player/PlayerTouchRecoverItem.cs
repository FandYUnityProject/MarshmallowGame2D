using UnityEngine;
using System.Collections;

public class PlayerTouchRecoverItem : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "RecoverItem") {

			// 回復アイテムを取得したら、回復して回復アイテム削除
			if( col.gameObject.name == "Cake"    ){ StatusUIControll.playerHP  = 100;   Destroy(col.gameObject); }
			if( col.gameObject.name == "Choko"   ){ StatusUIControll.playerHP +=  40;   Destroy(col.gameObject); }
			if( col.gameObject.name == "Dounuts" ){ StatusUIControll.playerHP +=  20;   Destroy(col.gameObject); }
			if( col.gameObject.name == "Ichigo"  ){ StatusUIControll.playerHP +=  10;   Destroy(col.gameObject); }
			if( col.gameObject.name == "1UP"     ){ StatusUIControll.RemainingNumber++; Destroy(col.gameObject); }
		}
	}
}
