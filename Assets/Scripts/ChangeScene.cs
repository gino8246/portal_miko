using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	public void ChangeBattle () {
		Application.LoadLevelAsync ("DemoBattle");
	}
	public void ChangeMenu() {
		Application.LoadLevelAsync ("Menu");
	}

}
