using UnityEngine;
using System.Collections;

public class GravityBlock : MonoBehaviour {
	
	void Update () {
		if (PlayerUseAbility.isGravityChange) {
			this.GetComponent<Rigidbody2D> ().gravityScale = -1.5f;
		} else {
			this.GetComponent<Rigidbody2D> ().gravityScale =  1.5f;
		}
	}}
