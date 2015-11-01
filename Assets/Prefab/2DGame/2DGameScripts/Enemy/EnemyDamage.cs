using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

	#region 変数宣言
	private bool  isInvincibleEnemy 	= false;	// 無敵中
	public  float InvincibleTimeLimit	= 0.4f;		// 無敵時間
	private float InvincibleTimer    	= 0.0f;		// 無敵時間計測用タイマー

	public  int 敵キャラのヒットポイント = 100;	// 敵キャラのHP
	private int EnemyHP;
	#endregion
	
	private bool isEnemyInCamera = false;
	private bool isPlayerRight   = false;

	void Start(){
		
		#region 無敵時間計測用タイマー初期化
		// 無敵時間計測用タイマー初期化
		InvincibleTimer    	= 0.0f;
		#endregion

		EnemyHP = 敵キャラのヒットポイント;
		StartCoroutine ("EnemyInCamera");
	}

	void Update(){
		
		#region 無敵中の時のタイマー処理
		// 無敵中の時、タイマーを動かす
		if (isInvincibleEnemy) {
			InvincibleTimer += Time.deltaTime;
			if( InvincibleTimer > InvincibleTimeLimit ){
				// 無敵時間が終了したら、フラグを下ろしてタイマー初期化
				isInvincibleEnemy = false;
				InvincibleTimer   = 0.0f;
			}
		}
		#endregion

		// HPが0になったら削除
		if (EnemyHP <= 0) {
			this.gameObject.GetComponent<Collider2D>().isTrigger = true;
			EnemyHP = 敵キャラのヒットポイント;
			StartCoroutine("EnemyDestroy");
		}
	}

	private IEnumerator EnemyDestroy(){
		if( !PlayerControll2D.isRight ){
			this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100000, 50000));
		} else {
			this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 100000, 50000));
		}
		yield return new WaitForSeconds (1.0f);
		Destroy(this.gameObject);
	}

	void OnTriggerStay2D(Collider2D col){
		
		#region パンチを受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "Panch") {
			if( !isInvincibleEnemy ){

				// 無敵中で無ければ無敵中にする
				isInvincibleEnemy = true;

				// 無敵エフェクト
				StartCoroutine("InvincibleEffectTime");

				#region オブジェクトの位置に応じてノックバック
				// オブジェクトの位置に応じてノックバック
				if( this.transform.position.x < col.transform.position.x ){
					isPlayerRight = false;
					if( !PlayerUseAbility.isStrengthMode ){
						this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 500));
					} else {
						this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3000,1500));
					}
				} else {
					isPlayerRight = true;
					if( !PlayerUseAbility.isStrengthMode ){
						this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 1000, 500));
					} else {
						this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 3000,1500));
					}
				}
				#endregion

				if( !PlayerUseAbility.isStrengthMode ){
					EnemyHP -= 30;
				} else {
					EnemyHP -= 50;
				}
			}
		}

		#region 手裏剣を受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "Shuriken") {
			if( !isInvincibleEnemy ){
				
				// 無敵中で無ければ無敵中にする
				isInvincibleEnemy = true;
				
				// 無敵エフェクト
				StartCoroutine("InvincibleEffectTime");
				
				#region オブジェクトの位置に応じてノックバック
				// オブジェクトの位置に応じてノックバック
				if( this.transform.position.x < col.transform.position.x ){
					isPlayerRight = false;
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 500));
				} else {
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 1000, 500));
				}
				#endregion
				
				EnemyHP -= 20;
			}
		}
		#endregion

		#region 羽を受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "Feather") {
			if( !isInvincibleEnemy ){
				
				// 無敵中で無ければ無敵中にする
				isInvincibleEnemy = true;
				
				// 無敵エフェクト
				StartCoroutine("InvincibleEffectTime");

				EnemyHP -= 20;
			}
		}
		#endregion

		#region アイスランスを受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "IceLance") {
			if( !isInvincibleEnemy ){
				
				// 無敵中で無ければ無敵中にする
				isInvincibleEnemy = true;
				
				// 無敵エフェクト
				StartCoroutine("InvincibleEffectTime");

				#region オブジェクトの位置に応じてノックバック
				// オブジェクトの位置に応じてノックバック
				if( this.transform.position.x < col.transform.position.x ){
					isPlayerRight = false;
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 500));
				} else {
					isPlayerRight = true;
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 1000, 500));
				}
				#endregion
				
				EnemyHP -= 20;
			}
		}
		#endregion

		#region 雷を受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "Lightning") {
			if( !isInvincibleEnemy ){
				
				// 無敵中で無ければ無敵中にする
				isInvincibleEnemy = true;
				
				// 無敵エフェクト
				StartCoroutine("InvincibleEffectTime");
				
				#region ノックバック
				// ノックバック
				this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 0, 3000));
				#endregion
				
				EnemyHP -= 20;
			}
		}
		#endregion

		#region 炎を受けた処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "FireShield") {
			if( !isInvincibleEnemy ){
				
				// 無敵中で無ければ無敵中にする
				isInvincibleEnemy = true;
				
				// 無敵エフェクト
				StartCoroutine("InvincibleEffectTime");
				
				#region オブジェクトの位置に応じてノックバック
				// オブジェクトの位置に応じてノックバック
				if( this.transform.position.x < col.transform.position.x ){
					isPlayerRight = false;
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 500));
				} else {
					this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( 1000, 500));
				}
				#endregion
				
				EnemyHP -= 20;
			}
		}
		#endregion
	}
	#endregion



	// カメラに映っていなかったら削除
	void OnBecameVisible () {
		isEnemyInCamera = true;
	}

	// ゲーム開始時にカメラに入っていなければ削除
	private IEnumerator EnemyInCamera(){
		yield return new WaitForSeconds (0.3f);
		if (!isEnemyInCamera) {
			Destroy(this.gameObject);
		}
	}

	private IEnumerator InvincibleEffectTime(){

		for (int i=1; i<=8; i++) {
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.5f);
			yield return new WaitForSeconds (0.025f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1.0f);
			yield return new WaitForSeconds (0.025f);
		}
	}
}