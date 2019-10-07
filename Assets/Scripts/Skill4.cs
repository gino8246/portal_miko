using UnityEngine;
using System.Collections;

public class Skill4 : MonoBehaviour {
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
			((Monster)Coll.gameObject.GetComponent<Monster> ()).LostHeath (Random.Range (30, 75) + (int)BattleSys.getAttention ());
		
	}
	
	public void triggerOn() {
		transform.GetComponent<CircleCollider2D> ().enabled = true;
	}
	public void triggerOff() {
		transform.GetComponent<CircleCollider2D> ().enabled = false;
	}

	public void SEplay1() {
		SP.playsound (4,0.3f);
	}
	public void SEplay2() {
		SP.playsound (5,0.5f);
	}
}