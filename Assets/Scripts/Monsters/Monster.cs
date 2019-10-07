using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monster : MonoBehaviour {
	public GameObject timebar ; 
	public GameObject monster ;
	public GameObject hpbar ;
	public Texture2D blood_red,blood_yellow;
	public Text MonsterHp ;
	public TextAsset MonsterList ;					//怪物資料(Resource)
	public TeamHp teamHp ;
	public bool attacking ;
	public float attackbar ;
	public Vector3 monsterPostion ;

	public int attack ;
	public int orgHP ;
	public int nowHP ;
	public float size ;
	public float MonHeight;
	public int score ;
	//private string[] MonData ;
	// HP Bar
	public float bloodValue = 0.0f;
	public Text debug ;
	public float tmpValue;
	private float alpha = 255f;
	public Rect rctBloodBar;
	public Vector3 barPosition ; 
	public Controller controller ;

	// Use this for initialization
	public void Start () {
		monster = transform.gameObject ;
		timebar = GameObject.Find("timebar");
		hpbar = GameObject.Find("Canvas/HP bar");
		teamHp = hpbar.transform.GetComponent<TeamHp>() ;
		attacking = false;
		attackbar = 0 ;
		monsterPostion = monster.transform.position;
		controller = GameObject.Find ( "Controller" ).transform.GetComponent<Controller>() ;

		//load red and yellow
		blood_red = Resources.Load("HP red", typeof(Texture2D)) as Texture2D;
		blood_yellow = Resources.Load("HP back", typeof(Texture2D)) as Texture2D;
		float size_y = transform.position.y;
		//得到模型縮放比例
		float scal_y = transform.localScale.y;
		//它們的乘積就是高度
		MonHeight = (size_y *scal_y)*0.5f ;

	}
	
	// Update is called once per frame
	public void Update () {

		if ( nowHP <= 0) {
			transform.GetComponent<CircleCollider2D>().isTrigger = true ;
			Renderer render = gameObject.GetComponentInChildren<Renderer>();
			alpha = alpha - 10.0f;
			Color c  = render.material.color ;
			c.a = alpha / 255.0f;
			render.material.color = c ;
			if(render.material.color.a <= 0) {
				Controller.Score+= this.score ;
				Destroy(gameObject);
			}
		}
	}

	public void LostHeath( int damage ) {
		nowHP -= damage;
		if (nowHP <= 0)
			nowHP = 0;
		GameObject mObject=(GameObject)Instantiate( Resources.Load( "PopupDamage") ,transform.position,Quaternion.identity);
		mObject.GetComponent<DamagePopup>().Value= damage;
	}


	public void OnGUI()
	{

		//bloodValue = (float)nowHP / (float)orgHP;	
		/*
		barPosition = transform.position;
		barPosition += new Vector3 (0, size, 0);
		debug.text = ""+ size;
		Vector3 barPositionInScreen  = Camera.main.WorldToScreenPoint( barPosition ) ;
		rctBloodBar = new Rect ( barPositionInScreen.x-50 , Screen.height - barPositionInScreen.y, 100, 10 );		

		GUI.HorizontalScrollbar(rctBloodBar, 1f, bloodValue, 1f, 0f, GUI.skin.GetStyle( "horizontalScrollbar")); 
		*/
		//得到NPC頭頂在3D世界中的坐標
		//默認NPC坐標點在腳底下，所以這裡加上npcHeight它模型的高度即可
		Vector3 worldPosition = new Vector3 (transform.position.x , transform.position.y + MonHeight,0);
		//根據NPC頭頂的3D坐標換算成它在2D屏幕中的坐標
		Vector2 position = Camera.main.WorldToScreenPoint (worldPosition);
		//得到真實NPC頭頂的2D坐標
		position = new Vector2 (position.x, Screen.height - position.y);
		//註解2
		//計算出血條的寬高
		Vector2 bloodSize = GUI.skin.label.CalcSize (new GUIContent(blood_red))*0.1f;

		//通過血值計算紅色血條顯示區域
		int blood_width = 50 * nowHP/orgHP;
		//先繪製黃色血條
		GUI.DrawTexture(new Rect(position.x - (bloodSize.x*2),position.y - bloodSize.y ,50,bloodSize.y),blood_yellow);
		//在繪製紅色血條
		GUI.DrawTexture(new Rect(position.x - (bloodSize.x*2),position.y - bloodSize.y ,blood_width,bloodSize.y),blood_red);

		//註解3
		//計算NPC名稱的寬高
		Vector2 nameSize = GUI.skin.label.CalcSize (new GUIContent(name));
		//設置顯示顏色為黃色
		GUI.color  = Color.yellow;
		//繪製NPC名稱
		GUI.Label(new Rect(position.x - (nameSize.x/2),position.y - nameSize.y - bloodSize.y ,nameSize.x,nameSize.y), name);
	}
}
