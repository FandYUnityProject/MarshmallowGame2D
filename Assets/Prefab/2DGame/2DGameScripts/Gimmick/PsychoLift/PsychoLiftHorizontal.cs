using UnityEngine;
using System.Collections;

public class PsychoLiftHorizontal : MonoBehaviour {

	private Animator PsychoLiftHorizontalAnimator;
	private float    PsychoLiftHorizontalStartPositionX;
	public  float    PsychoLiftHorizontalPositionXLimit;

	private GameObject PlayerObj;

	void Start () {
		PsychoLiftHorizontalAnimator = GetComponent (typeof(Animator)) as Animator;
		PsychoLiftHorizontalStartPositionX = this.transform.position.x;

		PlayerObj = GameObject.Find("Player");
	}

	void Update () {
		if (PlayerUseAbility.isPsychoEffect) {
			PsychoLiftHorizontalAnimator.speed =  1.0f;
			if( this.transform.position.x - PsychoLiftHorizontalStartPositionX < PsychoLiftHorizontalPositionXLimit ){
				this.transform.position = new Vector2(this.transform.position.x + 0.1f, this.transform.position.y);
			}
		} else {
			PsychoLiftHorizontalAnimator.speed =  0.0f;
			if( PsychoLiftHorizontalStartPositionX < this.transform.position.x ){
				this.transform.position = new Vector2(this.transform.position.x - 0.1f, this.transform.position.y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
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
