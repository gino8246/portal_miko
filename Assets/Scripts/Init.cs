using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Init : MonoBehaviour {
	public static PlayerPrefs allyList ;
	public GameObject trainingB;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("ally1", 1);
	}
	
	// Update is called once per frame
	public void connect() {		 	
		UnityThinkGear.StartStream();
	}

	void Update () {
	
	}
	public void TrainInputBar() {
		/*GameObject trainInput = Instantiate(Resources.Load ("TrainingInput"), GameObject.Find ("Canvas").transform.position, Quaternion.identity) as GameObject;
		trainInput.transform.parent = GameObject.Find ("Canvas").transform ;*/
		trainingB.SetActive (true);
		
	}
	public void ModeSet( int mode ) {
		PlayerPrefs.SetInt ("mode", mode);
	}
}
