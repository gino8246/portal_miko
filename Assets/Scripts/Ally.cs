using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Ally : MonoBehaviour {
	static public GameObject targetLock ;				// 鎖定攻擊目標
	public Vector3 targetPos, devPos ;					// 目標位置
	public bool move = false ;					// 是否指定移動
	public float aSpeed ;						// 角色移動速度
	public float devTime, invTime ;				// 後彈時間,無敵時間
	public GameObject SoundPlayer ;				


	private GameObject[] Search ;				// 怪物列表
	private Sound SP ;
	public float[] CD ;
	public float[] oCD ;
	Animator playerAnimator ;					// 動畫
	void Start () {
		playerAnimator = (Animator)transform.GetComponent ("Animator");
		targetPos = transform.position ;
		move = false;
		aSpeed = 0.05f;							// ally's speed setting
		CD = new float[4]{0,0,0,0} ;
		oCD = new float[4]{3,7,10,15};
		SP = SoundPlayer.GetComponent<Sound>();
	}

	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)); //這邊假設你Camera的Z軸在-10而物體的Z軸在0
		if (PlayerPrefs.GetInt ("mode") == 0){
			if (Input.GetMouseButtonDown (0)) {
				if (!BattleSys.PCdebug && IsPointerOverGameObject (Input.GetTouch (0).fingerId)) 		//電腦上測試需先去掉
					Debug.Log ("Hit UI, Ignore Touch");
				else
					TargetSet (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f)));
			}
		} /*else if (PlayerPrefs.GetInt ("mode") == 1) {
			if ( !move )  
				TargetSet (new Vector3( Random.Range( 2,-2), Random.Range(4,-4),10f ));

		}*/

		if (Time.timeScale == 0)
			;
		else if (move) {
			if (transform.position == targetPos) 
				move = false;

			transform.position = Vector3.MoveTowards (transform.position, targetPos, aSpeed); 

		} 
		else if (devTime > 0) {//往後彈的動作
			transform.position = Vector3.MoveTowards (transform.position, 2 * transform.position - devPos, 5 * aSpeed);
			devTime -= 0.1f ;
		}
		else if ( PlayerPrefs.GetInt( "mode" ) == 1){
			if ( EnemySearch() ) {

				if ( devTime > 0 ) //往後彈的動作
					transform.position = Vector3.MoveTowards (transform.position, 2*transform.position - targetPos, 5*aSpeed) ;
				else 
					transform.position = Vector3.MoveTowards (transform.position, targetPos, aSpeed);
				devTime -= 0.1f ;
			} // 索敵成功
			else 
				targetPos = transform.position ; // 索敵失敗
		}

		if (invTime>0) {
			invTime -= 0.1f;
			Renderer render = gameObject.GetComponentInChildren<Renderer>();
			Color c  = render.material.color ;
			if ( c.a == 1f && invTime>0 )
				c.a = 0f ;
			else
				c.a = 1f ;
			render.material.color = c ;

		} 

		CDSet ();
		AnimPlay ();

	}


	void TargetSet( Vector3 target ) {
		if (devTime > 0)
			return;
		targetPos = target ;
		move = true;
	}

	bool IsPointerOverGameObject( int fingerId )
	{
		EventSystem eventSystem = EventSystem.current;
		return ( eventSystem.IsPointerOverGameObject( fingerId )
		        && eventSystem.currentSelectedGameObject != null );
	}



	void AnimPlay() {
		Vector2 v = (targetPos - transform.position).normalized ;
		playerAnimator.SetFloat ("X", v.x);
		playerAnimator.SetFloat ("Y", v.y);
		/*
		if ( targetPos == transform.position ) 
			playerAnimator.Play ("downAnim");
		else if (targetPos.x > transform.position.x && targetPos.y > transform.position.y) {
			if (targetPos.x - transform.position.x > targetPos.y - transform.position.y)
				playerAnimator.Play ("rightAnim");
			else
				playerAnimator.Play ("upAnim");
		} else if (targetPos.x > transform.position.x && targetPos.y < transform.position.y) {
			if (targetPos.x - transform.position.x > transform.position.y - targetPos.y)
				playerAnimator.Play ("rightAnim");
			else
				playerAnimator.Play ("downAnim");
		} else if (targetPos.x < transform.position.x && targetPos.y > transform.position.y) {
			if (transform.position.x - targetPos.x > targetPos.y - transform.position.y)
				playerAnimator.Play ("leftAnim");
			else
				playerAnimator.Play ("upAnim");
		} else if (targetPos.x < transform.position.x && targetPos.y < transform.position.y) {
			if (transform.position.x - targetPos.x > transform.position.y - targetPos.y)
				playerAnimator.Play ("leftAnim");
			else
				playerAnimator.Play ("downAnim");
		}
		*/
	}

	void OnCollisionStay2D(Collision2D Coll)
	{
		if (Coll.gameObject.CompareTag ("Enemy")) {
			Debug.Log( "Attack " + Coll.gameObject.name ) ;
			((Monster)Coll.gameObject.GetComponent<Monster>()).LostHeath( (int)BattleSys.getAttention() ) ;
			//transform.position = Vector3.MoveTowards (transform.position, (transform.position - targetPos)*500, 5*aSpeed) ;
			Dev ( Coll.gameObject.transform.position , 0.5f ) ;	//這一行已經設定了往後彈的動作,所以不需要上一行
			SP.playsound(0,0.5f) ;
			if ( ((Monster)Coll.gameObject.GetComponent<Monster>()).nowHP <= 0 )
				devTime = 0;
		}	

		move = false;			//接觸到敵人後直接開始攻擊動作,結束移動
	}

	bool EnemySearch() {
		float SearchRange = 10f ;
		Vector3 minDir ;
		Vector3 minVec ;
		Search = GameObject.FindGameObjectsWithTag ("Enemy");
		if (Search.Length == 0)
			return false;
		else {
			minVec = Search[0].transform.position ;
			minDir = Search[0].transform.position - transform.position ;
			for( int i = 0 ; i < Search.Length ; i++ ) {
				Vector3 newDir = Search[i].transform.position - transform.position ;
				if ( newDir.magnitude < minDir.magnitude ) {
					minDir = newDir ;	
					minVec = Search[i].transform.position ;
				}
			}
			if ( minDir.magnitude > SearchRange )
				return false ;

			targetPos = minVec ;
			return true ;
		}
	}

	public void Dev( Vector3 devPos, float devTime ) {
		this.devPos = devPos;
		this.devTime = devTime;
	}

	public void Invincible ( float time ) {
		/*if ( time > 0 ) 
			GetComponent<CircleCollider2D> ().isTrigger = true;*/
		invTime = time;
	}

	public void LostHeath( int damage ) {
		TeamHp teamHp = GameObject.Find ("Canvas/HP bar").transform.GetComponent<TeamHp> ();
		teamHp.LostHP (damage);
		Invincible ( 10 ) ;

	}

	/*private void CDSet() {
		for (int i = 0; i < 4; i++) {
			if ( Time.time - CD[i] < oCD[i] ) {
				Text cd = GameObject.Find( "Canvas/Skill"+(i+1)+"/Text").GetComponent<Text>() ;
				cd.text = ""+(int)(oCD[i] - ( Time.time - CD[i] )) ;
			}
			else 
				GameObject.Find( "Canvas/Skill"+(i+1)+"/Text").GetComponent<Text>().text = "" ;
		}
	}*/
	private void CDSet() {
		if (PlayerPrefs.GetInt ("mode") == 1) {
			if ( CD[0] <= 0 ) 
				skill_1() ;
			else if ( CD[1] <= 0 ) 
				skill_2() ;
			else if ( CD[2] <= 0 ) 
				skill_3() ;
			else if ( CD[3] <= 0 ) 
				skill_4() ;
		}


		for (int i = 0; i < 4; i++) {
			if ( CD[i] > 0 ) {
				CD[i] -= Time.deltaTime ;
				Text cd = GameObject.Find( "Canvas/Skillbar/Skill"+(i+1)+"/Text").GetComponent<Text>() ;
				cd.text = ""+(int)CD[i] ;
			}
			else 
				GameObject.Find( "Canvas/Skillbar/Skill"+(i+1)+"/Text").GetComponent<Text>().text = "" ;
		}
	}

	public void skill_1() {
		Debug.Log ("Skill 1");
		Ally player = GameObject.Find ("Ally1").GetComponent<Ally>();
		if ( player.CD [0] > 0)
			return;

		if (targetLock != null) {
			GameObject skill1 = Instantiate (Resources.Load ("Effect"), targetLock.transform.position, Quaternion.identity) as GameObject;
			Effect skillEffect = skill1.GetComponent<Effect>() ;
			skill1.transform.localScale+= new Vector3( ((float)BattleSys.getAttention () / 100)+1,((float)BattleSys.getAttention () / 100)+1,1) ;
			skillEffect.Set( "Skill1", 1f, Random.Range( 100 , 150 ) + 3*(int)BattleSys.getAttention() ) ;
			player.CD [0] = player.oCD [0];
		}
		else if (player.EnemySearch ()) {
			GameObject skill1 = Instantiate (Resources.Load ("Effect"), player.targetPos, Quaternion.identity) as GameObject;
			Effect skillEffect = skill1.GetComponent<Effect>() ;
			skillEffect.Set( "Skill1", 1f , Random.Range( 100 , 150 ) + 3*(int)BattleSys.getAttention() ) ;
			player.CD [0] = player.oCD [0];
		}


		player.move = false ;
	}
	public void skill_2() {
		Debug.Log ("Skill 2");
		Ally player = GameObject.Find ("Ally1").GetComponent<Ally>();
		if ( player.CD [1] > 0)
			return;	
		GameObject skill2 = Instantiate (Resources.Load ("Character/Ally_shadow"), player.transform.position, Quaternion.identity) as GameObject;
		player.CD [1] = player.oCD [1];

		SP.playsound(2,1f) ;
		player.move = false ;

	}
	public void skill_3() {
		Debug.Log ("Skill 3");
		Ally player = GameObject.Find ("Ally1").GetComponent<Ally>();
		if ( player.CD [2] > 0)
			return;

		GameObject skill3 = Instantiate (Resources.Load ("Skill/Skill_3"), player.transform.position, Quaternion.identity) as GameObject;
		skill3.transform.parent = this.transform;
		Skill3 skillEffect = skill3.GetComponent<Skill3>() ;
		skillEffect.Set( "Skill3" ) ;
		player.CD [2] = player.oCD [2];

		player.move = false ;
	}
	public void skill_4() {
		Debug.Log ("Skill 4");
		Ally player = GameObject.Find ("Ally1").GetComponent<Ally>();
		GameObject[] Target = new GameObject[5] ;
		Vector3 temp;
		Search = GameObject.FindGameObjectsWithTag ("Enemy");
		if ( player.CD [3] > 0 || Search.Length == 0 )
			return;

		for( int i = 0 ; i < 5; i++ )
			Target[i] = Search[Random.Range( 0 , Search.Length )] ;


		for( int i = 0 ; i < 5 ; i++ ) {
				temp = Target[i].transform.position ;
				temp.y += 0.9f ;
				GameObject skill4 = Instantiate (Resources.Load ("Skill/Skill_4"),temp , Quaternion.identity) as GameObject;
				Skill4 skillEffect = skill4.GetComponent<Skill4>() ;
				skillEffect.Set( "Skill4" ) ;
		}

		player.CD [3] = player.oCD [3];
		
		player.move = false ;
	}
}	
