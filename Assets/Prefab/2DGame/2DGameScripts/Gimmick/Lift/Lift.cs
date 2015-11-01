using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour {

	public int リフトの動作限界数;

	public static int NowPositionCountX;	//リフトの現在の動作数
	public static int LimitPositionCountX;	//リフトの動作限界数
	
	private Vector2 StartPosition;
	private Quaternion StartRotation;

	void Start () {
		// 初期化
		NowPositionCountX = 0;
		LimitPositionCountX = リフトの動作限界数;
		
		StartPosition = transform.position;
		StartRotation = transform.rotation;	
	}

	void Update () {

		if (PlayerGameOver.isAllObjectDefaultPosition) {
			// ゲームオーバーになったら、初期位置に戻す
			NowPositionCountX = 0;
			transform.position = StartPosition;
			transform.rotation = StartRotation;
		}
	}
}
