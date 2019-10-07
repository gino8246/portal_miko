using UnityEngine;
using System.Collections;

public class Golem : Monster {
	public float attackCD ;
	// Use this for initialization
	new void Start() {
		LoadData ();
		attackCD = 3f;
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if ( nowHP > 0 )
			Attack ();
	}
	void LoadData() {
		orgHP = 1000;	
		nowHP = orgHP;
		attack = 100;
		size = 1.2f;
		name = "Golem";
		score = 5;
		GetComponent<SpriteRenderer> ().sprite = Resources.Load("Monster/golem13", typeof(Sprite)) as Sprite ;
	}

	void Attack() {
		GameObject ally = GameObject.FindGameObjectWithTag("Player");
		Vector3 targetPos = ally.transform.position;
		if (attacking) {
			if (attackbar <= 0) {
				attacking = false;
				monster.transform.position = monsterPostion;
				
				GameObject rock = Instantiate( Resources.Load( "Monster/golemRock" ), transform.position, Quaternion.identity ) as GameObject ;
				FixedSpeedFlyer rockcs = rock.GetComponent<FixedSpeedFlyer>() ; 
				rockcs.set( "" , transform.position, targetPos, 4f, 3 ) ;
			} else if (attackbar > 1f) {
				transform.position = Vector3.MoveTowards ( transform.position, 2*transform.position - targetPos, 0.025f) ;
			} else {
				transform.position = Vector3.MoveTowards ( transform.position, targetPos, 0.1f) ;

			}
			
			attackbar -= 0.2f  ;
		} else	if ( attackCD <= 0 ) {
			attackCD = 7f ;
			//teamHp.tmpValue += 0.2f;
			attacking = true;
			attackbar = 5f;
		}
		
		attackCD -= Time.deltaTime ;
	}

	void OnMouseDown() {
		if (Ally.targetLock == transform.gameObject)
			Ally.targetLock = null;
		else {
			Ally.targetLock = transform.gameObject;
		}
	}


}
