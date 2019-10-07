using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
	public bool seted ;
	public GameObject SoundPlayer ;
	float startTime ;
	float destroyTime ;
	int damage ;
	string ename ;
	Animator playerAnimator ;
	GameObject[] Search ;
	Sound SP ;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		playerAnimator = (Animator)transform.GetComponent ("Animator");
		Search = GameObject.FindGameObjectsWithTag ("Enemy");
		SoundPlayer = GameObject.FindGameObjectWithTag ("SoundPlayer");
		SP = SoundPlayer.GetComponent<Sound>();
	}
	
	// Update is called once per frame
	void Update () {
		if (seted) {		
			playerAnimator.Play( ename ) ;
			if ( damage > 0 ) {
				if (Search.Length > 0) 
					for( int i = 0 ; i < Search.Length ; i++ ) {
						Vector3 newDir = Search[i].transform.position - transform.position ;
						if ( newDir.magnitude < 1f ) 
							Search[i].transform.GetComponent<Monster>().LostHeath( damage ) ;						
					}

				damage = 0 ;
			}

		}

	}


	public void Set( string name, float time ) {
		destroyTime = time;
		this.ename = name;
		this.damage = 0 ;
		seted = true ;
		transform.GetComponent<CircleCollider2D> ().isTrigger = false;
	}

	public void Set( string name, float time, int damage ) {
		destroyTime = time;
		this.ename = name;
		this.damage = damage;
		seted = true ;
		transform.GetComponent<CircleCollider2D> ().isTrigger = true ;
	}
	public void Destroy() {
		Destroy(gameObject);
	}
	public void SEplay() {
		SP.playsound (1,1f);
	}
}
