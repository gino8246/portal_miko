using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		name = "TrainingInput";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	
	public void Destroy() {
		//Destroy (GameObject.Find ("Canvas/TrainingInput"));
		this.gameObject.SetActive (false);
	}
	public void ChangeBattle () {
		Application.LoadLevelAsync ("DemoBattle");
	}
	public void ModeSet( int mode ) {
		PlayerPrefs.SetInt ("mode", mode);
	}
	public void TrainTimeSet() {
		int trainTime = 0;
		int input;

		input = int.Parse( GameObject.Find ("Canvas/TrainingInput/h/Text").transform.GetComponent<Text> ().text );
		if (input > 0) 
			trainTime += input * 60 * 60;
		input = int.Parse( GameObject.Find ("Canvas/TrainingInput/m/Text").transform.GetComponent<Text> ().text );
		if (input > 0) 
			trainTime += input * 60;
		input = int.Parse( GameObject.Find ("Canvas/TrainingInput/s/Text").transform.GetComponent<Text> ().text );
		if (input > 0) 
			trainTime += input;
		PlayerPrefs.SetInt ("trainTime", trainTime);
	}
}
