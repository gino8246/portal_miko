using UnityEngine;
using System.Collections;

public class Skill3 : MonoBehaviour {
	float delayTime ;
	string ename ;
	Sound SP ;
	public Animator playerAnimator ;
	public GameObject SoundPlayer ;

	// Use this for initialization
	void Start () {
		SoundPlayer = GameObject.FindGameObjectWithTag ("SoundPlayer");
		SP = SoundPlayer.GetComponent<Sound>();
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Set( string name ) {
		this.ename = name;
		playerAnimator.Play( ename ) ;
	}
	public void Destroy() {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D Coll)
	{
		if (Coll.gameObject.CompareTag ("Enemy")) 
			((Monster)Coll.gameObject.GetComponent<Monster> ()).LostHeath (Random.Range (10, 15) + (int)BattleSys.getAttention ());
	
	}

	public void triggerOn() {
		transform.GetComponent<CircleCollider2D> ().enabled = true;
	}
	public void triggerOff() {
		transform.GetComponent<CircleCollider2D> ().enabled = false;
	}

	public void SEplay() {
		SP.playsound (3,1f);
	}

}
