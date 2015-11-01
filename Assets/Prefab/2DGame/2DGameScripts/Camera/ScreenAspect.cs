using UnityEngine;
using System.Collections;

public class ScreenAspect : MonoBehaviour {

	private Camera cam;

	// 画像のサイズ
	public float ScreenWidth  = 1024f;
	public float ScreenHeight = 768f;

	// 画像のPixel Per Unit
	public float pixelPerUnit = 32f;

	void Awake(){

		float Aspect   = (float)Screen.height / (float)Screen.width;
		float BgAcpect = ScreenHeight / ScreenWidth;

		// カメラコンポーネントを取得
		cam = GetComponent<Camera> ();
		// カメラのorthographicSizeを設定
		cam.orthographicSize = (ScreenHeight / 2.0f / pixelPerUnit);

		if (BgAcpect > Aspect) {
			// 倍率
			float BgScale  = ScreenHeight / Screen.height;
			// viewport rectの幅
			float CamWidth = ScreenWidth / (Screen.width * BgScale);
			// viewportRectを設定
			cam.rect = new Rect ((1.0f - CamWidth) / 2.0f, 0.0f, CamWidth, 1.0f);
		} else {
			// 倍率
			float BgScale   = ScreenWidth / Screen.width;
			// viewport rectの幅
			float CamHeight = ScreenHeight / (Screen.height * BgScale);
			// viewportRectを設定
			cam.rect = new Rect (0.0f, (1.0f - CamHeight) / 2.0f, 1.0f, CamHeight);
		}
	}
}
