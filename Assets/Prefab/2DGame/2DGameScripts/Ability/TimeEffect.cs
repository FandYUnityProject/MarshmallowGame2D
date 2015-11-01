using UnityEngine;
using System.Collections;

public class TimeEffect : MonoBehaviour {


	private GameObject PlayerObj;
	private GameObject TimeEffect_NeedleObj;

	public  float RotateSpeedDefault = 40.0f;
	public static float RotateSpeedDefaultStatic;
	public static float TimeEffectNeedleRotateSpeed;
	public static float TimeEffectNeedleNowRotate = 0.0f;

	void Start () {

		PlayerObj = GameObject.Find ("Player");
		TimeEffect_NeedleObj = GameObject.Find ("TimeEffect_Needle");

		RotateSpeedDefaultStatic = RotateSpeedDefault;
		TimeEffectNeedleRotateSpeed = RotateSpeedDefault;
	}

	void Update () {

		this.transform.position = new Vector3 (PlayerObj.transform.position.x, PlayerObj.transform.position.y, this.transform.position.z);

		if (PlayerUseAbility.isChronoUse) {
			if (PlayerUseAbility.isChronoHigh) {

				if (TimeEffectNeedleRotateSpeed < RotateSpeedDefault * 6) {
					TimeEffectNeedleRotateSpeed += Time.deltaTime * 4;
				} else {
					TimeEffectNeedleRotateSpeed = RotateSpeedDefault * 6;
				}
			} else if (PlayerUseAbility.isChronoSlow) {

				if (TimeEffectNeedleRotateSpeed > -RotateSpeedDefault / 4) {
					TimeEffectNeedleRotateSpeed -= Time.deltaTime * 4;
				} else {
					TimeEffectNeedleRotateSpeed = -RotateSpeedDefault / 4;
				}
			} else {
				iTween.Stop();
			}
		}

		TimeEffectNeedleNowRotate -= TimeEffectNeedleRotateSpeed;
		TimeEffect_NeedleObj.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, TimeEffectNeedleNowRotate);
	}
}
