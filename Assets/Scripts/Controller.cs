using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	public int Att ;							// 專注數值
	private int PoorSignal ;							// 冷靜數值
	public Image SignalImage ;
	public Image[] SkillImage ;
	private float AttValue, tmpValue ;
	public Text AttText, MedText ;						// 顯示腦波數值
	static public int Score ;
	public float PlayTime ;
	static public float AvrAtt ;
	private float TotalAtt ;
	private float StartTime ;
	private float click ;
	private Text[] GameOverText;
	public GameObject gameoverbar ;
	private GameObject monster ;
	private float rebroneTime;
	public Scrollbar AttBar ;
	static public bool gameover ;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1 ;
		Score = 0;
		PlayTime = 0f;
		AvrAtt = 0f;
		TotalAtt = 0f;
		StartTime = 0f;
		gameover = false;
		rebroneTime = 3;
		monster = null;
		click = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("mode") == 0) {
			if (rebroneTime <= 0) {
				Instantiate (Resources.Load ("Monster/golem"), new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-2f, 2.5f), 0), Quaternion.identity);
				rebroneTime = 6;
			} else
				rebroneTime -= Time.deltaTime;
		} else if (PlayerPrefs.GetInt ("mode") == 1) {
			if (monster == null ) 
				monster = Instantiate (Resources.Load ("Monster/golem"), new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-2f, 2.5f), 0), Quaternion.identity) as GameObject;

		}
		Att = BattleSys.getAttention();
		PoorSignal = BattleSys.getPoorSignal();

		StartTime += Time.deltaTime;

		click -= Time.deltaTime;
		if (click <= 0) {
			TotalAtt += Att;
			click = 1 ;
		}
		AvrAtt = TotalAtt / StartTime;
		GameObject.Find( "Canvas/Score" ).GetComponent<Text>().text = "Score:" +Score;

		//signal
		if(PoorSignal == 0){
			SignalImage.color= Color.blue ;
		}else if(PoorSignal == 200){
			SignalImage.color= Color.black ;
		}else {
			SignalImage.color= Color.green ;
		}
		MedText.text = "" + PoorSignal;

		// att
		Color skillcolor;
		if (Att < 30) 
			skillcolor = Color.white;
		else if (Att < 80) 
			skillcolor = Color.blue;
		else 
			skillcolor = Color.red;
		for (int i = 0; i < 4; i++) 
			SkillImage [i].color = skillcolor;

		AttText.text = "" + Att;
		tmpValue = (float)Att/100f;
		AttValue = Mathf.Lerp(AttValue, tmpValue, 0.05f);
		AttBar.size = AttValue;

		if ( gameover)  {

		}else if ( GameObject.Find ("Canvas/HP bar").transform.GetComponent<TeamHp> ().nowHP <= 0 ) {
			/*gameoverbar = Instantiate(Resources.Load ("GameOver"), GameObject.Find ("Canvas").transform.position, Quaternion.identity) as GameObject;
			gameoverbar.transform.parent = GameObject.Find ("Canvas").transform ;*/
			gameoverbar.SetActive(true) ;
			GameOverText = gameoverbar.transform.GetComponentsInChildren<Text>()   ;
			GameOverText[0].text = "GameOver" ;
			GameOverText[1].text = "Score:" + Score + "\n";
			GameOverText[2].text = "Average Attention:"  + AvrAtt ;
			Time.timeScale = 0 ;
			gameover = true ;
		}
	}


	public void Pause() {
		if (Controller.gameover) 
			return;
		BattleSys.pause = !BattleSys.pause;
		Debug.Log ("PAUSE");
		if (BattleSys.pause) {
			gameoverbar.SetActive( true ) ;
			Text[] GameOverText = gameoverbar.transform.GetComponentsInChildren<Text>()   ;
			GameOverText[0].text = "PAUSE" ;
			GameOverText[1].text = "Score:" + Controller.Score ;
			GameOverText[2].text = "Average Attention:"  + Controller.AvrAtt ;
			Time.timeScale = 0;
		} else {
			gameoverbar.SetActive( false ) ;
			Time.timeScale = 1;
		}
	}





}
