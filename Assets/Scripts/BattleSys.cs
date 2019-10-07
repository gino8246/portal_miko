using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class BattleSys : MonoBehaviour {
	// Use this for initialization
	//public GameObject ally ;
	//public GameObject monster ;
	//static public GameObject[] allylist ;
	public Text Cmodtext ;
	public Text Pausetext ;
	static public bool PCdebug = true;
	static public bool pause = false ;
	// MindWave
	ThinkGearController controller;
	
	public Texture2D[] signalIcons;
	
	private float indexSignalIcons = 1;
	private bool enableAnimation = false;
	private float animationInterval = 0.06f;
	
	private int Raw = 0;
	static private int PoorSignal = 200;
	static private int Attention = 0;
	static private int Meditation = 0;
	private int Blink = 0;
	private float Delta = 0.0f;
	private float Theta = 0.0f;
	private float LowAlpha = 0.0f;
	private float HighAlpha = 0.0f;
	private float LowBeta = 0.0f;
	private float HighBeta = 0.0f;
	private float LowGamma = 0.0f;
	private float HighGamma = 0.0f;
	//MindWave

	//Sprite Monster ;
	void Start () {
		//GameObject save = (GameObject)Instantiate (Resources.Load ("Character/ally1"));
		//allylist.SetValue( save, 0 );

		//Monster = Resources.Load("Monster/golem13", typeof(Sprite)) as Sprite;
		//monster.GetComponent<SpriteRenderer> ().sprite = Monster;
		/*Ally1 = Resources.LoadAll("Character/org_h01", typeof(Sprite)) as Sprite[];
		//ally = GameObject.Find ("Ally1");
		SpriteRenderer playerSpriteRenderer = (SpriteRenderer)ally.GetComponent ("SpriteRenderer");
		playerSpriteRenderer.sprite = Ally1[1] ;*/

		//MindWave		
		controller = GameObject.Find("ThinkGear").GetComponent<ThinkGearController>();
		
		controller.UpdateRawdataEvent += OnUpdateRaw;
		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
		controller.UpdateAttentionEvent += OnUpdateAttention;
		controller.UpdateMeditationEvent += OnUpdateMeditation;
		
		controller.UpdateDeltaEvent += OnUpdateDelta;
		controller.UpdateThetaEvent += OnUpdateTheta;
		
		controller.UpdateHighAlphaEvent += OnUpdateHighAlpha;
		controller.UpdateHighBetaEvent += OnUpdateHighBeta;
		controller.UpdateHighGammaEvent += OnUpdateHighGamma;
		
		controller.UpdateLowAlphaEvent += OnUpdateLowAlpha;
		controller.UpdateLowBetaEvent += OnUpdateLowBeta;
		controller.UpdateLowGammaEvent += OnUpdateLowGamma;
		
		controller.UpdateBlinkEvent += OnUpdateBlink;
	}
	
	// Update is called once per frame


	//MindWave
	void OnUpdatePoorSignal(int value){
		PoorSignal = value;
		if(value == 0){
			indexSignalIcons = 0;
			enableAnimation = false;
		}else if(value == 200){
			indexSignalIcons = 1;
			enableAnimation = false;
		}else if(!enableAnimation){
			indexSignalIcons = 2;
			enableAnimation = true;
		}
	}
	void OnUpdateRaw(int value){
		Raw = value;
	}
	void OnUpdateAttention(int value){
		Attention = value;
	}
	void OnUpdateMeditation(int value){
		Meditation = value;
	}
	void OnUpdateDelta(float value){
		Delta = value;
	}
	void OnUpdateTheta(float value){
		Theta = value;
	}
	void OnUpdateHighAlpha(float value){
		HighAlpha = value;
	}
	void OnUpdateHighBeta(float value){
		HighBeta = value;
	}
	void OnUpdateHighGamma(float value){
		HighGamma = value;
	}
	void OnUpdateLowAlpha(float value){
		LowAlpha = value;
	}
	void OnUpdateLowBeta(float value){
		LowBeta = value;
	}
	void OnUpdateLowGamma(float value){
		LowGamma = value;
	}
	
	void OnUpdateBlink(int value){
		Blink = value;
	}

	static public int getAttention() {
		return Attention ;
	}

	static public int getMeditantion() {
		return Meditation ;
	}
	static public int getPoorSignal() {
		return PoorSignal ;
	}
	//MindWave



	public void shadow() {
		Instantiate( Resources.Load("Monster/golem"), new Vector3( Random.Range(-2.7f,2.7f), Random.Range(2f,2.5f),0 ), Quaternion.identity  ) ;
	}

	public void Cmod() {
		PCdebug = !PCdebug;
		if (PCdebug)
			Cmodtext.text = "PC Mod";
		else
			Cmodtext.text = "And Mod";
	}

	public void Pause() {
		if (Controller.gameover) 
			return;
		pause = !pause;
		Debug.Log ("PAUSE");
		if (pause) {
			GameObject gameoverbar = GameObject.Find( "Canvas/GameOver" ) ;
			gameoverbar.SetActive( true ) ;
			Text[] GameOverText = gameoverbar.transform.GetComponentsInChildren<Text>()   ;
			GameOverText[0].text = "PAUSE" ;
			GameOverText[1].text = "Score:" + Controller.Score ;
			GameOverText[2].text = "Average Attention:"  + Controller.AvrAtt ;
			Time.timeScale = 0;
		} else {
			GameObject gameoverbar = GameObject.Find( "Canvas/GameOver" ) ;
			gameoverbar.SetActive( false ) ;
			Time.timeScale = 1;
		}
	}
}
