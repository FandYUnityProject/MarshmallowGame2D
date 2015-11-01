using UnityEngine;
using System.Collections;

public class PlayerTouchZone : MonoBehaviour {
	
	public static bool isGravityResetZone = false;

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "PsychoZone") {
			// サイコエフェクト使用中でなければ、ダメージ
			if( !PlayerUseAbility.isPsychoEffect ){
				StatusUIControll.playerHP--;
			}
		}
		
		if (col.gameObject.tag == "GravityResetZone") {
			#region 重力リセットゾーンに触れると、重力がリセットされる
			//--- 重力リセットゾーンに触れると、重力がリセットされる ---//

			isGravityResetZone = true;
			this.GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
			#endregion
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "GravityResetZone") {
			isGravityResetZone = false;
		}
	}
}
