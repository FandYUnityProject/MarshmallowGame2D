using UnityEngine;
using System.Collections;

public class EnemyGravityChange : MonoBehaviour {

	private bool  isGravityChangeCheck = false;
	public  float EnemyGravityScale = 1.2f;

	void Update () {
		if (PlayerUseAbility.isGravityChange) {
			this.GetComponent<Rigidbody2D> ().gravityScale = -EnemyGravityScale;
			if( PlayerUseAbility.isGravityChangeAnime && !isGravityChangeCheck ){
				isGravityChangeCheck = true;
				iTween.RotateTo (gameObject, iTween.Hash ("z", -180, "time", 0.3f, "easetype", iTween.EaseType.easeInQuad));
			}
		} else {
			this.GetComponent<Rigidbody2D> ().gravityScale = EnemyGravityScale;
			if( isGravityChangeCheck ){
				isGravityChangeCheck = false;
				iTween.RotateTo (gameObject, iTween.Hash ("z",    0, "time", 0.3f, "easetype", iTween.EaseType.easeInQuad));
			}
		}
	}
}
