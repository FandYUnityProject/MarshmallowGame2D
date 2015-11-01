using UnityEngine;
using System.Collections;

public class EnemyMovePingPong : MonoBehaviour {
	
	private float StartPositionX;
	private float StartPositionY;
	
	public  bool 直線運動 = false;
	public  bool 円運動   = false;
	
	public  float X軸直線移動距離 = 1.0f;
	public  float Y軸直線移動距離 = 1.0f;
	
	public  float X軸直線移動速度 = 1.0f;
	public  float Y軸直線移動速度 = 1.0f;
	
	public  float 円運動速度 = 1.0f;
	public  float 円運動半径 = 1.0f;

	void Start () {
		StartPositionX = this.transform.position.x;
		StartPositionY = this.transform.position.y;

		// 直線運動優先
		if (直線運動) {
			円運動 = false;
		} else if (!直線運動 && !円運動) {
			直線運動 = true;
		}
	}

	void Update () {


		if (直線運動) {
			if (X軸直線移動距離 != 0 && Y軸直線移動距離 != 0) {
				this.transform.position = new Vector2 (StartPositionX + Mathf.PingPong (WorldTimerControll.WorldTimer * X軸直線移動速度, X軸直線移動距離) - (X軸直線移動距離 / 2)
			                                         , StartPositionY + Mathf.PingPong (WorldTimerControll.WorldTimer * Y軸直線移動速度, Y軸直線移動距離) - (Y軸直線移動距離 / 2));
			} else if (X軸直線移動距離 == 0 && Y軸直線移動距離 != 0) {
				this.transform.position = new Vector2 (this.transform.position.x
				                                     , StartPositionY + Mathf.PingPong (WorldTimerControll.WorldTimer * Y軸直線移動速度, Y軸直線移動距離) - (Y軸直線移動距離 / 2));
			} else if (X軸直線移動距離 != 0 && Y軸直線移動距離 == 0) {
				this.transform.position = new Vector2 (StartPositionX + Mathf.PingPong (WorldTimerControll.WorldTimer * X軸直線移動速度, X軸直線移動距離) - (X軸直線移動距離 / 2)
			                                         , this.transform.position.y);
			}
		}

		if (円運動) {
			this.transform.position = new Vector3 (StartPositionX + Mathf.Cos (WorldTimerControll.WorldTimer * 円運動速度 / Mathf.PI) * 円運動半径, StartPositionY + Mathf.Sin (WorldTimerControll.WorldTimer * 円運動速度 / Mathf.PI) * 円運動半径);
		}
	}
}
