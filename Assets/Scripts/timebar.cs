using UnityEngine;
using System.Collections;

public class timebar : MonoBehaviour {
	public float CountDown ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( gameObject.transform.position.x <= 2.7f )
			gameObject.transform.position += new Vector3 (0.02f, 0);
		else 
			gameObject.transform.position = new Vector3 (-2.7f, 0.1f) ;


		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0 && CountDown <= 0 )
			CountDown = 5;

		/*if (CountDown >= 0) {
			CountDown -= Time.deltaTime;
			if ( CountDown <= 0 ) {
				Instantiate( Resources.Load("Monster/golem"), new Vector3( Random.Range(-2.7f,2.7f), Random.Range(2f,2.5f),0 ), Quaternion.identity  ) ;
				Debug.Log( "New" ) ;
			}
		}*/
	}
}
