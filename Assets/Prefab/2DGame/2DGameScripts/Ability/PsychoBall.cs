using UnityEngine;
using System.Collections;

public class PsychoBall : MonoBehaviour {
	
	private float PsychoBallPositionX = 0.0f;
	private bool  isPlayerRightNow  = true;

	void Start () {
		PsychoBallPositionX = 0.0f;

		// サイコボールを表示させた瞬間のプレイヤーの向きを取得
		isPlayerRightNow = PlayerControll2D.isRight;
		this.name = "PsychoBall";
	}

	void Update () {

		if (!PlayerUseAbility.isPsychoUse) {
			Destroy (gameObject);
			return;
		}

		// 向いている方向に応じて、サイコボールが飛んで行く方向を変更
		if (isPlayerRightNow) {	PsychoBallPositionX += Time.deltaTime / 6; }
		else { PsychoBallPositionX -= Time.deltaTime / 6; }

		this.transform.localPosition = new Vector2 (this.transform.localPosition.x + PsychoBallPositionX, this.transform.localPosition.y);
	}

	void OnTriggerEnter2D(Collider2D col){
		// サイコボールが敵、もしくは壁にヒットしたら削除（サイコブロックの時は削除しない）
		if (col.gameObject.name != "PsychoBlock" && (col.gameObject.tag == "Enemy" || col.gameObject.tag == "LeftWall" || col.gameObject.tag == "RightWall")) {

			// トゲトゲキャラ（ダメージ無効キャラ）なら弾かれる音を出力
			if( col.gameObject.name == "TogeTogeBlack" || col.gameObject.name == "TogeTogeRed" ){

			} else if(col.gameObject.tag == "LeftWall" || col.gameObject.tag == "RightWall"){
				// 壁ならサクっという音
			} else {
				// その他の敵ならザクッ！とした音
			}
			Destroy (gameObject);
		}
	}
}
