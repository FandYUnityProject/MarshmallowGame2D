using UnityEngine;
using System.Collections;

public class ImpactDoubleSwitch : MonoBehaviour {

	public Sprite[] ImpactSwitchSprite;
	private   float SwitchOnTimer = 0.0f; 
	
	public GameObject ImpactSwitchUp1;
	public GameObject ImpactSwitchBottom1;
	public GameObject ImpactSwitchUp2;
	public GameObject ImpactSwitchBottom2;
	
	void Start () {
		this.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [0];
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Shuriken" || col.gameObject.name == "Punch" || col.gameObject.name == "Feather") {
			SwitchOnTimer = 0.025f;
		}
	}

	void Update(){

		if ((ImpactSwitchUp1.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1] && ImpactSwitchBottom1.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1])
		    || ImpactSwitchUp2.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1] && ImpactSwitchBottom2.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1]) {
			ImpactSwitchUp1.GetComponent<SpriteRenderer> ().sprite     = ImpactSwitchSprite [1];
			ImpactSwitchBottom1.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [1];
			ImpactSwitchUp2.GetComponent<SpriteRenderer> ().sprite     = ImpactSwitchSprite [1];
			ImpactSwitchBottom2.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [1];
		} else {
			if (SwitchOnTimer <= 0.0f) {
				SwitchOnTimer = 0.0f;
				this.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [0];
			} else {
				this.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [1];
			}
		}

		SwitchOnTimer -= Time.deltaTime;
	}
}
