using UnityEngine;
using System.Collections;

public class CameraOutDestroy : MonoBehaviour {

	void OnBecameInvisible () {
		//カメラに映っていなければ削除
		Destroy (this.gameObject);
	}
}
