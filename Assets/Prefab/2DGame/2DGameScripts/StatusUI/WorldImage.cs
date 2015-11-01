using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldImage : MonoBehaviour {
	
	public Sprite[] WorldImages;

	void Update () {
		this.gameObject.GetComponent<Image> ().sprite = WorldImages [WorldNumberControll.WorldNumber];
	}
}
