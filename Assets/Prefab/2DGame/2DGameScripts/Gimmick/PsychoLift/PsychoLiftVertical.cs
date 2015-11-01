using UnityEngine;
using System.Collections;

public class PsychoLiftVertical : MonoBehaviour {

	private Animator PsychoLiftVerticalAnimator;
	private float    PsychoLiftVerticalStartPositionY;
	public  float    PsychoLiftVerticalPositionYLimit;
	
	private GameObject PlayerObj;

	void Start () {
		PsychoLiftVerticalAnimator = GetComponent (typeof(Animator)) as Animator;
		PsychoLiftVerticalStartPositionY = this.transform.position.y;

		PlayerObj = GameObject.Find("Player");
	}

	void Update () {
		if (PlayerUseAbility.isPsychoEffect) {
			PsychoLiftVerticalAnimator.speed =  1.0f;
			if( this.transform.position.y - PsychoLiftVerticalStartPositionY < PsychoLiftVerticalPositionYLimit ){
				this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 0.1f);
			}
		} else {
			PsychoLiftVerticalAnimator.speed =  0.0f;
			if( PsychoLiftVerticalStartPositionY < this.transform.position.y ){
				this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.1f);
			}
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name == "Player") {
			// 一時的にプレイヤーをサイコリフトの子オブジェクトにする
			PlayerObj.transform.parent = this.gameObject.transform;
			if (PlayerControll2D.isRight) {
				PlayerObj.transform.localScale = new Vector2( 0.135f, 0.135f);
			} else {
				PlayerObj.transform.localScale = new Vector2(-0.135f, 0.135f);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		// 子オブジェクトの解除
		PlayerObj.transform.parent = null;
	}
}
