using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerShadow : MonoBehaviour {
	static public GameObject targetLock ;				// 鎖定攻擊目標
	public Vector3 targetPos, devPos ;					// 目標位置
	public bool move = false ;					// 是否指定移動
	public float aSpeed ;						// 角色移動速度
	public float devTime, invTime ;				// 後彈時間,無敵時間
	public GameObject SoundPlayer ;				
	
	
	private GameObject[] Search ;				// 怪物列表
	private Sound SP ;
	Animator playerAnimator ;					// 動畫
	void Start () {
		SoundPlayer = GameObject.Find ("SoundPlayer");
		playerAnimator = (Animator)transform.GetComponent ("Animator");
		targetPos = transform.position ;
		move = false;
		aSpeed = 0.05f;							// ally's speed setting
		SP = SoundPlayer.GetComponent<Sound>();
		Renderer render = gameObject.GetComponentInChildren<Renderer>();
		Color c  = render.material.color ;
		c.a = 0.5f;
		render.material.color = c ;
		invTime = 3f;
	}
	
	void Update () {
				
		if (move) {
			if (transform.position == targetPos) 
				move = false;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, aSpeed); 
			
		} else if (devTime > 0) {//往後彈的動作
			transform.position = Vector3.MoveTowards (transform.position, 2 * transform.position - devPos, 5 * aSpeed);
			devTime -= 0.1f ;
		}
		else {
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
		
		if (invTime > 0) {
			invTime -= Time.deltaTime;
			invTime += (float)BattleSys.getAttention () / 100;
		}
		else 
			Destroy (gameObject);
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
	}
	
	void OnCollisionStay2D(Collision2D Coll)
	{
		if (Coll.gameObject.CompareTag ("Enemy")) {
			Debug.Log( "Attack " + Coll.gameObject.name ) ;
			((Monster)Coll.gameObject.GetComponent<Monster>()).LostHeath( 100 ) ;
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

}	
