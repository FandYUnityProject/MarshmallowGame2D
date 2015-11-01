using UnityEngine;
using System.Collections;

public class CameraInOutCollider : MonoBehaviour {

	void Start(){
		//当たり判定無効
		if(GetComponent<Collider> ()){
			GetComponent<Collider> ().enabled   = false;
		} else if(GetComponent<Collider2D> ()){
			GetComponent<Collider2D> ().enabled = false;
		}
	}

	void OnBecameVisible (){
		//カメラに映っていれば当り判定有効
		if(GetComponent<Collider> ()){
			GetComponent<Collider> ().enabled   = true;
		} else if(GetComponent<Collider2D> ()){
			GetComponent<Collider2D> ().enabled = true;
		}
	}

	void OnBecameInvisible () {
		//カメラに映っていなければ当たり判定無効
		if(GetComponent<Collider> ()){
			GetComponent<Collider> ().enabled   = false;
		} else if(GetComponent<Collider2D> ()){
			GetComponent<Collider2D> ().enabled = false;
		}
	}
}
