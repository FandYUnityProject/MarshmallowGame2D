using UnityEngine;
using System.Collections;

public class DestroyMetalBlock : MonoBehaviour {

	private bool isDestroy = false;

	void OnTriggerEnter2D(Collider2D col){
		// 壊せるタイミング
		if( Punch.isCanDestroyTimingStatic ){
			if (col.gameObject.name == "Punch") {
				//--- メタルブロックで肉体強化モードなら破壊 ---//
				if( PlayerUseAbility.isStrengthUse ){

					StartCoroutine( "DestroyMetalBlockEnd" );

					this.gameObject.GetComponent<Collider2D> ().enabled = false;
					if( !isDestroy ){
						Destroy(this.gameObject.transform.FindChild("MetalBlockLeft").gameObject);
						Destroy(this.gameObject.transform.FindChild("MetalBlockRight").gameObject);
						isDestroy = true;
					}

					this.gameObject.transform.FindChild("MetalBlockDestroy").gameObject.SetActiveRecursively (false);
					this.gameObject.transform.FindChild("MetalBlockDestroy").gameObject.SetActiveRecursively (true);
				}
			}
		}
	}

	private IEnumerator DestroyMetalBlockEnd(){
		yield return new WaitForSeconds (0.16f);

		// 壁に付いたまま破壊した時、プレイヤーがフリーズする挙動を修正
		PlayerTouchObject.isTouchLeftWall = false;
		PlayerTouchObject.isTouchRightWall = false;

		Destroy(this.gameObject);
	}
}
