using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusUIControll : MonoBehaviour {

	//--- オブジェクト ---//
	private GameObject PlayerHP;
	private GameObject PlayerDamageHP;
	private GameObject WorldImage;
	private GameObject EnemyPanel;
	private GameObject EnemyHP;
	private GameObject EnemyDamageHP;

	//--- フラグ管理 ---//
	public  bool isEnemy        = false;	// 敵(BOSS)出現フラグ public static
	private bool isPlayerHPAnime  = false;	// Playerダメージアニメーションフラグ
	private bool isEnemyHPAnime   = false;	// Enemyダメージアニメーションフラグ
	private bool isPlayerDanger = false;	// Player危険フラグ
	private bool isEnemyDanger  = false;	// Enemy危険フラグ

	//--- HP関連 ---//
	public static int playerHP       = 100;	// プレイヤーのHP
	public static int playerDamageHP = 100;	// プレイヤーのダメージ中のHP
	public int enemyHP        = 100;	// public static
	public int enemyDamageHP  = 100;	// public static
	
	private const float HPBarMagnification  = 2.25f;	// HP...225px = playerHP * HPBarMagnification;
	private const int   HPBarHeight = 45;				// HPバーの高さ

	private int MinHP    = 0;	// 最小HP
	private int MaxHP    = 100;	// 最大HP
	private int DangerHP = 30;	// 危険時のHP
	public  float  FlashinTime = 0.03f;	// HP危険時のHPバー明滅速度

	//--- 残機関連 ---//
	private Text RemainingTextNumber;
	public  static int  RemainingNumber;

	void Start () {
		//--- 各ゲームオブジェクトを取得 ---//
		PlayerHP       = GameObject.Find ("PlayerHPImage");
		PlayerDamageHP = GameObject.Find ("PlayerDamageHPImage");
		WorldImage     = GameObject.Find ("WorldImage");
		EnemyPanel     = GameObject.Find ("EnemyPanel");
		EnemyHP        = GameObject.Find ("EnemyHPImage");
		EnemyDamageHP  = GameObject.Find ("EnemyDamageHPImage");
		RemainingTextNumber = GameObject.Find ("RemainingTextNumber").GetComponent<Text>();

		//--- ワールドイメージ、BOSS用パネルを非表示 ---//
		WorldImage.SetActiveRecursively (false);
		EnemyPanel.SetActiveRecursively (false);

		// 残機
		RemainingNumber = 2;
	}
	
	// Update is called once per frame
	void Update () {

		//--- HPの上限下限を超えないようにし、ダメージ量に応じてHPバーを反映 ---//
		playerHP = Mathf.Clamp (playerHP, MinHP, MaxHP);
		enemyHP  = Mathf.Clamp (enemyHP , MinHP, MaxHP);
		PlayerHP.GetComponent<RectTransform>().sizeDelta = new Vector2 (playerHP * HPBarMagnification, HPBarHeight);
		EnemyHP.GetComponent<RectTransform>().sizeDelta  = new Vector2 (enemyHP  * HPBarMagnification, HPBarHeight);

		//--- ダメージを受けたらHPバーのダメージアニメーションをスタート ---//
		if (playerDamageHP != playerHP && !isPlayerHPAnime) {
			isPlayerHPAnime = true;
			iTween.ValueTo (gameObject, iTween.Hash ("from", playerDamageHP, "to", playerHP, "time", 0.6f, "delay", 0.15f, "onUpdate", "UpdatePlayerDamageHPBar", "onComplete", "CompletePlayerDamageHPBar", "easetype", iTween.EaseType.easeOutQuad));
		}
		if (enemyDamageHP  != enemyHP  && !isEnemyHPAnime) {
			isEnemyHPAnime = true;
			iTween.ValueTo (gameObject, iTween.Hash ("from", enemyDamageHP , "to", enemyHP , "time", 0.6f, "delay", 0.15f, "onUpdate", "UpdateEnemyDamageHPBar" , "onComplete", "CompleteEnemyDamageHPBar" , "easetype", iTween.EaseType.easeOutQuad));
		}

		//--- 危険時にHPを明滅するアニメーションの開始 ---//
		if (playerHP < DangerHP && !isPlayerDanger) {
			isPlayerDanger = true;
			StartCoroutine("FlashingPlayerHPBar");
		}
		if (enemyHP < DangerHP && !isEnemyDanger) {
			isEnemyDanger = true;
			StartCoroutine("FlashingEnemyHPBar");
		}
	
		//--- 敵(BOSS)出現時はBOSS用パネルを表示、それ以外はワールドイメージを表示 ---//
		if (isEnemy) {
			WorldImage.SetActiveRecursively (false);
			EnemyPanel.SetActiveRecursively (true);
		} else {
			WorldImage.SetActiveRecursively (true);
			EnemyPanel.SetActiveRecursively (false);
		}

		// 残機反映
		if( RemainingNumber >= 99 ){ RemainingNumber = 99; } 
		RemainingTextNumber.text = RemainingNumber.ToString();
	}

	//--- 危険時にHPを明滅するアニメーション ---//
	private IEnumerator FlashingPlayerHPBar(){
		//--- Player ---//
		PlayerHP.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
		yield return new WaitForSeconds (FlashinTime);
		PlayerHP.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		yield return new WaitForSeconds (FlashinTime);

		if (playerHP < DangerHP) {
			StartCoroutine("FlashingPlayerHPBar");
		} else { 
			PlayerHP.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			isPlayerDanger = false;
		}
	}
	private IEnumerator FlashingEnemyHPBar(){
		//--- Enemy ---//
		EnemyHP.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
		yield return new WaitForSeconds (FlashinTime);
		EnemyHP.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		yield return new WaitForSeconds (FlashinTime);
		
		if (enemyHP < DangerHP) {
			StartCoroutine("FlashingEnemyHPBar");
		} else { 
			EnemyHP.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			isEnemyDanger = false;
		}
	}

	//--- ダメージHPバーのアニメーション ---//
	private void UpdatePlayerDamageHPBar(int newDamageHP){
		//--- Player ---//
		playerDamageHP = newDamageHP;
		PlayerDamageHP.GetComponent<RectTransform>().sizeDelta = new Vector2 (playerDamageHP * HPBarMagnification, HPBarHeight);
	}
	private void CompletePlayerDamageHPBar(){
		//--- Enemy ---//
		isPlayerHPAnime = false;
	}

	//--- ダメージHPバーのアニメーション完了後 ---//
	private void UpdateEnemyDamageHPBar (int newDamageHP){
		//--- Player ---//
		enemyDamageHP = newDamageHP;
		EnemyDamageHP.GetComponent<RectTransform>().sizeDelta  = new Vector2 (enemyDamageHP  * HPBarMagnification, HPBarHeight);
	}
	private void CompleteEnemyDamageHPBar(){
		//--- Enemy ---//
		isEnemyHPAnime = false;
	}
}
