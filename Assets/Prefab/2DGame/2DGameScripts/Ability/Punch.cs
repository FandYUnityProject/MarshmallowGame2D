using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour {

	// 壊せるタイミングかどうか（パンチを出し尽くした後は破壊できない）
	public        bool isCanDestroyTiming       = false;	// 壊せるタイミングはAnimationで設定
	public static bool isCanDestroyTimingStatic = false;

	private GameObject PlayerObj;

	void Start(){
		// 初期化
		isCanDestroyTimingStatic = false;

		PlayerObj = GameObject.Find ("Player");
	}

	void Update(){
		// プレイヤーが向いている方向に応じて、パンチの方向も変える
		if (PlayerControll2D.isRight) {
			this.transform.localScale = new Vector2 ( PlayerUseAbility.PunchSize, PlayerUseAbility.PunchSize);
			this.transform.localPosition = new Vector2 (PlayerObj.transform.position.x + 0.5f, PlayerObj.transform.position.y + 0.1f);
		} else {
			this.transform.localScale = new Vector2 (-PlayerUseAbility.PunchSize, PlayerUseAbility.PunchSize);
			this.transform.localPosition = new Vector2 (PlayerObj.transform.position.x - 0.5f, PlayerObj.transform.position.y + 0.1f);
		}

		isCanDestroyTimingStatic = isCanDestroyTiming;
		if (!isCanDestroyTimingStatic) {
			// アニメーションが終わったら非表示にする
			this.gameObject.SetActiveRecursively (false);
		}
	}
}
