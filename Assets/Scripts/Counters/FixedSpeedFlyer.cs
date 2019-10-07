using UnityEngine;
using System.Collections;

public class FixedSpeedFlyer : MonoBehaviour {
	public bool seted = false ;
	Vector3 dir ;
	float speed;
	float destroyTime ;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (seted) {
			transform.Translate( dir * speed * Time.deltaTime );
			transform.Rotate(Vector3.up, speed * Time.deltaTime);
			
			if ( destroyTime<= 0 )
				Destroy ( gameObject );
			else 
				destroyTime-= Time.deltaTime ;
		}

	}
	public void set( string sprite, Vector3 org, Vector3 targ , float speed ,  float destroyTime ) {
		if ( !sprite.Equals( "" ) )
			GetComponent<SpriteRenderer> ().sprite = Resources.Load( sprite, typeof(Sprite)) as Sprite ;
		this.destroyTime = destroyTime;
		this.dir = fix2D ( targ-org );
		this.speed = speed;
		seted = true;
	}

	Vector3 fix2D( Vector3 input ) {
		float org = Mathf.Sqrt( Mathf.Pow( input.x, 2 ) + Mathf.Pow ( input.y, 2 ) );
		return new Vector3( input.x / org , input.y / org, input.z / org ) ;
	}

	void OnTriggerStay2D(Collider2D Coll) {

		if (Coll.gameObject.CompareTag ("Player")) {
			Ally player = Coll.gameObject.GetComponent<Ally>() ;
			if ( player.invTime <= 0 ) {
				player.move = false ;
				player.Dev( transform.position, 0.5f ) ;
				player.LostHeath( 100 ) ;
			}
		}
	}

}
