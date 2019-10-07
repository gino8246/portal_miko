using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public AudioClip[] sound ;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	public void playsound( int num, float scale ) {
		((AudioSource)GetComponent<AudioSource>()).PlayOneShot( sound[num],scale ) ;
	}
}
