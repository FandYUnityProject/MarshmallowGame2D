using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Event01Controll : MonoBehaviour {
	
	private Image      BlackOutImage;	// 画面暗転用イメージ

	//--- 表示させるメッセージ関連 ---//
	public  EventMessageControll 	 EventMessageControllerClass;	// EventMessageControllのClass
	
	public  string[] scenarios;		// シナリオを格納する
	
	public  GameObject textCanvas;	// uGUIのテキストキャンバス
	public  GameObject textPanel;	// uGUIのテキストPanel
	
	[SerializeField] [Range(0.001f, 0.3f)]
	float intervalForCharacterTextSpeed = 0.05f;	// 1文字の表示にかかる時間

	// メッセージのフラグ
	private       bool isEventEnd;		// イベントが終わったか

	// 各フェーズの完了フラグ
	private bool Phase01 = false;
	private bool Phase02 = false;
	private bool Phase03 = false;
	private bool Phase04 = false;
	
	public GameObject[] 動かしたいゲームオブジェクト;
	/* 0...Player
	 * 1...Main Camera
	 *
	 */


	void Start(){
		// 画面暗転用イメージ
		BlackOutImage = GameObject.Find ("BlackOut").GetComponent<Image> ();

		// イベント終了フラグを下ろす
		isEventEnd = false;
		// メッセージを表示させたらダメ
		EventMessageControll.isCanMessage = false;
		// 会話終了フラグを下ろす
		EventSceneControll.isTextEnd = false;

		// イベント開始
		StartCoroutine ("Event01_Phase01");
	}

	// フェーズ1
	IEnumerator Event01_Phase01(){

		Debug.Log (動かしたいゲームオブジェクト [0].transform.localPosition.x);
		//  カメラ位置調整
		動かしたいゲームオブジェクト[1].transform.localPosition = new Vector3 (動かしたいゲームオブジェクト[0].transform.position.x, 動かしたいゲームオブジェクト[1].transform.position.y, 動かしたいゲームオブジェクト[1].transform.position.z);

		EventMessageControll.isCanMessage = false;
		yield return new WaitForSeconds (4.0f);
		EventMessageControll.isCanMessage = true;

		// テキストウィンドウの大きさを初期化し、アニメーションスタート
		textPanel.transform.localScale = new Vector3 (1.0f, 0.0f, 1.0f);
		iTween.ScaleTo (textPanel, iTween.Hash ("scale", new Vector3 (1.0f, 1.0f, 1.0f), "time", 0.3f));
		
		// TextControllerのStartScenarios関数を実行
		textCanvas.SetActive (true);
		EventMessageControllerClass.StartScenarios (scenarios, intervalForCharacterTextSpeed);

		Phase01 = true;
		EventMessageControll.isCanMessage = false;
	}

	IEnumerator Event01_Phase02(){
		
		EventMessageControll.isCanMessage = false;

		yield return new WaitForSeconds (0.5f);
		動かしたいゲームオブジェクト [0].transform.localScale = new Vector2 (-0.135f, 0.135f);
		yield return new WaitForSeconds (0.5f);
		動かしたいゲームオブジェクト [0].transform.localScale = new Vector2 ( 0.135f, 0.135f);
		yield return new WaitForSeconds (0.5f);
		動かしたいゲームオブジェクト [0].transform.localScale = new Vector2 (-0.135f, 0.135f);
		yield return new WaitForSeconds (0.5f);
		動かしたいゲームオブジェクト [0].transform.localScale = new Vector2 ( 0.135f, 0.135f);
		yield return new WaitForSeconds (1.0f);
		EventMessageControll.isCanMessage = true;

		// テキストウィンドウの大きさを拡大
		iTween.ScaleTo (textPanel, iTween.Hash ("scale", new Vector3 (1.0f, 1.0f, 1.0f), "time", 0.3f));

		// 次の行へ
		EventMessageControllerClass.SetNextLine (true,1);

		Phase02 = true;
	}

	IEnumerator Event01_Phase03(){
		
		EventMessageControll.isCanMessage = false;
		
		// プレイヤーの移動
		yield return new WaitForSeconds (0.5f);
		float PlayerPositionX = 動かしたいゲームオブジェクト [0].transform.localPosition.x;
		float PlayerPositionY = 動かしたいゲームオブジェクト [0].transform.localPosition.y;
		iTween.MoveTo (動かしたいゲームオブジェクト [0], iTween.Hash ("x", PlayerPositionX + 10.0f , "time", 20.0f));

		yield return new WaitForSeconds (1.5f);
		EventMessageControll.isCanMessage = true;

		// テキストウィンドウの大きさを拡大
		iTween.ScaleTo (textPanel, iTween.Hash ("scale", new Vector3 (1.0f, 1.0f, 1.0f), "time", 0.3f));
		// 次の行へ
		EventMessageControllerClass.SetNextLine (true,3);
		
		Phase03 = true;
	}
	
	void Update(){
		
		// イベントが終わっていなければ、かつメッセージを表示させて良いなら
		if (!isEventEnd) {
			if (EventMessageControll.currentLine-1 < scenarios.Length && (Input.GetMouseButtonDown (0) || Input.GetButtonDown ("Submit"))) {

				// フェーズ１
				if( Phase01 ){
					// テキストウィンドウの大きさを小さくする
					iTween.ScaleTo (textPanel, iTween.Hash ("scale", new Vector3 (1.0f, 0.0f, 1.0f), "time", 0.3f));

					Phase01 = false;
					StartCoroutine ("Event01_Phase02");
				}

				// フェーズ２
				if( Phase02 ){

					Debug.Log("EventMessageControll.currentLine: " + EventMessageControll.currentLine);

					// ２行目が表示されたら
					if(EventMessageControll.currentLine-1 == 3){
						// テキストウィンドウの大きさを小さくする
						iTween.ScaleTo (textPanel, iTween.Hash ("scale", new Vector3 (1.0f, 0.0f, 1.0f), "time", 0.3f));
						Phase02 = false;
						StartCoroutine ("Event01_Phase03");
					}
				}

			} else if (EventSceneControll.isTextEnd) {
				
				// 会話が終了したら行数番号を0に戻す
				EventMessageControll.currentLine = 0;
				// イベント終了
				isEventEnd = true;
				// 画面暗転アニメーション
				iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", 1.0f, "time", 3.0f, "delay", 0.5f, "onUpdate", "UpdateFadeInAnimation", "onComplete", "CompleteFadeOutNextScene"));
				
			}
		}
	}

	void UpdateFadeInAnimation(float ImageAlpha){
		// 徐々に画面明るくする
		BlackOutImage.color = new Color(0.0f, 0.0f, 0.0f, ImageAlpha);
	}
	
	void CompleteFadeOutNextScene(){
		// 幻影の神殿ステージに飛ばす
		Application.LoadLevel ("PhantomTemple");
	}
}
