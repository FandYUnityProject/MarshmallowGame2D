using UnityEngine;
using System.Collections;

public class BigPsychoBall : MonoBehaviour {
	
	private float PsychoBallStartPositionX = 0.0f;
	private float PsychoBallPositionX = 0.0f;
	private bool  isPlayerRightNow  = true;

	private bool  isPsychoTweenAnime = false;
	
	private GameObject PlayerObj;
	
	void Start () {

		PsychoBallPositionX = 0.0f;
		isPsychoTweenAnime = false;
		this.name = "BigPsychoBall";

		// サイコボールを表示させた瞬間のプレイヤーの向きを取得
		isPlayerRightNow = PlayerControll2D.isRight;
		PlayerObj = GameObject.Find ("Player");

		// 向いている方向に応じて、サイコボールの初期値を変更
		if (isPlayerRightNow) {
			PsychoBallStartPositionX = PlayerObj.transform.localPosition.x + 0.8f;
		} else {
			PsychoBallStartPositionX = PlayerObj.transform.localPosition.x - 0.8f;
		}
	}
	
	void Update () {

		if (!PlayerUseAbility.isPsychoUse) {
			Destroy (gameObject);
			return;
		}
		
		// 向いている方向に応じて、サイコボールが飛んで行く方向を変更
		if (isPlayerRightNow) {
			// 一定の距離まで移動
			if (this.transform.localPosition.x - PsychoBallStartPositionX <  1.5f) {
				PsychoBallPositionX += Time.deltaTime * 5;
			} else {
				if( !isPsychoTweenAnime ){
					iTween.ScaleTo(gameObject, iTween.Hash("x", 0.5f, "y", 0.5f, "time", 0.6f, "delay", 0.5f, "oncomplete", "CompletePsychoTweenAnimeIn", "easetype", iTween.EaseType.easeInBack));
					isPsychoTweenAnime = true;
				}
			}
		} else {
			// 一定の距離まで移動
			if (this.transform.localPosition.x - PsychoBallStartPositionX > -1.5f) {
				PsychoBallPositionX -= Time.deltaTime * 5;
			} else {
				if( !isPsychoTweenAnime ){
					iTween.ScaleTo(gameObject, iTween.Hash("x", 0.5f, "y", 0.5f, "time", 0.6f, "delay", 0.5f, "oncomplete", "CompletePsychoTweenAnimeIn", "easetype", iTween.EaseType.easeInBack));
					isPsychoTweenAnime = true;
				}
			}
		}
		
		this.transform.localPosition = new Vector2 (PsychoBallStartPositionX + PsychoBallPositionX, this.transform.localPosition.y);

	}

	void CompletePsychoTweenAnimeIn(){
		iTween.ScaleTo(gameObject, iTween.Hash("x", 0.0f, "y", 0.0f, "time", 0.6f, "delay", 2.5f, "oncomplete", "CompletePsychoTweenAnimeOut", "easetype", iTween.EaseType.easeInBack));
	}

	void CompletePsychoTweenAnimeOut(){
		Destroy (this.gameObject);
	}

	
	void OnTriggerEnter2D(Collider2D col){
		// サイコボールが敵、もしくは壁にヒットしたら削除
		/*
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
		*/
	}
}
