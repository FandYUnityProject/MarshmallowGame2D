using UnityEngine;
using System.Collections;

public class GravityChangeEffect : MonoBehaviour {
	
	public float GravityChangeEffectRotateSpeed = 3.0f;

	void Update () {
		transform.Rotate (0, 0, GravityChangeEffectRotateSpeed);
	}
}
