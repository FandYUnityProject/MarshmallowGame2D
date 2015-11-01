using UnityEngine;
using System.Collections;

public class LiftCenter : MonoBehaviour {
	
	// 初期位置取得変数
	private Vector2    StartPosition;
	private Quaternion StartRotation;

	void Start () {
		StartPosition = transform.position;
		StartRotation = transform.rotation;	
	}
	
	void Update () {
		if (PlayerGameOver.isAllObjectDefaultPosition) {
			// ゲームオーバーになったら、初期位置に戻す
			transform.position = StartPosition;
			transform.rotation = StartRotation;
		}
	}
}
