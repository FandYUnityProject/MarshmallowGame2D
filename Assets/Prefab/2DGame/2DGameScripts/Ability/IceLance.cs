using UnityEngine;
using System.Collections;

public class IceLance : MonoBehaviour {

	private float IceLancePositionX = 0.0f;
	private bool  isPlayerRightNow  = true;
	private bool  isIceLanceMakeAnimeFinish = false;
	
	void Start () {
		IceLancePositionX = 0.0f;
		
		// 手裏剣を表示させた瞬間のプレイヤーの向きを取得
		isPlayerRightNow = PlayerControll2D.isRight;
		this.name = "IceLance";

		isIceLanceMakeAnimeFinish = false;
		this.transform.localScale = new Vector2 (0, 0);
		if (isPlayerRightNow) {
			iTween.ScaleTo (gameObject, iTween.Hash ("x",  0.8f, "y", 0.8f, "time", 0.2f, "oncomplete", "CompleteIceLanceMakeAnime"));
		} else {
			iTween.ScaleTo (gameObject, iTween.Hash ("x", -0.8f, "y", 0.8f, "time", 0.2f, "oncomplete", "CompleteIceLanceMakeAnime"));
		}
	}

	void CompleteIceLanceMakeAnime(){
		isIceLanceMakeAnimeFinish = true;
	}
	
	void Update () {
		
		if (!PlayerUseAbility.isMagicUse) {
			Destroy (gameObject);
			return;
		}

		// アイスランス生成アニメが終わった後. 向いている方向に応じて、手裏剣が飛んで行く方向を変更
		if (isIceLanceMakeAnimeFinish) {
			if (isPlayerRightNow) {
				IceLancePositionX += Time.deltaTime / 3;
			} else {
				IceLancePositionX -= Time.deltaTime / 3;
			}
		
			this.transform.localPosition = new Vector2 (this.transform.localPosition.x + IceLancePositionX, this.transform.localPosition.y);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		// 手裏剣が敵、もしくは壁にヒットしたら削除
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "LeftWall" || col.gameObject.tag == "RightWall") {
			
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
