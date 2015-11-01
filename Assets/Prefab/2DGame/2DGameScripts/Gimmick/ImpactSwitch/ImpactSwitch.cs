using UnityEngine;
using System.Collections;

public class ImpactSwitch : MonoBehaviour {

	public Sprite[]   ImpactSwitchSprite;
	
	public GameObject ImpactSwitch1;
	public GameObject ImpactSwitch2;
	public GameObject ImpactSwitchBarrier1;
	public GameObject ImpactSwitchBarrier2;

	void Start () {
		ImpactSwitch1.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [0];
		ImpactSwitch2.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [0];
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.name == "Shuriken" || col.gameObject.name == "Punch" || col.gameObject.name == "Feather") {
			ImpactSwitch1.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [1];
			ImpactSwitch2.GetComponent<SpriteRenderer> ().sprite = ImpactSwitchSprite [1];
			iTween.ScaleTo (ImpactSwitchBarrier1, iTween.Hash ("x", 0f, "y", 0f, "time", 1.0f, "delay", 1.0f, "oncomplete", "CompleteDestroyImpactSwitchBarrierAnime", "easetype", iTween.EaseType.easeInBack));
			iTween.ScaleTo (ImpactSwitchBarrier2, iTween.Hash ("x", 0f, "y", 0f, "time", 1.0f, "delay", 1.0f, "oncomplete", "CompleteDestroyImpactSwitchBarrierAnime", "easetype", iTween.EaseType.easeInBack));
		}
	}
	
	void CompleteDestroyImpactSwitchBarrierAnime(){
		Destroy (this.ImpactSwitchBarrier1);
		Destroy (this.ImpactSwitchBarrier2);
	}
}
