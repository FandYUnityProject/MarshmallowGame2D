using UnityEngine;
using System.Collections;

public class FadeOutRight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Save" + PlaySaveDataNumber.NowPlaySaveDataNumber + "_GetAbility") == 0) {
			this.transform.position = new Vector2 (32.12f, this.transform.position.y);
		} else {
			this.transform.position = new Vector2 (35.96f, this.transform.position.y);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
