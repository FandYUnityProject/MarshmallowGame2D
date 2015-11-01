using UnityEngine;
using System.Collections;

public class PlayerTouchPiston : MonoBehaviour {
	
	private bool isTouchPistonPaulBottom;
	private bool isTouchPistonCatch;

	void Start(){
		isTouchPistonPaulBottom  = false;
		isTouchPistonCatch       = false;
	}

	void Update(){
		if (isTouchPistonPaulBottom && isTouchPistonCatch && !PlayerGameOver.isGameOver && PistonPaul.isPistonDeath) {
			StartCoroutine("DeathPress");
		}
	}

	private IEnumerator DeathPress(){
		yield return new WaitForSeconds (0.02f);
		if (!PlayerGameOver.isGameOver) {
			StatusUIControll.playerHP = 0;
			isTouchPistonPaulBottom = false;
			isTouchPistonCatch = false;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name == "PistonPaulBottom") {
			isTouchPistonPaulBottom = true;
		}
		if (col.gameObject.name == "PistonCatch") {
			isTouchPistonCatch = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name == "PistonPaulBottom") {
			isTouchPistonPaulBottom = false;
		}
		if (col.gameObject.name == "PistonCatch") {
			isTouchPistonCatch = false;
		}
	}
}
