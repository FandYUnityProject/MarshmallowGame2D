using UnityEngine;
using System.Collections;

public class PistonCatch : MonoBehaviour {
	
	private GameObject PistonEffectObj;

	void Start () {
		PistonEffectObj = GameObject.Find ("PistonEffect");
		PistonEffectObj.SetActiveRecursively (false);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "PistonPaulBottom") {
			PistonEffectObj.SetActiveRecursively (false);
			PistonEffectObj.SetActiveRecursively (true);
		}
	}
}
