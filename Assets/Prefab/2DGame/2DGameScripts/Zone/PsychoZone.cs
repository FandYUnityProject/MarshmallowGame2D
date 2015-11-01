using UnityEngine;
using System.Collections;

public class PsychoZone : MonoBehaviour {
	
	private Animator PsychoZoneAnimator;
	private bool isPsychoZoneActive = true;

	void Start () {
		PsychoZoneAnimator = GetComponent (typeof(Animator)) as Animator;
		isPsychoZoneActive = true;
	}

	void Update () {

		PsychoZoneAnimator.speed =  WorldTimerControll.WorldSpeed;
	
		if (isPsychoZoneActive && PlayerUseAbility.isPsychoEffect) {
			isPsychoZoneActive = false;
			iTween.ScaleTo (gameObject, iTween.Hash ("x",  0.0f, "y",  0.0f, "time", 0.6f, "delay", 0.1f, "easetype", iTween.EaseType.easeInBack));
		} else if (!isPsychoZoneActive && !PlayerUseAbility.isPsychoEffect) {
			isPsychoZoneActive = true;
			iTween.ScaleTo (gameObject, iTween.Hash ("x", 0.25f, "y", 0.25f, "time", 0.6f, "delay", 0.1f, "easetype", iTween.EaseType.easeInBack));
		}
	}
}
