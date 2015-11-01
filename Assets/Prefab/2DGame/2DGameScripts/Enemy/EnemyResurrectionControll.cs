using UnityEngine;
using System.Collections;

public class EnemyResurrectionControll : MonoBehaviour {

	public  GameObject 敵オブジェクト;
	private GameObject EnemyObj;
	private string     EnemyName;
	
	private float EnemyStartPositionX;
	private float EnemyStartPositionY;

	private bool  isResurrectionFlg = false;

	void Start () {

		EnemyObj = 敵オブジェクト;
		EnemyName = EnemyObj.gameObject.name;
		EnemyStartPositionX = this.transform.position.x;
		EnemyStartPositionY = this.transform.position.y;

		Debug.Log ("敵の位置： " + "X:" + EnemyStartPositionX + " Y:" + EnemyStartPositionY);
	}

	void Update(){
		if (PlayerTouchObject.isGateMove) {
			// ゲートをくぐったら全ての敵の復活するためのフラグが立つ
			isResurrectionFlg = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		// 一定以上離れたか敵キャラを削除、かつ画面内に再度入った時の復活フラグを立てる
		if (col.gameObject.name == "Player") {
			isResurrectionFlg = true;
			foreach ( Transform n in gameObject.transform )
			{
				GameObject.Destroy(n.gameObject);
			}
		}
	}
	
	void OnBecameVisible (){
		//敵の初期位置がカメラに映っている時
		if (this.transform.childCount == 0) {
			// 既に倒されていて、一定以上の距離を離れていれば復活させる
			if( isResurrectionFlg ){
				isResurrectionFlg = false;
				GameObject CloneEnemy = Instantiate(EnemyObj, new Vector2(EnemyStartPositionX, EnemyStartPositionY), Quaternion.identity) as GameObject;
				CloneEnemy.transform.position = new Vector2 (EnemyStartPositionX, EnemyStartPositionY);
				CloneEnemy.transform.parent   = this.transform;
				CloneEnemy.name = EnemyName;
			}
		}
	}
}
