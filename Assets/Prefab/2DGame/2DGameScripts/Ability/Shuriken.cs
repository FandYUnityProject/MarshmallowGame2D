using UnityEngine;
using System.Collections;

public class Shuriken : MonoBehaviour {
	
	private float ShurikenPositionX = 0.0f;
	private bool  isPlayerRightNow  = true;

	void Start () {
		ShurikenPositionX = 0.0f;

		// 手裏剣を表示させた瞬間のプレイヤーの向きを取得
		isPlayerRightNow = PlayerControll2D.isRight;
		this.name = "Shuriken";
	}

	void Update () {

		if (!PlayerUseAbility.isAvatarUse) {
			Destroy (gameObject);
			return;
		}
		// 向いている方向に応じて、手裏剣が飛んで行く方向を変更
		if (isPlayerRightNow) {	ShurikenPositionX += Time.deltaTime / 3; }
		else { ShurikenPositionX -= Time.deltaTime / 3; }

		this.transform.localPosition = new Vector2 (this.transform.localPosition.x + ShurikenPositionX, this.transform.localPosition.y);
	}

	void OnTriggerEnter2D(Collider2D col){
		// 手裏剣が敵、もしくは壁,もしくは衝撃スイッチにヒットしたら削除
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "LeftWall" || col.gameObject.tag == "RightWall" || col.gameObject.tag == "ImpactSwitch") {

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
