using UnityEngine;
using System.Collections;

public class PsychoBlock : MonoBehaviour {
	
	private Animator PsychoBlockAnimator;

	void Start(){	
		PsychoBlockAnimator = GetComponent (typeof(Animator)) as Animator;
		this.gameObject.transform.FindChild("PsychoBlockDestroy").gameObject.SetActiveRecursively (false);
	}

	void Update(){	
		PsychoBlockAnimator.speed =  WorldTimerControll.WorldSpeed;
	}

	void OnTriggerStay2D ( Collider2D col ){
		if (col.gameObject.tag == "PsychoBall") {

			StartCoroutine( "DestroyPsychoBlockEnd" );

			this.gameObject.transform.FindChild("PsychoBlockDestroy").gameObject.SetActiveRecursively (true);
			Instantiate(this.gameObject.transform.FindChild("PsychoBlockDestroy").gameObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

		}
	}

	private IEnumerator DestroyPsychoBlockEnd(){
		yield return new WaitForSeconds (0.16f);
		
		// 壁に付いたまま破壊した時、プレイヤーがフリーズする挙動を修正
		PlayerTouchObject.isTouchLeftWall = false;
		PlayerTouchObject.isTouchRightWall = false;
		
		Destroy(this.gameObject);
	}
}
