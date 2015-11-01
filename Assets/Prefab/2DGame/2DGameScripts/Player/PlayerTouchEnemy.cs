using UnityEngine;
using System.Collections;

public class PlayerTouchEnemy : MonoBehaviour {

	#region 変数宣言
	private bool  isInvinciblePlayer 	= false;	// 無敵中
	public  float InvincibleTimeLimit	= 1.5f;		// 無敵時間
	private float InvincibleTimer    	= 0.0f;		// 無敵時間計測用タイマー

	public static bool isDamage = false;
	
	private GameObject DamageEffect; // ダメージエフェクトオブジェクト
	private GameObject NeedleDamageEffect; // ダメージエフェクトオブジェクト(針用)
	#endregion

	void Start(){

		#region 無敵時間計測用タイマー初期化
		// 無敵時間計測用タイマー初期化
		InvincibleTimer    	= 0.0f;
		#endregion

		#region ダメージエフェクトをセット
		// ダメージエフェクトをセット
		DamageEffect = GameObject.Find ("DamageEffect");
		NeedleDamageEffect = GameObject.Find("NeedleDamageEffect");
		DamageEffect.SetActiveRecursively (false);
		NeedleDamageEffect.SetActiveRecursively (false);
		#endregion
	}

	void Update(){

		#region 無敵中の時のタイマー処理
		// 無敵中の時、タイマーを動かす
		if (isInvinciblePlayer) {
			InvincibleTimer += Time.deltaTime;
			if( InvincibleTimer > InvincibleTimeLimit ){
				// 無敵時間が終了したら、フラグを下ろしてタイマー初期化
				isInvinciblePlayer = false;
				InvincibleTimer    	= 0.0f;
			}
		}
		#endregion
	}

	void OnTriggerStay2D(Collider2D col){

		#region 敵に衝突時の処理
		//--- 敵に衝突時の処理 ---//
		if (col.gameObject.tag == "Enemy") {
			if( !isInvinciblePlayer ){

				#region 無敵中で無ければ無敵中にする
				// 無敵中で無ければ無敵中にする
				isInvinciblePlayer = true;
				#endregion

				#region ダメージエフェクトの表示
				// ダメージエフェクトの表示
				DamageEffect.SetActiveRecursively (false);
				DamageEffect.SetActiveRecursively (true);
				#endregion

				#region プレイヤーの向きに応じてノックバック
				// プレイヤーの向きに応じてノックバック
				isDamage = true;
				this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				if( PlayerControll2D.isRight ){
					iTween.MoveTo (gameObject, iTween.Hash ("x", this.transform.position.x - 0.4f, "time", 0.5f, "oncomplete", "CompletePlayerDamageAnime"));
				} else {
					iTween.MoveTo (gameObject, iTween.Hash ("x", this.transform.position.x + 0.4f, "time", 0.5f, "oncomplete", "CompletePlayerDamageAnime"));
				}
				#endregion

				#region 黒トゲ衝突時
				// 黒トゲ衝突時
				if(col.gameObject.name == "TogeTogeBlack"){
					if(!PlayerUseAbility.isStrengthMode ){
						StatusUIControll.playerHP -= 20;
					} else {
						// 肉体強化モード時は半減
						StatusUIControll.playerHP -= 10;
					}
				}
				#endregion

				#region 赤トゲ衝突時
				// 赤トゲ衝突時
				if(col.gameObject.name == "TogeTogeRed"){
					if(!PlayerUseAbility.isStrengthMode ){
						StatusUIControll.playerHP -= 30;
					} else {
						// 肉体強化モード時は半減
						StatusUIControll.playerHP -= 15;
					}
				}
				#endregion

				#region 
				// レーザービーム衝突時
				if(col.gameObject.name == "LaserBiim"){
					StatusUIControll.playerHP -= 20;
				}
				#endregion

			}
		}
		#endregion

		#region 針接触時の処理
		//--- 針接触時の処理 ---//
		if (col.gameObject.name == "Needle" && !PlayerUseAbility.isStrengthMode) {
			// 肉体強化モード時は無効
			if( !isInvinciblePlayer ){
			
				// 無敵中で無ければ無敵中にする
				isInvinciblePlayer = true;

				#region ダメージエフェクトの表示
				// ダメージエフェクトの表示
				NeedleDamageEffect.SetActiveRecursively (false);
				NeedleDamageEffect.SetActiveRecursively (true);
				#endregion

				#region プレイヤーの向きに応じてノックバック
				// ノックバック
				this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				iTween.MoveTo (gameObject, iTween.Hash ("y", this.transform.position.y + 0.8f, "time", 0.5f, "oncomplete", "CompletePlayerDamageAnime"));
				#endregion

				StatusUIControll.playerHP -= 30;
			}
		}
		#endregion
	}

	void CompletePlayerDamageAnime(){
		// ダメージアニメが終了してから無敵時間に入る
		this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		isInvinciblePlayer = true;
		isDamage = false;

		// 無敵エフェクト
		InvincibleTimer    	= 0.0f;
		StartCoroutine("InvincibleEffectTime");
	}

	private IEnumerator InvincibleEffectTime(){
		
		for (int i=1; i<=20; i++) {
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.5f);
			yield return new WaitForSeconds (0.025f);
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1.0f);
			yield return new WaitForSeconds (0.025f);
		}
	}
}
