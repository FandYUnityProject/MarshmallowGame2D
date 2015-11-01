using UnityEngine;
using System.Collections;

public class PlayerComplianceCamera : MonoBehaviour {

	private GameObject playerObj;
	private float CameraDefaultPosY;

	void Start () {
		playerObj = GameObject.Find ("Player");
		CameraDefaultPosY = transform.position.y;
		Debug.Log ("CameraDefaultPosY :" + CameraDefaultPosY);
	}
	
	// Update is called once per frame
	void Update () {
		//--- イベントシーンじゃない、かつセーブマップでなければ、プレーヤーに追随 ---//
		if (!EventSceneControll.EventScene) {
			if (!PlayerTouchObject.isSaveMap) {
				// カメラ中央より右側まできたら右に移動開始
				if (playerObj.transform.position.x >= transform.position.x) {
					transform.position = new Vector3 (playerObj.transform.position.x, transform.position.y, transform.position.z);
				}
				// カメラの初期中央値より高い位置に行くと上に移動開始
				if (playerObj.transform.position.y >= CameraDefaultPosY) {
					transform.position = new Vector3 (transform.position.x, playerObj.transform.position.y, transform.position.z);
				}
				//画面のある程度左側に移動したら左側に移動
				if (transform.position.x - playerObj.transform.position.x >= 3.5f) {
					transform.position = new Vector3 (playerObj.transform.position.x + 3.5f, transform.position.y, transform.position.z);
				}
			}
		}
	}
}
