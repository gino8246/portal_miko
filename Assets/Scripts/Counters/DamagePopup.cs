using UnityEngine;
using System.Collections;

public class DamagePopup : MonoBehaviour {
	
	//目標位置
	private Vector3 mTarget;
	//屏幕坐標
	private Vector3 mScreen;
	//傷害數值
	public int Value;
	
	//文本寬度
	public float ContentWidth=100;
	//文本高度
	public float ContentHeight=50;
	
	//GUI坐標
	private Vector2 mPoint;
	
	//銷毀時間
	public float FreeTime=1.5F;
	
	void Start () 
	{
		//獲取目標位置
		mTarget=transform.position;
		//獲取屏幕坐標
		mScreen= Camera.main.WorldToScreenPoint(mTarget);
		//將屏幕坐標轉化为GUI坐標
		mPoint=new Vector2(mScreen.x,Screen.height-mScreen.y);
		//開启自動銷毀線程
		StartCoroutine("Free");
	}
	
	void Update()
	{
		//使文本在垂直方向山產生一個偏移
		transform.Translate(Vector3.up * 0.5F * Time.deltaTime);
		//重新計算坐標
		mTarget=transform.position;
		//獲取屏幕坐標
		mScreen= Camera.main.WorldToScreenPoint(mTarget);
		//將屏幕坐標轉化为GUI坐標
		mPoint=new Vector2(mScreen.x,Screen.height-mScreen.y);
	}
	
	void OnGUI()
	{
		//保證目標在攝像機前方
		if(mScreen.z>0)
		{
			//內部使用GUI坐標進行繪制
			GUI.Label(new Rect(mPoint.x,mPoint.y,ContentWidth,ContentHeight),Value.ToString());
		}
	}
	
	IEnumerator Free()
	{
		yield return new WaitForSeconds(FreeTime);
		Destroy(this.gameObject);
	}
}