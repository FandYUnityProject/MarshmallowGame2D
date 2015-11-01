using UnityEngine;
using System.Collections;

public class ImpactDoubleSwitchControll : MonoBehaviour {
	
	public GameObject ImpactSwitchUp1;
	public GameObject ImpactSwitchBottom1;
	public GameObject ImpactSwitchUp2;
	public GameObject ImpactSwitchBottom2;

	public GameObject ImpactSwitchBarrier1;
	public GameObject ImpactSwitchBarrier2;
	
	public Sprite[] ImpactSwitchSprite;
	private   float SwitchOnTimer = 0.0f;

	void Update(){
		
		if (ImpactSwitchUp1.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1] && ImpactSwitchBottom1.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1]) {
			iTween.ScaleTo (ImpactSwitchBarrier1, iTween.Hash ("x", 0f, "y", 0f, "time", 1.0f, "delay", 1.0f, "oncomplete", "CompleteDestroyImpactSwitchBarrierAnime", "easetype", iTween.EaseType.easeInBack));
			iTween.ScaleTo (ImpactSwitchBarrier2, iTween.Hash ("x", 0f, "y", 0f, "time", 1.0f, "delay", 1.0f, "oncomplete", "CompleteDestroyImpactSwitchBarrierAnime", "easetype", iTween.EaseType.easeInBack));
		}

		if (ImpactSwitchUp2.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1] && ImpactSwitchBottom2.GetComponent<SpriteRenderer> ().sprite == ImpactSwitchSprite [1]) {
			iTween.ScaleTo (ImpactSwitchBarrier1, iTween.Hash ("x", 0f, "y", 0f, "time", 1.0f, "delay", 1.0f, "oncomplete", "CompleteDestroyImpactSwitchBarrierAnime", "easetype", iTween.EaseType.easeInBack));
			iTween.ScaleTo (ImpactSwitchBarrier2, iTween.Hash ("x", 0f, "y", 0f, "time", 1.0f, "delay", 1.0f, "oncomplete", "CompleteDestroyImpactSwitchBarrierAnime", "easetype", iTween.EaseType.easeInBack));
		}

		SwitchOnTimer -= Time.deltaTime;
	}

	void CompleteDestroyImpactSwitchBarrierAnime(){
		Destroy (this.ImpactSwitchBarrier1);
		Destroy (this.ImpactSwitchBarrier2);
	}
}
