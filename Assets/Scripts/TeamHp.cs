using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class TeamHp : MonoBehaviour
{
	public Scrollbar HpBar ;
	public float bloodValue = -1f;
	public float tmpValue = -1f ;
	public Text hptext ;
	public int  orgHP ;
	public float nowHP ;
	//private Rect rctUpButton;
	//private Rect rctDownButton;
	private bool onoff;
	
	// Use this for initialization
	void Start()
	{	
		if ( PlayerPrefs.GetInt( "mode" ) == 0  ) 
			orgHP = 1000;
		else if ( PlayerPrefs.GetInt( "mode" ) == 1 ) 
			orgHP = PlayerPrefs.GetInt("trainTime");
		nowHP = orgHP;
		//rctUpButton = new Rect(50, 20, 40, 20);
		//rctDownButton = new Rect(50, 50, 40, 20);
		tmpValue = bloodValue = -1f ;
		HpBar.size = -bloodValue;
	}
	
	void OnGUI()
	{
		/*
		if (GUI.Button(rctUpButton, "加血"))
		{
			tmpValue-=0.2f;
		}
		if (GUI.Button(rctDownButton, "減血"))
		{
			tmpValue += 0.2f;
		}*/
		if (tmpValue > 0.0f) tmpValue = 0.0f;
		if (tmpValue < -1.0f) tmpValue = -1.0f;
		//重點在於這條語句，實現了緩變的效果。
		bloodValue = Mathf.Lerp(bloodValue, tmpValue, 0.05f);
		//~ Debug.Log (bloodValue + " " + tmpValue);
		HpBar.size = -bloodValue;
		//nowHP = (int) (-tmpValue * (float)orgHP) ;
		if ( PlayerPrefs.GetInt ("mode") == 0 )
			hptext.text = (int)nowHP  + "/" + orgHP ;
		else if ( PlayerPrefs.GetInt ("mode") == 1 ) {
			hptext.text = "" ;
			if ( (int)nowHP / 3600 > 0 ) 
				hptext.text+= (int)nowHP / 3600 + "m" ;
			if ( (int)nowHP % 3600/ 60 > 0 ) 
				hptext.text+= (int)nowHP % 3600/ 60 + "h" ;
			hptext.text+= (int)nowHP % 60 + "s" ;
		}
	}

	public void LostHP( int damage ) {
		if ( PlayerPrefs.GetInt ("mode") == 0 )
			nowHP -= damage;
	}
	// Update is called once per frame
	void Update()
	{
		tmpValue = - (nowHP / orgHP);
		if (PlayerPrefs.GetInt ("mode") == 1)
			nowHP -= Time.deltaTime;
	}
} 