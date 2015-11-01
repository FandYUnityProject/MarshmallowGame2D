using UnityEngine;
using System.Collections;

public class Feather : MonoBehaviour {
	
	private float FeatherPositionX = 0.0f;
	private float FeatherPositionY = 0.0f;
	private float FeatherSpeedX = 0.0f;
	private float FeatherSpeedY = 0.0f;
	private bool  isPlayerRightNow  = true;
	private float isFeatherDestroyTimer  = 0.6f;

	void Start () {
		FeatherPositionX = 0.0f;
		FeatherPositionY = 0.0f;
		
		FeatherSpeedX = Random.Range (0.0080f, 0.0120f);
		FeatherSpeedY = Random.Range (-0.003f, 0.0030f);

		// 羽を表示させた瞬間のプレイヤーの向きを取得
		isPlayerRightNow = PlayerControll2D.isRight;
		this.name = "Feather";

		if (isPlayerRightNow) {	
			this.transform.localScale = new Vector2(-0.6f, 0.6f);
		} else {
			this.transform.localScale = new Vector2( 0.6f, 0.6f);
		}
	}
	
	void Update () {

		isFeatherDestroyTimer -= Time.deltaTime;
		if (isFeatherDestroyTimer <= 0) {
			Destroy (gameObject);
			return;
		}

		if (!PlayerUseAbility.isFlyUse) {
			Destroy (gameObject);
			return;
		}
		// 向いている方向に応じて、手裏剣が飛んで行く方向を変更
		if (isPlayerRightNow) {	
			FeatherPositionX += FeatherSpeedX; 
			FeatherPositionY += FeatherSpeedY; 
		} else { 
			FeatherPositionX -= FeatherSpeedX; 
			FeatherPositionY += FeatherSpeedY; 
		}
		
		this.transform.localPosition = new Vector2 (this.transform.localPosition.x + FeatherPositionX, this.transform.localPosition.y + FeatherPositionY);
		if (isPlayerRightNow) {	
			this.transform.eulerAngles = new Vector3 (this.transform.rotation.x, this.transform.rotation.y, FeatherSpeedY *  5000);
		} else {
			this.transform.eulerAngles = new Vector3 (this.transform.rotation.x, this.transform.rotation.y, FeatherSpeedY * -5000);
		}
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
